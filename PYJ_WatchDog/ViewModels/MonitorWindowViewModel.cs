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

        IEventAggregator _EventAggregator;
        public MonitorWindowViewModel(IEventAggregator eventAggregator)
        {
            App = MyAppInfo.Instance();
            FileManager.TaskCheckAll();
            //this.TaskList = App.setting.TaskList;

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
                                //_EventAggregator.GetEvent<SettingEvent>().Publish(App.setting);
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
                                //_EventAggregator.GetEvent<SettingEvent>().Publish(App.setting);
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
                                var task = App.setting.TaskList.FirstOrDefault(g => g.Name == App.SelName);
                                FileManager.RunProcess(task);
                                FileManager.TaskCheckAll();
                                //_EventAggregator.GetEvent<SettingEvent>().Publish(App.setting);
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
                                //_EventAggregator.GetEvent<SettingEvent>().Publish(App.setting);
                            }
                        });
                        break;
                    case "add":
                        DialogHost.Show(new TaskDialog(), "RootDialog", (obj, e) =>
                        {
                            if (e.Parameter == null) return;
                            var task = e.Parameter as TaskInfo;
                            App.setting.TaskList.Add(task);
                            this.TaskList = new ObservableCollection<TaskInfo>(App.setting.TaskList);
                            App.SaveSetting();
                        });
                        break;
                    case "remove":
                        var find = App.setting.TaskList.FirstOrDefault(g => g.Name == App.SelName);
                        if (find != null)
                        {
                            ShowMessageYesNoDialog("삭제", $"[{App.SelName}] 프로그램을 목록에서 삭제합니까?", yes =>
                            {
                                if (yes)
                                {
                                    App.setting.TaskList.Remove(find);
                                    this.TaskList = new ObservableCollection<TaskInfo>(App.setting.TaskList);
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
                            App.setting.IsAuto = setting.IsAuto;
                            App.setting.CheckTick = setting.CheckTick;
                            App.setting.KillNotRespond = setting.KillNotRespond;
                            App.SaveSetting();
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
                        dlg.Left = Screen.PrimaryScreen.Bounds.Width;
                        dlg.Top = Screen.PrimaryScreen.Bounds.Height;
                        dlg.Location = Screen.PrimaryScreen.Bounds.Location;
                        dlg.WindowState = FormWindowState.Maximized;

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
