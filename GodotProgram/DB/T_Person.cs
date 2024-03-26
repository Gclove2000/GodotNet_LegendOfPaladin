using Bogus;
using GodotProgram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.DB
{
    public class T_Person : T_ModelBase, IModelFaker<T_Person>
    {

        public int Age { get; set; }
        public string Name { get; set; }

        private Faker<T_Person> faker = new Faker<T_Person>()
            .RuleFor(t => t.Id, f => f.IndexFaker)
            .RuleFor(t => t.CreateTime, f => f.Date.Between(new DateTime(2024, 1, 1), DateTime.Now))
            .RuleFor(t => t.Name, f => f.Name.FindName())
            .RuleFor(t => t.Age, f => f.Random.Int(10, 30));

        public IEnumerable<T_Person> FakeMany(int num)
        {
            return faker.Generate(num);
        }

        public T_Person FakerOne()
        {
            return faker.Generate();
        }
    }
}
