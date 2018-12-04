using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PYJ_WatchDog.Models;
using PYJ_WatchDog.ViewModels;
using PYJ_WatchDog.Views;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PYJ_WatchDog.Common
{
    public class MyAppInfo
    {
        private static MyAppInfo appInfo = null;        
        private string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Settings.json";

        public Settings setting { get; set; }
        public string SelName { get; set; }
        public int StxTick { get; set; }

        private MyAppInfo()
        {
            setting = new Settings();
        }

        public static MyAppInfo Instance()
        {
            if (appInfo == null)
                appInfo = new MyAppInfo();
            return appInfo;
        }
        public void LoadSettings()
        {
            if (File.Exists(FilePath))
            {
                setting = File.ReadAllText(FilePath).FromJson<Settings>();
            }
            else
            {
                setting.CheckTick = 5;
                setting.TaskList = new List<TaskInfo>();
            }
        }

        public void SaveSetting()
        {
            File.WriteAllText(FilePath, setting.ToJson());
        }
    }
}
