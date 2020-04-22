using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.ATM_Info.Core.Model
{
    class IdentityCard
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssuedDate { get; set; }
        public IdentityCardType IdentityCardType { get; set; }

        public override string ToString()
        {
            return string.Concat(Series, Number, " ", IssuedBy, "(", IssuedDate.ToShortDateString(), ")");
        }
    }
}
