using Autofac;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }


    public class SingletonDatabase : IDatabase
    {

        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;
        private SingletonDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initialising database");
            capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(list => list.ElementAt(0).Trim(), list=> int.Parse(list.ElementAt(1)));


        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

       



        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }
    public class SingletonrecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);

            return result;
        }
    }


    public class ConfigRecordFinder
    {
        private IDatabase database;
        public ConfigRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += database.GetPopulation(name);

            return result;
        }

    }


    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 4,
                ["gamma"] = 2
            }[name];
        }
    }


    public class OrinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private OrinaryDatabase()
        {
            Console.WriteLine("Initialising database");
            capitals = File.ReadAllLines(Path.Combine(
                new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName ,"capitals.txt")
                )
                .Batch(2).
                ToDictionary(list => list.ElementAt(0).Trim(), list => int.Parse(list.ElementAt(1)));


        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));


            

        }
        [Test]
        public void SingletonPopulationTest()
        {
            var record = new SingletonrecordFinder();
            var name = new[] { "Manila", "Mexico city" };
            int tp = record.GetTotalPopulation(name);
            Assert.That(tp, Is.EqualTo(1750000 + 17400000));
        }
        [Test]
        public void GetConfigTotalPopulation()
        {
            var db = new DummyDatabase();
            var rf = new ConfigRecordFinder(db);
            var name = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(name);
            Assert.That(tp, Is.EqualTo(3));
        }
        [Test]
        public void DIPopulation()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigRecordFinder>();
            using(var c = cb.Build())
            {
                var rf = c.Resolve<ConfigRecordFinder>();
            }
        }
    }


    class Program
    {


        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            string city = "Manila";
            Console.WriteLine($"{city} has population { db.GetPopulation(city)}");
            Console.ReadLine();

        }
    }
}
