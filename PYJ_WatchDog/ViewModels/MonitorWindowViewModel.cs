using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PYJ_WatchDog.Common;
using PYJ_WatchDog.Events;
using PYJ_WatchDog.Models;
using PYJ_WatchDog.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Forms;

namespace PYJ_WatchDog.ViewModels
{
    public class MonitorWindowViewModel : ViewModelBase
    {
        private MyAppInfo App;
        private TaskInfo selTask;
        private BackgroudForm dlg = new BackgroudForm();
        public TaskInfo SelTask
        {
            get { return selTask; }
            set
            {
                if (value != null)
                    App.SelName = value.Name;
                SetProperty(ref selTask, value);
            }
        }
        private ObservableCollection<TaskInfo> taskList;
        public ObservableCollection<TaskInfo> TaskList
        {
            get { return taskList; }
            set { SetProperty(ref taskList, value); }
        }

        private bool isAuto;
        public bool IsAuto
        {
            get { return isAuto; }
            set 
            {
                SetProperty(ref isAuto, value);
                App.Setting.IsAuto = value;
                App.SaveSetting();
            }
        }

        IEventAggregator _EventAggregator;
        public MonitorWindowViewModel(IEventAggregator eventAggregator)
        {
            App = MyAppInfo.Instance();            
            IsAuto = App.Setting.IsAuto;
            FileManager.TaskCheckAll();

            _EventAggregator = eventAggregator;
            _EventAggregator.GetEvent<SettingEvent>().Subscribe((e) =>
            {
                this.TaskList = new ObservableCollection<TaskInfo>(e.TaskList);
                RaisePropertyChanged("TaskList");
            });
        }

        private DelegateCommand<string> menuCommand;
        public DelegateCommand<string> MenuCommand =>
            menuCommand ?? (menuCommand = new DelegateCommand<string>((s) =>
            {
                switch (s.ToLower())
                {
                    case "allrun":
                        ShowMessageYesNoDialog("실행", "모두 실행 하시겠습니까?", yes =>
                        {
                            if (yes)
                            {
                                FileManager.RunProcess();
                                FileManager.TaskCheckAll();
                            }
                        });
                        break;
                    case "allstop":
                        ShowMessageYesNoDialog("정지", "모두 정지 하시겠습니까?", yes =>
                        {
                            if (yes)
                            {
                                FileManager.KillProcess();
                                Thread.Sleep(200);
                                FileManager.TaskCheckAll();
                            }
                        });
                        break;
                    case "run":
                        if (App.SelName == null)
                        {
                            ShowMessageDialog("에러", "프로그램을 선택하세요");
                            return;
                        }
                        ShowMessageYesNoDialog("실행", $"[{App.SelName}] 프로그램을 실행 하시겠습니까?", yes =>
                        {
                            if (yes)
                            {
                                var task = App.Setting.TaskList.FirstOrDefault(g => g.Name == App.SelName);
                                FileManager.RunProcess(task);
                                FileManager.TaskCheckAll();
                            }
                        });
                        break;
                    case "stop":
                        if (App.SelName == null)
                        {
                            ShowMessageDialog("에러", "프로그램을 선택하세요");
                            return;
                        }
                        ShowMessageYesNoDialog("정지", $"[{App.SelName}] 프로그램을 정지 하시겠습니까?", yes =>
                        {
                            if (yes)
                            {
                                FileManager.KillProcess(App.SelName);
                                Thread.Sleep(200);
                                FileManager.TaskCheckAll();
                            }
                        });
                        break;
                    case "add":
                        DialogHost.Show(new TaskDialog(), "RootDialog", (obj, e) =>
                        {
                            if (e.Parameter == null) return;
                            var task = e.Parameter as TaskInfo;
                            App.Setting.TaskList.Add(task);
                            this.TaskList = new ObservableCollection<TaskInfo>(App.Setting.TaskList);
                            App.SaveSetting();
                        });
                        break;
                    case "remove":
                        var find = App.Setting.TaskList.FirstOrDefault(g => g.Name == App.SelName);
                        if (find != null)
                        {
                            ShowMessageYesNoDialog("삭제", $"[{App.SelName}] 프로그램을 목록에서 삭제합니까?", yes =>
                            {
                                if (yes)
                                {
                                    App.Setting.TaskList.Remove(find);
                                    this.TaskList = new ObservableCollection<TaskInfo>(App.Setting.TaskList);
                                    App.SaveSetting();
                                }
                            });
                        }
                        break;
                    case "setting":
                        DialogHost.Show(new SettingDialog(), "RootDialog", (obj, e) =>
                        {
                            if (e.Parameter == null) return;
                            var setting = e.Parameter as Settings;
                            App.Setting.IsAuto = setting.IsAuto;
                            App.Setting.CheckTick = setting.CheckTick;
                            App.Setting.KillNotRespond = setting.KillNotRespond;
                            App.SaveSetting();
                            IsAuto = App.Setting.IsAuto;
                        });
                        break;
                    case "hide":
                        if (SelTask == null) return;
                        FileManager.HideProcess(SelTask);
                        break;
                    case "show":
                        if (SelTask == null) return;
                        FileManager.ShowProcess(SelTask);
                        break;
                    case "pos":
                        if (SelTask == null) return;
                        FileManager.IsPosSetting = true;
                        dlg.Task = SelTask;
                        dlg.TopMost = true;
                        dlg.ShowInTaskbar = false;

                        dlg.FormBorderStyle = FormBorderStyle.None;
                        dlg.Opacity = 0.5;

                        dlg.StartPosition = FormStartPosition.Manual;
                        dlg.Width = Screen.AllScreens.Sum(g => g.Bounds.Width);
                        dlg.Height = Screen.AllScreens.Sum(g => g.Bounds.Height);
                        dlg.Location = Screen.PrimaryScreen.Bounds.Location;
                        dlg.WindowState = FormWindowState.Normal;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            SelTask.X = dlg.nStxX;
                            SelTask.Y = dlg.nStxY;
                            SelTask.Width = dlg.nWidth;
                            SelTask.Height = dlg.nHeight;
                            FileManager.HideProcess(SelTask);
                            FileManager.ShowProcess(SelTask);
                            FileManager.IsPosSetting = false;
                            MyAppInfo.Instance().SaveSetting();
                        }
                        break;
                    default:
                        break;
                }
            }));
    }
}
