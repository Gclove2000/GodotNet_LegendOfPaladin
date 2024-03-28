using Godot;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.Utils
{
    public class NlogHelper
    {
        private Logger logger;

        public NlogHelper()
        {
            var url = string.Format("{0}Assets/NLog.config", AppDomain.CurrentDomain.BaseDirectory.ToString());
            GD.Print($"Nlog加载完毕，url地址为[{url}]");
            LogManager.Configuration = new XmlLoggingConfiguration(url);

            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            GD.Print(msg);
            logger.Debug(msg);
        }

        public void Info(string msg)
        {
            GD.Print($"[info]:{msg}");
            logger.Info(msg);

        }

        public void Error(string msg)
        {
            GD.PrintErr(msg);
            GD.PushError(msg);
            logger.Error(msg);
        }

        public void Warning(string msg)
        {
            GD.Print($"[warning]:{msg}");
            GD.PushWarning(msg);
            logger.Warn(msg);
        }
    }
}
