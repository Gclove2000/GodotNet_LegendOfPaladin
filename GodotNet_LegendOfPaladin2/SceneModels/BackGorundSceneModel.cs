using GodotNet_LegendOfPaladin2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class BackGorundSceneModel : ISceneModel
    {

        private PrintHelper printHelper;

        public BackGorundSceneModel(PrintHelper printHelper) {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(BackGorundSceneModel));
        }
        public override void Process(double delta)
        {
            
        }

        public override void Ready()
        {
            printHelper.Debug("加载成功！");
        }
    }
}
