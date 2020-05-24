using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string patern = @"(.+)\s\[(.+)\].+-\s([\w]+)[^\w]*([\w]+)[^{]+(.+)";
            string str = "2020-05-20 20:14:41,725 [8] INFO  ApiPZU.Logging.LogManager - WriteToDb, result: {\"ContractId\":8708381,\"ErrorMessage\":\"\"}";
            var res =  Regex.Match(str, patern, RegexOptions.IgnoreCase);
            Console.WriteLine(res);
        }
    }
}
