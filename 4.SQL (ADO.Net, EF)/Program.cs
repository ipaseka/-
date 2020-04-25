using _4.SQL__ADO.Net__EF_.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace _4.SQL__ADO.Net__EF_
{
    class Program
    {
        static void Main(string[] args)
        {
            PrepareDb();
            TestMethod();
            TestView();
        }

        private static void TestView()
        {
            using (IRepository rep = new CustomRep())
            {
                Console.WriteLine(rep.GetStatusHistoryByContract(1));
                Console.WriteLine(rep.GetContractByCustomer(3));
                Console.WriteLine(rep.GetContract("AE.1236589"));
            }
            Console.Read();
        }

        private static void TestMethod()
        {
            using (IRepository rep = new CustomRep())
            {
                var customer1 = new Customer()
                {
                    Surname = "Пасєка",
                    Name = "Ігор",
                    FatherName = "Ігорович",
                    IdentCode = "0022332266",
                    DateOfBorn = DateTime.Parse("28.09.1992"),
                    CustomerType = rep.CustomerTypeByName("Individual"),
                    DateCreate = DateTime.Now
                };
                var customer2 = new Customer()
                {
                    Surname = "Далас",
                    Name = "Лілу",
                    IdentCode = "4569853255",
                    DateOfBorn = DateTime.Parse("01.01.1975"),
                    CustomerType = rep.CustomerTypeByName("Individual"),
                    DateCreate = DateTime.Now
                };
                var customer3 = new Customer()
                {
                    Surname = "ТОВ \"Мрія\"",
                    IdentCode = "12345678",
                    CustomerType = rep.CustomerTypeByName("Legal"),
                    DateCreate = DateTime.Now
                };
                var seller1 = new Seller() { Surname = "Перший", Name = "Продавець", FatherName = "Вікторович" };
                var seller2 = new Seller() { Surname = "Другий", Name = "Продавець", FatherName = "Олегович" };
                var seller3 = new Seller() { Surname = "Третій", Name = "Продавець", FatherName = "Сергійович" };

                var statusDraft = new StatusHistory() { DateCreate = DateTime.Now, Status = rep.ContractStatusByName("Draft") };

                var contract1 = new Contract()
                {
                    Number = "AM.1234564",
                    DateCreate = DateTime.Now,
                    DateSign = DateTime.Parse("25.04.2020"),
                    DateBegin = DateTime.Parse("25.04.2020"),
                    DateEnd = DateTime.Parse("25.12.2020"),
                    Payment = 956,
                    Customer = customer1,
                    Seller = seller1,
                    StatusHistory = { statusDraft }
                };
                var contract2 = new Contract()
                {
                    Number = "UA048.00236695",
                    DateCreate = DateTime.Now,
                    DateSign = DateTime.Parse("25.04.2020"),
                    DateBegin = DateTime.Parse("26.04.2020"),
                    DateEnd = DateTime.Parse("25.04.2021"),
                    Payment = 2036,
                    Customer = customer2,
                    Seller = seller1,
                    StatusHistory = { statusDraft }
                };
                var contract3 = new Contract()
                {
                    Number = "MB.4569665",
                    DateCreate = DateTime.Now,
                    DateSign = DateTime.Parse("01.04.2020"),
                    DateBegin = DateTime.Parse("02.04.2020"),
                    DateEnd = DateTime.Parse("25.08.2020"),
                    Payment = 236658,
                    Customer = customer3,
                    Seller = seller2,
                    StatusHistory = { statusDraft }
                };
                var contract4 = new Contract()
                {
                    Number = "AE.1236589",
                    DateCreate = DateTime.Now,
                    DateSign = DateTime.Parse("01.04.2020"),
                    DateBegin = DateTime.Parse("02.04.2020"),
                    DateEnd = DateTime.Parse("25.08.2020"),
                    Payment = 123,
                    Customer = customer3,
                    Seller = seller2,
                    StatusHistory = { statusDraft }
                };
                rep.AddContract(contract1);
                rep.AddContract(contract2);
                rep.AddContract(contract3);
                rep.AddContract(contract4);

                var statusProject = new StatusHistory() { DateCreate = DateTime.Now, Status = rep.ContractStatusByName("Project"), Contract = rep.GetContract("AE.1236589") };
                rep.AddContactStatus(statusProject);

                statusProject.Contract = rep.GetContract("MB.4569665");
                rep.AddContactStatus(statusProject);

                var statusTerminated = new StatusHistory() { DateCreate = DateTime.Now, Status = rep.ContractStatusByName("Terminated"), Contract = rep.GetContract("AE.1236589") };
                rep.AddContactStatus(statusTerminated);
            }
        }

        private static void PrepareDb()
        {
            var entities = new DotNetTestBaseEntities();
            if (!entities.CustomerType.Any())
            {
                entities.CustomerType.Add(new CustomerType() { Name = "Individual" });
                entities.CustomerType.Add(new CustomerType() { Name = "Legal" });
            }
            if (!entities.Status.Any())
            {
                entities.Status.Add(new Status() { Name = "Draft" });
                entities.Status.Add(new Status() { Name = "Project" });
                entities.Status.Add(new Status() { Name = "Terminated" });
            }
            entities.SaveChanges();
        }
    }
}
