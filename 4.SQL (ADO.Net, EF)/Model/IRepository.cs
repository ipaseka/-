using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.SQL__ADO.Net__EF_.Model
{
    interface IRepository: IDisposable
    {
        List<Contract> GetContract();
        Contract GetContract(int id);
        Contract GetContract(string number);
        List<Contract> GetContractByCustomer(int id);
        List<StatusView> GetStatusHistoryByContract(int id);
        void AddContract(Contract contract);
        void AddContactStatus(StatusHistory status);
        Status ContractStatusByName(string name);
        CustomerType CustomerTypeByName(string name);
    }
}
