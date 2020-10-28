using PYJ_WatchDog.Models;
using PYJ_WatchDog.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.Common
{
    public static class FileManager
    {
        public static bool IsPosSetting = false;
        public static Action<string> OnResponseFail;    // 응답 없음 발생
        public static Action<string> OnRunTask;         // 프로그램 실행
        public static Action<string> OnKillTask;        // 프로그램 강제종료
        public static Action OnWerFault;        // 윈도우 에러 리포팅 발생(WerFault)

        #region 윈도우메세지 상수
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MOVE = 0xF010;
        public const int SC_RESTORE = 0xF120;
        public const int SC_SIZE = 0xF000;
        #endregion

        #region 윈도우메세지 함수
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string NoteName);
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(int hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]

        private static extern int GetWindowRect(int hwnd, out Rectangle rect);
        #endregion

        /// <summary>
        /// 프로그램 상태 체크
        /// </summary>
        public static void TaskCheckAll()
        {
            try
            {
                // 리스트에 등록된 프로세스 상태를 할당한다.
                MyAppInfo.Instance().Setting.TaskList.ToList().ForEach(g =>
                {
                    var procs = Process.GetProcessesByName(g.Name);
                    if (procs.Length > 0)
                    {
                        g.IsRun = true;
                        g.MemorySize = (procs[0].WorkingSet64 / 1024f).ToString("#,##0 K");
                        g.StartTime = procs[0].StartTime.ToString();
                        g.IsResponse = procs[0].Responding;
                        g.PID = procs[0].Id.ToString();
                        g.Node = string.IsNullOrEmpty(g.Node) ? procs[0].MainWindowTitle : g.Node;
                        GetWindowStatus(g);
                    }
                    else
                    {
                        g.IsRun = false;
                        g.MemorySize = string.Empty;
                        g.StartTime = string.Empty;
                        g.PID = string.Empty;
                        g.IsResponse = false;
                    }
                });

                // WerFault가 실행 되었는지 체크한다.
                var wer = Process.GetProcessesByName("WerFault");
                if (wer.Length > 0)
                {
                    // 기존에 감지되지 않았다면 이벤트를 전송한다. (이벤트 한번만 전송하기 위함)
                    if (!MyAppInfo.Instance().IsWerFault)
                    {
                        MyAppInfo.Instance().IsWerFault = true;
                        OnWerFault?.Invoke();
                    }                        
                }
                else
                    MyAppInfo.Instance().IsWerFault = false;


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 모든 프로그램 실행
        /// </summary>
        /// <returns></returns>
        public static List<string> RunProcess()
        {
            List<string> arrResult = new List<string>();
            MyAppInfo.Instance().Setting.TaskList.ToList().ForEach(g =>
            {
                RunProcess(g);
                arrResult.Add(g.Name);
            });
            return arrResult;
        }

        /// <summary>
        /// 단일 프로그램 실행
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string RunProcess(TaskInfo task)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            if (task == null) return string.Empty;
            //// 프로그램이 실행 중이지만 응답이 없는 경우 WerFault 킬 이후 해당 프로그램 킬.
            //if (task.IsRun && !task.IsResponse)
            //{
            //    var arg1 = KillProcess("WerFault");
            //    var arg2 = KillProcess(task.Name);
            //    OnResponseFail?.Invoke(arg1, arg2);
            //}

            // 응답이 없는 경우 프로그램 킬
            if (MyAppInfo.Instance().Setting.KillNotRespond)
            {
                if (task.IsRun && !task.IsResponse)
                {
                    var arg = KillProcess(task.Name);
                    OnResponseFail?.Invoke(arg);
                }
            }

            // 프로그램 실행
            if (!task.IsRun)
            {
                psi.FileName = task.FilePath;
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                if (File.Exists(psi.FileName))
                {
                    Process.Start(psi);
                    OnRunTask?.Invoke(task.Name);
                    return task.Name;
                }                    
            }
            return string.Empty;
        }

        /// <summary>
        /// 모든 프로그램 종료
        /// </summary>
        /// <returns></returns>
        public static List<string> KillProcess()
        {
            List<string> arrResult = new List<string>();
            MyAppInfo.Instance().Setting.TaskList.ToList().ForEach(g =>
            {
                KillProcess(g.Name);
                arrResult.Add(g.Name);
            });
            return arrResult;
        }

        /// <summary>
        /// 단일 프로그램 종료
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string KillProcess(string name)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.ToUpper() == name.ToUpper())
                {
                    proc.Kill();
                    OnKillTask?.Invoke(name);
                    return name;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 프로그램 숨기기
        /// </summary>
        /// <param name="task"></param>
        public static void HideProcess(TaskInfo task)
        {
            if (task == null) return;
            if (task.Node == string.Empty) return;
            var handle = FindWindow(null, task.Node);
            SendMessage(handle, WM_SYSCOMMAND, SC_MINIMIZE, 0);
        }

        /// <summary>
        /// 프로그램 보이기
        /// </summary>
        /// <param name="task"></param>
        public static void ShowProcess(TaskInfo task)
        {
            if (task == null) return;
            if (task.Node == string.Empty) return;
            var handle = FindWindow(null, task.Node);
            //SendMessage(handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
            SendMessage(handle, WM_SYSCOMMAND, SC_RESTORE, 0);
            MoveWindow(handle, task.X, task.Y, task.Width, task.Height, true);         
        }

        /// <summary>
        /// 프로그램 윈도우 위치,크기 가져오기
        /// </summary>
        /// <param name="task"></param>
        public static void GetWindowStatus(TaskInfo task)
        {
            if (IsPosSetting) return;
            if (task == null) return;
            if (task.Node == string.Empty) return;
            var handle = FindWindow(null, task.Node);
            Rectangle rect;
            GetWindowRect(handle, out rect);
            if (rect.X == -32000 && rect.Y == -32000) return;
            if (rect.X == 0 && rect.Y == 0 && rect.Width ==0 && rect.Height == 0) return;
            task.X = rect.X;
            task.Y = rect.Y;
            task.Width = rect.Width - rect.X;
            task.Height = rect.Height - rect.Y;
            //task.Width = rect.Width;
            //task.Height = rect.Height;
        }
    }
}
