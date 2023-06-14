using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlacementToolBackend.Interface
{
    public interface IRow
    {
        public int Width { get; set; }
        public bool FrontRow { get; set; }

        public int RowNumber { get; set; }

        public ObservableCollection<IChair> Chairs { get; set; }

        public void CreateChairs(int Width);

        //public bool PlaceVisitorInRow(IPerson person);
    }
}
