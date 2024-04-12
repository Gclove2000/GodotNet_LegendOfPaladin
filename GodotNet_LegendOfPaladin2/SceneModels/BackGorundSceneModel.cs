using Godot;
using GodotNet_LegendOfPaladin2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class BackGorundSceneModel : ISceneModel
    {
        private TileMap tileMap;
        private PrintHelper printHelper;

        public Rect2 TileMapSize { get;private set; }

        public BackGorundSceneModel(PrintHelper printHelper)
        {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(BackGorundSceneModel));
        }
        public override void Process(double delta)
        {

        }

        public override void Ready()
        {
            printHelper.Debug("加载成功！");
            tileMap = Scene.GetNode<TileMap>("TileMap");

            var tileMapSize = tileMap.TileSet.TileSize;
            var tileMapReact = tileMap.GetUsedRect();
            



            var position = tileMapReact.Position * tileMapSize;
            var size = tileMapReact.Size*tileMapSize;

            TileMapSize = new Rect2(position,size);

 

        }
    }
}
