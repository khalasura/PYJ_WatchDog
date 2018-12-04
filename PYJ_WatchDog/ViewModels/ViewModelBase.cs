using log4net;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using PYJ_WatchDog.Common;
using PYJ_WatchDog.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PYJ_WatchDog.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private MetroWindow metro;
        public Dispatcher dispatcher;
        public Paragraph lastPara;

        public static RichTextBox txtLog { get; set; }
        public ILog Logger { get; set; }

        public ViewModelBase()
        {
            metro = (Application.Current.MainWindow as MetroWindow);
            dispatcher = Application.Current.Dispatcher;
            var taskName = Application.Current.ToString().Split('.')[0];
            Logger = LogHelper.Create(taskName);
            lastPara = null;
        }

        /// <summary>
        /// Metro 메세지박스
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public async void ShowMessageDialog(string title, string content)
        {
            MessageDialogResult result = await metro.ShowMessageAsync(title, content);
        }

        /// <summary>
        /// Metro 메세지박스 Yes or No
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="callback"></param>
        public async void ShowMessageYesNoDialog(string title, string content, Action<bool> callback = null)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "YES",
                NegativeButtonText = "NO",
                ColorScheme = metro.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await metro.ShowMessageAsync(title, content, MessageDialogStyle.AffirmativeAndNegative, mySettings);
            callback?.Invoke(result == MessageDialogResult.Affirmative);
        }

        public void Log(string log, LogType type, bool isNew, RichTextBox _txtLog)
        {
            // log4net 로깅
            switch (type)
            {
                case LogType.Warning:
                    Logger.Warn(log);
                    break;
                case LogType.Error:
                    Logger.Error(log);
                    break;
                default:
                    Logger.Debug(log);
                    break;
            }

            // dispatcher : 크로스 스레딩을 막기 위함.
            dispatcher.Invoke(() =>
            {
                if (_txtLog == null) return;
                if (_txtLog.Document == null) return;
                // 100 줄이 넘어가면 클리어
                if (_txtLog.Document.Blocks.Count > 100)
                    _txtLog.Document.Blocks.Clear();

                // 아이콘에 대한 더블애니메이션 (테스트해봄:시간 잡아먹으면 삭제 예정)
                var icon = new PackIcon
                {
                    Width = 20,
                    Height = 20
                };
                DoubleAnimation vertAnim = new DoubleAnimation();
                vertAnim.From = 0;
                vertAnim.To = 1;
                vertAnim.DecelerationRatio = .2;
                vertAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                var sb = new Storyboard();
                sb.Children.Add(vertAnim);
                Storyboard.SetTargetProperty(vertAnim, new PropertyPath(FrameworkElement.OpacityProperty));
                Storyboard.SetTarget(vertAnim, icon);

                // 로그 타입에 따른 아이콘, 폰트색상을 구한다.
                Brush foreColor;
                switch (type)
                {
                    case LogType.System:
                        foreColor = Brushes.DimGray;
                        icon.Kind = PackIconKind.InformationOutline;
                        break;
                    case LogType.Normal:
                        foreColor = Brushes.Black;
                        icon.Kind = PackIconKind.PlayCircle;
                        break;
                    case LogType.Warning:
                        foreColor = Brushes.DarkOrange;
                        icon.Kind = PackIconKind.BellRing;
                        break;
                    case LogType.Error:
                        foreColor = Brushes.Red;
                        icon.Kind = PackIconKind.AlertDecagram;
                        break;
                    default:
                        foreColor = Brushes.Black;
                        icon.Kind = PackIconKind.Monitor;
                        break;
                }
                icon.Foreground = foreColor;

                if (isNew)
                {
                    lastPara = null;
                    // 새로운 텍스트 라인을 생성하여 추가한다.
                    Paragraph para = new Paragraph
                    {
                        Margin = new Thickness(0),
                        Foreground = foreColor,
                    };
                    para.Inlines.Add(icon);
                    para.Inlines.Add(new Run($" [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ") { BaselineAlignment = BaselineAlignment.Center });
                    para.Inlines.Add(new Run(log) { BaselineAlignment = BaselineAlignment.Center });
                    _txtLog.Document.Blocks.Add(para);
                    sb.Begin();
                }
                else
                {
                    // 마지막 텍스트 라인의 값만 변경한다. 단, 마지막 텍스트 라인이 없다면 한번만 생성하여 추가한다.
                    if (lastPara == null)
                    {
                        lastPara = new Paragraph
                        {
                            Margin = new Thickness(0),
                            Foreground = foreColor,
                        };
                        _txtLog.Document.Blocks.Add(lastPara);
                        sb.Begin();
                    }
                    try
                    {
                        var last = _txtLog.Document.Blocks.LastOrDefault();
                        var para = last as Paragraph;
                        para.Inlines.Clear();
                        para.Inlines.Add(icon);
                        para.Inlines.Add(new Run($" [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ") { BaselineAlignment = BaselineAlignment.Center });
                        para.Inlines.Add(new Run(log) { BaselineAlignment = BaselineAlignment.Center });
                    }
                    catch
                    {
                        Paragraph para = new Paragraph
                        {
                            Margin = new Thickness(0),
                            Foreground = foreColor,
                        };
                        para.Inlines.Add(icon);
                        para.Inlines.Add(new Run($" [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ") { BaselineAlignment = BaselineAlignment.Center });
                        para.Inlines.Add(new Run(log) { BaselineAlignment = BaselineAlignment.Center });
                        _txtLog.Document.Blocks.Add(para);
                        sb.Begin();
                    }
                }
                // 스크롤 끝으로 이동.
                _txtLog.ScrollToEnd();
            });
        }

        public void Log(string log, LogType type, bool isNew=true)
        {
            Log(log, type, isNew, txtLog);
        }
    }
}
