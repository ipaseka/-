using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Reflection__Serialize__Deserialize_
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass tc1 = TestClass.FromData(11, "dada", 12.36m, DateTime.UtcNow);
            tc1.Dump();
            string tc1Str = tc1.ToSerializedString();
            tc1Str.Dump();
            TestClass tc2 = tc1Str.AsDeserializedString<TestClass>();
            tc2.Dump();
        }
    }
}
