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
            //在Ready中测试IOC
            //testService.Test();
            //testUtils.Test();
            //以匿名对象为例

            //var stu = new MyStudent().FakeMany(10).ToList();

            //stu.ForEach(item =>
            //{
            //    nlogHelper.Debug(JsonConvert.SerializeObject(item));

            //});

            //以构造器为例

            var isConnect = freeSqlHelper.SqliteDb.Ado.ExecuteConnectTest(10);
            GD.Print($"数据库连接状态:[{isConnect}]");

            var insertLists = new T_Person().FakeMany(10);


            var insertName = freeSqlHelper.SqliteDb.Insert(insertLists).ExecuteAffrows();
            GD.Print($"数据库插入[{insertName}]行数据");
            GD.Print("数据库查询");
            var selectLists = freeSqlHelper.SqliteDb.Queryable<T_Person>()
                .OrderByDescending(t => t.Id)
                .Take(10)
                .ToList();
            foreach (var item in selectLists)
            {
                GD.Print(JsonConvert.SerializeObject(item));
            }
        }
    }
}
