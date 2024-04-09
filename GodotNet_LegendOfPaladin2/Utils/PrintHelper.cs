using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.Utils
{
    public class PrintHelper
    {
        private NlogHelper nlogHelper;

        private string title;
        public PrintHelper(NlogHelper nlogHelper)
        {
            this.nlogHelper = nlogHelper;
        }

        public void SetTitle(string title)
        {
            this.title = $"[{title}]:";
        }


        public void Debug(string msg)
        {
            nlogHelper.Debug(title + msg);
        }

        public void Error(string msg)
        {
            nlogHelper.Error(title + msg);
        }

        public void Info(string msg)
        {
            nlogHelper.Info(title + msg);
        }

        public void Warning(string msg)
        {
            nlogHelper.Warning(title + msg);
        }
    }
}
