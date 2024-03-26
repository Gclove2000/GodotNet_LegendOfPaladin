using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.Interfaces
{
    /// <summary>
    /// SceneModel的基类
    /// </summary>
    public abstract class ISceneModel
    {
        /// <summary>
        /// 挂载场景，强制Node2D
        /// </summary>
        public Node2D? Sence { get; set; }

        /// <summary>
        /// 打包场景，用于生成
        /// </summary>

        public PackedScene? PackedScene { get; set; }

        /// <summary>
        /// 重载Ready事件
        /// </summary>
        public abstract void Ready();

        /// <summary>
        /// 重载Process事件
        /// </summary>
        /// <param name="delta"></param>
        public abstract void Process(double delta);

        /// <summary>
        /// 方便加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        public virtual void SetPackedScene(string sceneName)
        {
            var targetName = sceneName.Replace("Scene", "");
            var url = $"res://Scenes//{targetName}.tscn";
            GD.Print($"加载PackedScene,{sceneName}:{url}");
            PackedScene = ResourceLoader.Load<PackedScene>(url);
        }

    }
}
