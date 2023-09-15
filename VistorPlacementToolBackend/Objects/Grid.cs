using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;
using VisitorPlaceMentToolInterfaces;

namespace VisitorPlacementToolController.Objects
{
    public class Grid : IGrid
    {
        private int rows;

        public int RowLength
        {
            get { return rows; }
            set { rows = value; }
        }
        private int collums;

        public int CollumLength
        {
            get { return collums; }
            set { collums = value; }
        }

        private bool open;

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }
        private IChair[,] chairs;

        public IChair[,] Chairs
        {
            get { return chairs; }
            set { chairs = value; }
        }

        public Grid(int rows, int collums)
        {
            this.RowLength = rows;
            this.CollumLength = collums;

            Chairs = new Chair[RowLength, CollumLength];

            for (int x = 0; x < RowLength; x++)
            {
                for (int y = 0; y < CollumLength; y++)
                {
                    
                    Chairs[x, y] = new Chair();
                    if(x == 0)
                    {
                        Chairs[x,y].FrontRowSeat = true;
                    }
                    
                }
            }
            SetNeibours();            
        }
        public void CreateCells(int rows, int collums)
        {
            RowLength = rows;
            CollumLength = collums;
            Chairs = new IChair[RowLength, CollumLength];

            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < CollumLength; j++)
                {
                    Chairs[i, j] = new Chair();
                }
            }

            SetNeibours();
        }

        public void RemoveGroup(string guid)
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < CollumLength; j++)
                {
                    if (Chairs[i,j].GetGUID() == guid)
                    {
                        Chairs[i, j].TryRemoveVisitor();
                    }
                }
            }
        }

        public void SetNeibours()
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < collums; j++)
                {
                    if (i == 0)
                    {
                        chairs[i, j].SetupNeighbours(null, chairs[i + 1, j]);
                    }
                    else if (i == RowLength - 1)
                    {
                        chairs[i, j].SetupNeighbours(chairs[i - 1, j], null);
                    }
                    else
                    {
                        chairs[i, j].SetupNeighbours(chairs[i - 1, j], chairs[i + 1, j]);
                    }
                }
            }
        }

        public bool TryPlaceGrid(IPerson p, bool FirstRowOnly = false)
        {
            if (!FirstRowOnly)
            {
                for (int j = 1; j < collums; j++)
                {
                    for (int i = 0; i < RowLength; i++)
                    {

                        if (chairs[i, j].TryPlaceVisitor(p))
                        {
                            return true;
                        }

                    }
                }
            }
            for (int j = 0; j < RowLength; j++)
            {
                if (Chairs[j, 0].TryPlaceVisitor(p))
                {
                    return true;

                }

            }
            return false;
        }

        public void TryPlaceUnplacedChildGroupAll(IGroup group)
        {
            if(group.UnplacedChildCount <= 0)
            {
                return;
            }
            List<IPerson>? childgroup = group.GetChildGroup;
            if (childgroup != null)
            {
                childgroup = childgroup.OrderBy(p => p.Child).ToList();
                foreach (IPerson p in childgroup)
                {
                    bool tmp = TryPlaceGrid(p, true);
                }

                if (group.UnplacedCount <= 0)
                {
                    group.GroupState = GroupState.Unplaced;
                }
                else if (group.UnplacedCount > 0)
                {
                    group.GroupState = GroupState.PatialPlaced;
                }
                else
                {
                    group.GroupState = GroupState.Placed;
                }
            }
            else
            {
                group.GroupState = GroupState.Not_Enough_Adults;
            }
        }

        

        public void TryPlaceUnplacedGroupAll(IGroup group)
        {
            foreach (IPerson p in group.People.Where(p => p.Child == false && p.Placed == false))
            {
                bool tmp = TryPlaceGrid(p);
            }

            if (group.UnplacedCount == group.People.Count)
            {
                group.GroupState = GroupState.Unplaced;
            }
            else if (group.UnplacedCount > 0)
            {
                group.GroupState = GroupState.PatialPlaced;
            }
            else
            {
                group.GroupState = GroupState.Placed;
            }

        }
    }
    
}
