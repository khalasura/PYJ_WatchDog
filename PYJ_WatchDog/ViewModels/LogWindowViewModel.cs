using Prism.Commands;
using Prism.Mvvm;
using PYJ_WatchDog.Common;
using PYJ_WatchDog.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace PYJ_WatchDog.ViewModels
{
	public class LogWindowViewModel : ViewModelBase
    {
        private double sizeH;
        public double SizeH
        {
            get { return sizeH; }
            set { SetProperty(ref sizeH, value); }
        }
        protected LogWindow thisCtrl;
        public LogWindowViewModel()
        {

        }

        // 뷰로드 커맨드
        private DelegateCommand<RoutedEventArgs> loadedCommand;
        public DelegateCommand<RoutedEventArgs> LoadedCommand =>
            loadedCommand ?? (loadedCommand = new DelegateCommand<RoutedEventArgs>((e) =>
            {
                if (thisCtrl == null)
                {
                    thisCtrl = e.Source as LogWindow;
                    thisCtrl.txtLog.Document = new FlowDocument();
                    txtLog = thisCtrl.txtLog;
                    Log("WatchDog 시작", LogType.System);

                    thisCtrl.SizeChanged += ThisCtrl_SizeChanged;
                }
            }));

        private void ThisCtrl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var test = e;
            SizeH = e.NewSize.Height;
        }
    }
}
