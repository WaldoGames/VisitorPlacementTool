using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPlaceMentToolInterfaces
{
    public interface IChair
    {
        public bool FrontRowSeat { get; set; }
        public IPerson? CellOwner { get; set; }

        public string CellOwnerName { get; }
        public IChair? NeighbourLeft { get; set; }
        public IChair? NeighbourRight { get; set; }
        public string GetGUID();
        public string GetName();
        public void SetupNeighbours(IChair? left, IChair? right);
        public bool TryPlaceVisitor(IPerson person);
        public bool TryRemoveVisitor();

            
            }

}
