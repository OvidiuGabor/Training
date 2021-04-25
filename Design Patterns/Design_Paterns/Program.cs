using Autofac;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Singleton
//Monostate
//Singleton-Thread
//AmbientContext
namespace Singleton
{
    #region Singleton
    //public interface IDatabase
    //{
    //    int GetPopulation(string name);
    //}
    //public class SingletonDatabase : IDatabase
    //{

    //    private Dictionary<string, int> capitals;
    //    private static int instanceCount;
    //    public static int Count => instanceCount;
    //    private SingletonDatabase()
    //    {
    //        instanceCount++;
    //        Console.WriteLine("Initialising database");
    //        capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(list => list.ElementAt(0).Trim(), list=> int.Parse(list.ElementAt(1)));


    //    }
    //    public int GetPopulation(string name)
    //    {
    //        return capitals[name];
    //    }





    //    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    //    public static SingletonDatabase Instance => instance.Value;
    //}
    //public class SingletonrecordFinder
    //{
    //    public int GetTotalPopulation(IEnumerable<string> names)
    //    {
    //        int result = 0;
    //        foreach (var name in names)
    //            result += SingletonDatabase.Instance.GetPopulation(name);

    //        return result;
    //    }
    //}
    //public class ConfigRecordFinder
    //{
    //    private IDatabase database;
    //    public ConfigRecordFinder(IDatabase database)
    //    {
    //        this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
    //    }

    //    public int GetTotalPopulation(IEnumerable<string> names)
    //    {
    //        int result = 0;
    //        foreach (var name in names)
    //            result += database.GetPopulation(name);

    //        return result;
    //    }

    //}
    //public class DummyDatabase : IDatabase
    //{
    //    public int GetPopulation(string name)
    //    {
    //        return new Dictionary<string, int>
    //        {
    //            ["alpha"] = 1,
    //            ["beta"] = 4,
    //            ["gamma"] = 2
    //        }[name];
    //    }
    //}
    //public class OrinaryDatabase : IDatabase
    //{
    //    private Dictionary<string, int> capitals;
    //    private OrinaryDatabase()
    //    {
    //        Console.WriteLine("Initialising database");
    //        capitals = File.ReadAllLines(Path.Combine(
    //            new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName ,"capitals.txt")
    //            )
    //            .Batch(2).
    //            ToDictionary(list => list.ElementAt(0).Trim(), list => int.Parse(list.ElementAt(1)));


    //    }
    //    public int GetPopulation(string name)
    //    {
    //        return capitals[name];
    //    }
    //}
    #endregion
    #region Tests
    [TestFixture]
    //public class SingletonTests
    //{
    //    [Test]
    //    public void IsSingletonTest()
    //    {
    //        var db = SingletonDatabase.Instance;
    //        var db2 = SingletonDatabase.Instance;
    //        Assert.That(db, Is.SameAs(db2));
    //        Assert.That(SingletonDatabase.Count, Is.EqualTo(1));


            

    //    }
    //    [Test]
    //    public void SingletonPopulationTest()
    //    {
    //        var record = new SingletonrecordFinder();
    //        var name = new[] { "Manila", "Mexico city" };
    //        int tp = record.GetTotalPopulation(name);
    //        Assert.That(tp, Is.EqualTo(1750000 + 17400000));
    //    }
    //    [Test]
    //    public void GetConfigTotalPopulation()
    //    {
    //        var db = new DummyDatabase();
    //        var rf = new ConfigRecordFinder(db);
    //        var name = new[] { "alpha", "gamma" };
    //        int tp = rf.GetTotalPopulation(name);
    //        Assert.That(tp, Is.EqualTo(3));
    //    }
    //    [Test]
    //    public void DIPopulation()
    //    {
    //        var cb = new ContainerBuilder();
    //        cb.RegisterType<OrinaryDatabase>()
    //            .As<IDatabase>()
    //            .SingleInstance();
    //        cb.RegisterType<ConfigRecordFinder>();
    //        using(var c = cb.Build())
    //        {
    //            var rf = c.Resolve<ConfigRecordFinder>();
    //        }
    //    }
    //}
    #endregion  


    //AmbientContext

    public class Builing
    {
       public List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var wall in Walls)
                sb.AppendLine(wall.ToString());

            return sb.ToString();
          
        }
    }

    public struct Point
    {
        private int X, Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}"; 
        }
    }
    public class Wall
    {
        public Point Start, End;
        public int Height;
        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return ("Start: " + Start.ToString() + ", End: " + End.ToString() + ", Height: " + Height);

        }

    }
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight;
        private static Stack<BuildingContext> stack = new Stack<BuildingContext>();

        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int height)
        {
            WallHeight = height;
            stack.Push(this);
        }
        public static BuildingContext Current => stack.Peek();
        public void Dispose()
        {
            if (stack.Count > 1)
                stack.Pop();
        }
    }
    class Program
    {
        //public class CEO
        //{
        //    private static string name;
        //    private static int age;

        //    public string Name
        //    {
        //        get => name;
        //        set => name = value;
        //    }

        //    public int Age
        //    {
        //        get => age;
        //        set => age = value;
        //    }

        //    public override string ToString()
        //    {
        //        return $"{nameof(Name)}: {Name}, {nameof(Age)} : {Age}";
        //    }

        //}

        static void Main(string[] args)
        {
            //    var db = SingletonDatabase.Instance;
            //    string city = "Manila";
            //    Console.WriteLine($"{city} has population { db.GetPopulation(city)}");
            //    Console.ReadLine();

            //var ceo = new CEO();
            //ceo.Name = "Adam Smith";
            //ceo.Age = 55;


            //var ceo2 = new CEO();
            //Console.WriteLine(ceo2.ToString());
            //Console.ReadLine();

            //var t1 = Task.Factory.StartNew(() => {
            //    Console.WriteLine("T1:" + PerThreadSingleton.Instance.id);
            //});

            //var t2 = Task.Factory.StartNew(() => {
            //    Console.WriteLine("T2:" + PerThreadSingleton.Instance.id);
            //    Console.WriteLine("T2:" + PerThreadSingleton.Instance.id);
            //});

            //Task.WaitAll(t1, t2);
            //Console.Read();

            //AmbientContext
            #region Ambient Context
            var house = new Builing();

            using (new BuildingContext(3000))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
            }



            using (new BuildingContext(3500))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 5000)));
            }

            using(new BuildingContext(3000))
            {
                house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
            }

            Console.WriteLine(house);
            Console.ReadLine();
            #endregion

        }



        //public sealed class PerThreadSingleton
        //{
        //    private static ThreadLocal<PerThreadSingleton> threadInstance = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());
        //    public int id;

        //    private PerThreadSingleton()
        //    {
        //        id = Thread.CurrentThread.ManagedThreadId;
        //    }

        //    public static PerThreadSingleton Instance => threadInstance.Value;
        //}

    }
}
