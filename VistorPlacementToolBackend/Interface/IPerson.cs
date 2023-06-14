using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlacementToolBackend.Interface
{
    interface IPerson
    {

        public bool IsAdult { get; }
        public DateTime Birthday { get; set; }
        public bool Afgemeld { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Id { get; set; }
    }
}
