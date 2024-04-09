using GodotNet_LegendOfPaladin2.DB;
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
            printHelper.Info("主函数加载成功！");
            printHelper.Debug("插入数据库测试");
            var lists = T_User.Faker.Generate(10);
            var num = freeSqlHelper.SqliteDb.Insert(lists).ExecuteAffrows();
            printHelper.Debug($"影响{num}行数据");

            var list = freeSqlHelper.SqliteDb.Queryable<T_User>()
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToList();
            printHelper.Debug($"查询数据库");
            foreach (var item in list)
            {
                printHelper.Debug(JsonConvert.SerializeObject(item));
            }
        }
    }
}
