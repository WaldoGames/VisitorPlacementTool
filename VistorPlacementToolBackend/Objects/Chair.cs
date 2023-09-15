using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolInterfaces;

namespace VisitorPlacementToolController.Objects
{
    public class Chair : IChair
    {
        private List<IGrid> grids;
        public List<IGrid> Grids { get { return grids; } set { grids = value; } }

        public bool frontRowSeat { get; set; }
        public bool FrontRowSeat { get { return frontRowSeat; } set { frontRowSeat = value; } }

        public IPerson? cellOwner { get; set; }
        public IPerson? CellOwner { get { return cellOwner; } set { cellOwner = value; } }

        public string CellOwnerName
        {
            get { if (cellOwner == null)
                {
                    return "Hier zit nog niemand";
                }
                else
                {
                    return CellOwner.Name;
                }
                 }
        }


        public IChair? neighbourLeft { get; set; }
        public IChair? NeighbourLeft { get { return neighbourLeft; } set { neighbourLeft = value; } }

        public IChair? neighbourRight { get; set; }
        public IChair? NeighbourRight { get { return neighbourRight; } set { neighbourRight = value; } }

        public string GetGUID()
        {
            if (CellOwner != null)
            {
                return CellOwner.Id;
            }
            else
            {
                return "no CellOwner";
            }
        }

        public string GetName()
        {
            if (CellOwner != null)
            {
                return CellOwner.Name;
            }
            else
            {
                return "no CellOwner";
            }
        }

        public void SetupNeighbours(IChair? left, IChair? right)
        {
            NeighbourLeft = left;
            NeighbourRight = right;
        }

        public bool TryPlaceVisitor(IPerson person)
        {
            if (CellOwner != null)
            {
                return false;
            }
            if (person.Child == true)
            {
                /*if (childCell == true)
                {*/
                if (NeighbourLeft != null)
                {
                    if (NeighbourLeft.CellOwner != null)
                    {
                        if (NeighbourLeft.CellOwner.Parent.Id == person.Parent.Id)
                        {
                            CellOwner = person;
                            person.Placed = true;
                            return true;
                        }
                    }
                }
                if (NeighbourRight != null)
                {
                    if (NeighbourRight.CellOwner != null)
                    {
                        if (NeighbourRight.CellOwner.Parent.Id == person.Parent.Id)
                        {
                            CellOwner = person;
                            person.Placed = true;
                            return true;
                        }
                    }
                }
                //}
                /*if(childCell == false)
                {
                    return false;
                }*/
            }
            else
            {
                CellOwner = person;
                person.Placed = true;
                return true;
            }

            return false;
        }

        public bool TryRemoveVisitor()
        {
            CellOwner = null;
            return true;

        }
    }
}
