using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;
using VisitorPlaceMentToolInterfaces;

namespace VisitorPlacementToolController.Objects
{
    public class Event : IEvent
    {
        private List<IGrid> grids;
        public List<IGrid> Grids { get { return grids; } set { grids = value; } }

        private List<IGroup> groups;
        public List<IGroup> Groups { get { return groups; } set { groups = value; } }

        public List<IGroup> RejectedGroups {get { return groups.Where(g => g.GroupState == GroupState.Rejected_Not_Enough_Seats || g.GroupState == GroupState.Rejected_Not_enough_Seats_Child || g.GroupState == GroupState.Not_Enough_Adults || g.GroupState == GroupState.Children_only_group).ToList(); } }

        public void CreateGrid(int width, int height)
        {
            if(width > 10)
            {
                width = 10;
            }if(height > 3)
            {
                height = 3;
            }

            Grid tmp = new Grid(width, height);
            AddGrid(tmp);
        }
        public void NameGrid(IGrid grid)
        {
            string[] alphabet = new string[] {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z"
            };
            if (Grids.Count > 25)
            {
                int DoubleDigit = (int)Math.Floor((decimal)Grids.Count / 26) - 1;
                int SingleDigit = Grids.Count % 26;
                grid.GridName = alphabet[DoubleDigit] + alphabet[SingleDigit];
            }
            else
            {
                grid.GridName = alphabet[Grids.Count];
            }
        }
        public void AddGrid(IGrid grid)
        {
            if (Grids == null)
            {
                Grids = new List<IGrid>();
            }
            NameGrid(grid);
            Grids.Add(grid);
        }
        public void AddGroup(IGroup group)
        {
            if(Groups == null)
            {
                Groups = new List<IGroup> { group };
            }
            else
            {
                Groups.Add(group);
            }
        }

        public void ChildPlaceLoop(IGroup g)
        {
            foreach (IGrid item in grids)
            {
                if (g.UnplacedChildCount > 0)
                {

                    item.TryPlaceUnplacedChildGroupAll(g);

                }
            }
            
            if (g.UnplacedChildCount > 0)
            {
                //rejected because there is no place for kids left.
                g.GroupState = GroupState.Rejected_Not_enough_Seats_Child;
                RemoveGroup(g.Id);
                //remove other group member because rejected?
            }
        }



        public void PlaceAllGroups()
        {
            foreach (IGroup item in groups)
            {
                TryPlaceGroup(item);
            }
        }

        public void PlaceLoop(IGroup g)
        {
            foreach (IGrid item in grids)
            {
                if (g.UnplacedCount > 0)
                {
                    item.TryPlaceUnplacedGroupAll(g);
                }
            }

            if (g.UnplacedCount > 0)
            {
                 g.GroupState = GroupState.Rejected_Not_Enough_Seats;
                 RemoveGroup(g.Id);
            }

        }

        public void RemoveGroup(string groupID)
        {
            foreach(IGrid g in Grids)
            {
                foreach (IChair item in g.Chairs)
                {
                    if (item.CellOwner != null)
                    {
                        if (item.CellOwner.Id == groupID)
                        {
                            item.CellOwner.PersonState = PersonState.Unplaced;
                            item.CellOwner.Placed = false;
                            item.CellOwner = null;
                        }
                    }
                }
            }
        }

        public void TryPlaceGroup(IGroup group)
        {
            if(group.UnplacedCount == group.UnplacedChildCount)
            {
                group.GroupState = GroupState.Children_only_group;
                return;
            }

            ChildPlaceLoop(group);
            if (group.GroupState != GroupState.Rejected_Not_enough_Seats_Child)
            {
                PlaceLoop(group);
            } 

        }
    }
}
