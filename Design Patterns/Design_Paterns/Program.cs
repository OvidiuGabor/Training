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

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var 
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
