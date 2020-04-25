using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.SQL__ADO.Net__EF_.Model
{
    class CustomRep : IRepository
    {
        private readonly DotNetTestBaseEntities db;

        public CustomRep() =>
            db = new DotNetTestBaseEntities();
        public void AddContactStatus(StatusHistory status)
        {
            db.StatusHistory.Add(status);
            db.SaveChanges();
        }

        public void AddContract(Contract contract)
        {
            db.Contract.Add(contract);
            db.SaveChanges();
        }
        public List<Contract> GetContract() =>
            db.Contract.ToList();
        public Contract GetContract(int id) =>
            db.Contract.Single(x => x.Id == id);
        public Contract GetContract(string number) =>
            db.Contract.Single(x => x.Number == number);
        public List<Contract> GetContractByCustomer(int id) =>
            db.Customer.Join(db.Contract, x => x.Id, y => y.CustomerId, (x, y) => y).ToList();
        public List<StatusView> GetStatusHistoryByContract(int id) =>
            db.StatusHistory.Where(x => x.ContractId == id)
            .Join(db.Status, x => x.StatusId, y => y.Id, (x, y) => new StatusView() { Id = x.StatusId, Date = x.DateCreate, Name = y.Name })
            .ToList();
        public Status ContractStatusByName(string name) => 
            db.Status.Single(x => x.Name == name);
        public CustomerType CustomerTypeByName(string name) => 
            db.CustomerType.Single(x => x.Name == name);
        public void Dispose() => db?.Dispose();

    }
}
