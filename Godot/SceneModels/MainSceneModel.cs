using Godot;
using GodotProgram.DB;
using GodotProgram.Interfaces;
using GodotProgram.Services;
using GodotProgram.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.SceneModels
{
    public class MainSceneModel : ISceneModel
    {
        private NlogHelper nlogHelper;
        private TestService testService;

        private TestUtils testUtils = new TestUtils();
        private FreeSqlHelper freeSqlHelper;

        public MainSceneModel(TestService testService, NlogHelper nlogHelper, FreeSqlHelper freeSqlHelper)
        {
            this.testService = testService;
            this.nlogHelper = nlogHelper;
            this.freeSqlHelper = freeSqlHelper;
        }
        public override void Process(double delta)
        {

        }

        public override void Ready()
        {
            GD.Print("Hello Godot!");
            
        }
    }
}
