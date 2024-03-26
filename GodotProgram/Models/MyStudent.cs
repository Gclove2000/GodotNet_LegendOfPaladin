using Bogus;
using GodotProgram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.Models
{
    public class MyStudent : IModelFaker<MyStudent>
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }


        /// <summary>
        /// 构建faker构造器
        /// </summary>
        private Faker<MyStudent> faker = new Faker<MyStudent>()
            .RuleFor(t => t.Id, f => f.IndexFaker)
            .RuleFor(t => t.Name, f => f.Name.FindName())
            .RuleFor(t => t.Age, f => f.Random.Int(10, 30));

        public MyStudent FakerOne()
        {
            return faker.Generate();
        }

        public IEnumerable<MyStudent> FakeMany(int num)
        {
            return faker.Generate(num);
        }
    }
}
