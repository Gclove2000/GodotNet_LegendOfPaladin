﻿using GodotNet_LegendOfPaladin2.DB;
using GodotNet_LegendOfPaladin2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class MainSceneModel : ISceneModel
    {
        private PrintHelper printHelper;

        private FreeSqlHelper freeSqlHelper;

        public MainSceneModel(PrintHelper printHelper, FreeSqlHelper freeSqlHelper)
        {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(MainSceneModel));
            this.freeSqlHelper = freeSqlHelper;
        }

        public override void Process(double delta)
        {

        }

        public override void Ready()
        {

            printHelper.Debug("主场景加载完成");

        }
    }
}
