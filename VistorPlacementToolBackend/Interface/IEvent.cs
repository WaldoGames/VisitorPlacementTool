using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlacementToolBackend.Interface
{
    public interface IEvent
    {
        public int MaxVisitors { get; set; }
        public ObservableCollection<ISection> Sections { get; set; }

        public void CreateSection(int Width, int Height);
    }
}
