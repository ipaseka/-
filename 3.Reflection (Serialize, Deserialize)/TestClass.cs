using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Reflection__Serialize__Deserialize_
{
    class TestClass
    {
        private int privateField = 32;
        public int publicField;
        public string Strr { get; set; }
        public decimal Dec { get; set; }
        public DateTime Date { get; set; }

        public override string ToString() =>
            $"publicField = {publicField}, privateField = {privateField}, Strr = {Strr}, Dec = {Dec}, Date = {Date}";
        public static TestClass FromData( int i, string s, decimal d, DateTime date)
        {
            return new TestClass()
            {
                publicField = i,
                Strr = s,
                Dec = d,
                Date = date
            };
        }
    }
}
