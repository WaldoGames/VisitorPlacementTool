using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlacementToolBackend.Interface
{
    public interface IChair: INotifyPropertyChanged 
    {
        public string VisitorIcon { get; set; }

        //public IPerson Person { get; set; }

        //bool PlaceVisitor(IPerson person);
    }
}
