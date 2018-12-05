using Prism.Mvvm;
using PYJ_WatchDog.Common;
using PYJ_WatchDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.ViewModels
{
    public class PopupDialogBase : BindableBase
    {
        private Settings setting;
        public Settings Setting
        {
            get { return setting; }
            set { SetProperty(ref setting, value); }
        }

        private TaskInfo task;
        public TaskInfo Task
        {
            get { return task; }
            set { SetProperty(ref task, value); }
        }

        public PopupDialogBase()
        {
            MyAppInfo.Instance().LoadSettings();
            InitProperty();
        }

        private void InitProperty()
        {
            Task = new TaskInfo();
            Setting = new Settings
            {
                IsAuto = MyAppInfo.Instance().setting.IsAuto,
                CheckTick = MyAppInfo.Instance().setting.CheckTick,
                KillNotRespond = MyAppInfo.Instance().setting.KillNotRespond,
                TaskList = MyAppInfo.Instance().setting.TaskList
            };
        }
    }
}
