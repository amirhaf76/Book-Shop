


Console.WriteLine("fdsfadsfasdfasdf");
MyNameSpace.Calculator c = (int [] x) => new List<int>(x).Sum();
MyNameSpace.B o = new MyNameSpace.B();  

c += (int[] x) => (from e in x select e *5).Sum();
var t = typeof(MyNameSpace.C).GetProperties();
Console.WriteLine(t.First());
Console.WriteLine(c.GetType().AssemblyQualifiedName);

namespace MyNameSpace
{
    public delegate int Calculator(params int[] nums);
    public class C
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected internal int z;
    }

    public class B: C {
        }
    
}

