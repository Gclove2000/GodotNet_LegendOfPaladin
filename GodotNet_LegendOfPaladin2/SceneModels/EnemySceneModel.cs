using GodotNet_LegendOfPaladin2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class EnemySceneModel : ISceneModel
    {
        private PrintHelper printHelper;

        public EnemySceneModel(PrintHelper printHelper)
        {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(EnemySceneModel));  
        }
        public EnemySceneModel() { }
        public override void Process(double delta)
        {
        }

        public override void Ready()
        {
            printHelper.Debug("加载成功!");
        }
    }
}
