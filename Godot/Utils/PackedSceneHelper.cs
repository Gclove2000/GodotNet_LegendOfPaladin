using Godot;
using GodotNet_LegendOfPaladin.SceneScripts;
using GodotProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.Utils
{
    public class PackedSceneHelper
    {
        public NlogHelper nlogHelper;


        public Dictionary<string, PackedScene> keyValuePairs = new Dictionary<string, PackedScene>();

        public PackedSceneHelper(NlogHelper nlogHelper)
        {
            this.nlogHelper = nlogHelper;
            Load();
        }
        /// <summary>
        /// 方便加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        private PackedScene SetPackedScene(string sceneName)
        {
            var targetName = sceneName.Replace("Scene", "");
            var url = $"res://Scenes//{targetName}.tscn";
            nlogHelper.Info($"加载PackedScene,{sceneName}:{url}");
            var res = ResourceLoader.Load<PackedScene>(url);
            return res;
        }

        private void Load()
        {
            AddItem(nameof(MainScene));
            AddItem(nameof(PlayerScene));
        }

        private void AddItem(string sceneName)
        {
            keyValuePairs.Add(sceneName, SetPackedScene(sceneName));
        }

    }
}
