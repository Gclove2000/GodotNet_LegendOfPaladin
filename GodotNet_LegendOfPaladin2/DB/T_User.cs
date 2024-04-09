using Bogus;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.DB
{
    public class T_User : T_DataBase
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public static readonly Faker<T_User> Faker = new Faker<T_User>()
            .RuleFor(t => t.Id, f => f.IndexFaker)
            .RuleFor(t => t.CreateTime, f => f.Date.Between(new DateTime(2024, 1, 1), DateTime.Now))
            .RuleFor(t => t.Name, f => f.Name.FirstName())
            .RuleFor(t => t.Age, f => f.Random.Int(10, 30));
    }
}
