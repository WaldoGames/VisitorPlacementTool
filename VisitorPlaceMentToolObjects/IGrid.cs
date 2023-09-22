using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlaceMentToolInterfaces
{
    public interface IGrid
    {
        public int RowLength { get; set; }
        public int CollumLength { get; set; }
        public bool Open { get;}

        public string GridName { get; set; }
        public IChair[,] Chairs { get; set; }

        public void CreateCells(int rows, int collums);
        public void SetNeibours();
        public void TryPlaceUnplacedGroupAll(IGroup group);
        public void TryPlaceUnplacedChildGroupAll(IGroup group);



        public bool TryPlaceGrid(IPerson p, bool FirstRowOnly = true);
        public void RemoveGroup(string guid);

    }
}
