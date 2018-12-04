using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.Common
{
    /// <summary>
    /// 사용법: ILog Log = LogHelper.Create();
    /// </summary>
    public class LogHelper
    {
        // 기본 로그파일 이름
        private const string DefaultName = "Log";
        // 기본 출력 패턴
        private const string DefaultPattern = "%d %-5p - %m%n";
        // 기본 Appender
        private static IAppender DefaultAppender;


        #region Create() 기본 Appender 인 RollingFileAppender 외에 다른 Appender 를 추가 할 수 있음.
        public static ILog Create(Type type)
        {
            DefaultAppender = GetRollingFileAppender($@"Log\{type.Namespace.Split('.')[0]}.log");
            return Create(type.FullName);
        }

        public static ILog Create(string name = DefaultName)
        {
            DefaultAppender = GetRollingFileAppender($@"Log\{name}.log");
            return Create(name, null);
        }
        public static ILog Create(params IAppender[] appender)
        {
            DefaultAppender = GetRollingFileAppender($@"Log\{DefaultName}.log");
            return Create(DefaultName, appender);
        }

        public static ILog Create(Type type, params IAppender[] appender)
        {
            DefaultAppender = GetRollingFileAppender($@"Log\{type.Namespace.Split('.')[0]}.log");
            return Create(type.FullName, appender);
        }

        public static ILog Create(string name, params IAppender[] appender)
        {
            DefaultAppender = GetRollingFileAppender($@"Log\{name}.log");
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.AddAppender(DefaultAppender);
            appender?.ToList().ForEach(g => { hierarchy.Root.AddAppender(g); });
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            return LogManager.GetLogger(name);
        }
        #endregion

        // Appender 추가
        public static void AddAppender(IAppender appender)
        {
            Hierarchy hierarchy =
                (Hierarchy)LogManager.GetRepository();

            hierarchy.Root.AddAppender(appender);
        }

        // RollingFileAppender : 로그 텍스트 파일을 생성
        private static IAppender GetRollingFileAppender(string logFile, string pattern = DefaultPattern)
        {
            var layout = new PatternLayout(pattern);
            layout.ActivateOptions();

            var appender = new RollingFileAppender
            {
                File = logFile,
                AppendToFile = true,
                Layout = layout,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaximumFileSize = "10MB",
                MaxSizeRollBackups = 10,
                StaticLogFileName = true,
            };
            appender.ActivateOptions();

            return appender;
        }

        // ConsolAppender : 콘솔 응용프로그램 창에 로그 표시
        public static IAppender GetConsolAppender(string pattern = DefaultPattern)
        {
            var layout = new PatternLayout(pattern);
            layout.ActivateOptions();

            var appender = new ConsoleAppender
            {
                Layout = layout,
            };
            appender.ActivateOptions();

            return appender;
        }

        // ColoredConsoleAppender : 콘솔 응용프로그램에 칼라풀하게 로그 표시
        public static IAppender GetColoredConsoleAppender(bool HighlightColor = false, string pattern = DefaultPattern)
        {
            var layout = new PatternLayout(pattern);
            layout.ActivateOptions();

            var appender = new ColoredConsoleAppender
            {
                Layout = layout,
            };
            // Debug : White
            var light = ColoredConsoleAppender.Colors.HighIntensity;
            var color = ColoredConsoleAppender.Colors.White;
            appender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Trace,
                ForeColor = HighlightColor ? color | light : color,
            });
            // Info : Green
            color = ColoredConsoleAppender.Colors.Green;
            appender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Info,
                ForeColor = HighlightColor ? color | light : color,
            });
            // Warning : Yellow
            color = ColoredConsoleAppender.Colors.Yellow;
            appender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Warn,
                ForeColor = HighlightColor ? color | light : color,
            });
            // Error : Red
            color = ColoredConsoleAppender.Colors.Red;
            appender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Error,
                ForeColor = HighlightColor ? color | light : color,
            });
            // Fatal : Purple
            color = ColoredConsoleAppender.Colors.Purple;
            appender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Fatal,
                ForeColor = HighlightColor ? ColoredConsoleAppender.Colors.White | light : ColoredConsoleAppender.Colors.White,
                BackColor = HighlightColor ? color | light : color,
            });

            appender.ActivateOptions();

            return appender;
        }

        // UdpAppender : 로그를 UDP 프로토콜로 전송한다.
        public static IAppender GetUdpAppender(IPAddress ip, int port, string pattern = DefaultPattern)
        {
            var layout = new PatternLayout(pattern);
            layout.ActivateOptions();

            var appender = new UdpAppender
            {
                Layout = layout,
                RemoteAddress = ip,
                RemotePort = port
            };
            appender.ActivateOptions();

            return appender;
        }

        // AdoNetAppender : 로그를 MS SQL Server에 Insert 한다.
        public static IAppender GetMsSqlAppender(string dataSource, string dbName, string user, string password, int bufferSize = 1)
        {
            #region 로그 테이블 스키마
            //CREATE TABLE [dbo].[TbLog] (
            //    [Id] [int] IDENTITY (1, 1) NOT NULL,
            //    [Date] [datetime] NOT NULL,
            //    [Thread] [varchar] (255) NOT NULL,
            //    [Level] [varchar] (50) NOT NULL,
            //    [Logger] [varchar] (255) NOT NULL,
            //    [Message] [varchar] (4000) NOT NULL,
            //    [Exception] [varchar] (2000) NULL,
            //    [User] [varchar] (50) NULL,
            //    [Host] [varchar] (50) NULL
            //) 
            #endregion

            var appender = new AdoNetAppender
            {
                BufferSize = bufferSize, // 버퍼 사이즈 만큼 로그가 쌓이면 DB에 저장 (로그 발생 시 마다 저장할려면 1)
                ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version = 1.0.3300.0, Culture = neutral, PublicKeyToken = b77a5c561934e089",
                ConnectionString = $"data source={dataSource};initial catalog={dbName};User ID={user};Password={password};persist security info=True;integrated security=false;",
                CommandText = "INSERT INTO TbLog([Date], [Thread], [Level], [Logger], [Message], [Exception], [User], [Host]) VALUES(@log_date, @thread, @log_level, @logger, @message, @exception, @user, @host)",
            };

            #region 파라미터 설정
            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "log_date",
                DbType = System.Data.DbType.DateTime,
                Layout = new RawUtcTimeStampLayout()
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "thread",
                DbType = System.Data.DbType.String,
                Size = 255,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread"))
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "log_level",
                DbType = System.Data.DbType.String,
                Size = 50,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level"))
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "logger",
                DbType = System.Data.DbType.String,
                Size = 255,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger"))
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "message",
                DbType = System.Data.DbType.String,
                Size = 4000,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%message"))
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "exception",
                DbType = System.Data.DbType.String,
                Size = 2000,
                Layout = new Layout2RawLayoutAdapter(new ExceptionLayout())
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "user",
                DbType = System.Data.DbType.String,
                Size = 50,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%property{log4net:UserName}"))
            });

            appender.AddParameter(new AdoNetAppenderParameter
            {
                ParameterName = "host",
                DbType = System.Data.DbType.String,
                Size = 50,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%property{log4net:HostName}"))
            });
            #endregion

            appender.ActivateOptions();
            return appender;
        }
    }
}
