using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlacementToolBackend.Interface
{
    public interface ISection
    {
        public int Height { get; set; }
        public string SectionSymbol { get; set; }

        public ObservableCollection<IRow> Rows { get; set; }

        public void CreateRows(int Width, int Heigth);
    }
}
