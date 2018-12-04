using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PYJ_WatchDog.ViewModels
{
	public class TaskDialogViewModel : PopupDialogBase
    {
        // 폴더찾기 커맨드
        private DelegateCommand cmdSearch;
        public DelegateCommand CmdSearch =>
            cmdSearch ?? (cmdSearch = new DelegateCommand(() =>
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".exe";
                dlg.Filter = "EXE Files (*.exe)|*.exe";
                bool? result = dlg.ShowDialog();
                if (result == true)
                {
                    Task.FilePath = dlg.FileName;
                    var tmp = dlg.FileName.Split('\\');
                    var tmp2 = tmp[tmp.Length - 1].Split('.')[0];
                    Task.Name = tmp2;
                    if (String.IsNullOrEmpty(Task.Desc)) Task.Desc = tmp2;
                    RaisePropertyChanged("Task");
                }
            }));
    }
}
