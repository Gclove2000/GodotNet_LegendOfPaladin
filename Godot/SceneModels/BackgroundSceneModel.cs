using GodotProgram.Interfaces;
using GodotProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.SceneModels
{
    public class BackgroundSceneModel : ISceneModel
    {

        private NlogHelper nlogHelper;

        public BackgroundSceneModel(NlogHelper nlogHelper)
        {
            this.nlogHelper = nlogHelper;
        }

        public override void Process(double delta)
        {
            //throw new NotImplementedException();
        }

        public override void Ready()
        {
            //throw new NotImplementedException();
        }
    }
}
