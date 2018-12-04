using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PYJ_WatchDog.Common;
using PYJ_WatchDog.Models;
using PYJ_WatchDog.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using MahApps.Metro.Controls;
using Prism.Events;
using PYJ_WatchDog.Events;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using Unity;

namespace PYJ_WatchDog.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        protected MainWindow thisCtrl;
        private MyAppInfo App;

        private string _title = "WatchDog";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set { SetProperty(ref currentTime, value); }
        }
        private int pbVal;
        public int PbVal
        {
            get { return pbVal; }
            set { SetProperty(ref pbVal, value); }
        }

        private int pbMax;
        public int PbMax
        {
            get { return pbMax; }
            set { SetProperty(ref pbMax, value); }
        }

        private bool isAuto;
        public bool IsAuto
        {
            get { return isAuto; }
            set { SetProperty(ref isAuto, value); }
        }

        private string selName;
        public string SelName
        {
            get { return selName; }
            set { SetProperty(ref selName, value); }
        }
        public IEventAggregator _EventAggregator;
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {            
            _EventAggregator = eventAggregator;
            App = MyAppInfo.Instance();

            App.LoadSettings();

            FileManager.OnRunTask = (s) => 
            {
                Log($"Run: {s}", LogType.Normal);
            };
            FileManager.OnKillTask = (s) =>
            {
                Log($"Kill: {s}", LogType.Normal);
            };
            FileManager.OnResponseFail = (s1, s2) => 
            {
                Log($"응답없음 발생: {s1}, {s2}", LogType.Warning);
            };

            #region 스레드
            Task.Run(async () =>
            {
                while (true)
                {
                    // 현재시각 표시
                    CurrentTime = DateTime.Now;

                    // 프로그램 상태 체크
                    FileManager.TaskCheckAll();

                    // 자동 실행
                    if (App.setting.IsAuto)
                    {
                        if (App.StxTick >= App.setting.CheckTick)
                        {
                            App.StxTick = 0;
                            FileManager.RunProcess();
                        }
                        else
                            App.StxTick++;                              
                    }
                    else
                        App.StxTick = 0;

                    // 현재 정보 이벤트 게시
                    _EventAggregator.GetEvent<SettingEvent>().Publish(MyAppInfo.Instance().setting);

                    // 프로그레스바
                    PbVal = App.StxTick;
                    PbMax = App.setting.CheckTick;
                    IsAuto = App.setting.IsAuto;
                    SelName = App.SelName;

                    // 스레드 지연시간
                    await Task.Delay(1000);
                }
            });
            #endregion
        }

        // 뷰로드 커맨드
        private DelegateCommand<RoutedEventArgs> loadedCommand;
        public DelegateCommand<RoutedEventArgs> LoadedCommand =>
            loadedCommand ?? (loadedCommand = new DelegateCommand<RoutedEventArgs>((e) =>
            {
                if (thisCtrl == null)
                {
                    thisCtrl = e.Source as MainWindow;
                    thisCtrl.Closing += ThisCtrl_Closing;
                }
            }));

        // 폼 클로즈 이벤트
        private void ThisCtrl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
            //ShowMessageYesNoDialog("프로그램 종료", "프로그램을 종료하겠습니까?", (flag) =>
            //{
            //    if (flag)
            //    {
            //        App.SaveSetting();
            //        Log("WatchDog 종료", LogType.System);
            //        Application.Current.Shutdown();
            //    }                    
            //});
            App.SaveSetting();

        }
    }
}
