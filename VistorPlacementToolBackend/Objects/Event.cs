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

        public Event()
        {

        }

        public void CreateGrid(int width, int height)
        {
            Grid tmp = new Grid(width, height);
            AddGrid(tmp);
        }
        public void AddGrid(IGrid grid)
        {
            if(Grids == null)
            {
                Grids = new List<IGrid>();
            }
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
            foreach (IGrid item in grids.Where(p => p.Open == true))
            {
                if (g.UnplacedChildCount > 0)
                {

                    item.TryPlaceUnplacedChildGroupAll(g);
                }
            }
            bool done = false;
            while (!done) {
                if (g.UnplacedChildCount > 0)
                {


                    if (grids.Where(grid => grid.Open == false).Count() > 0)
                    {
                        grids.Where(grid => grid.Open == false).FirstOrDefault().Open = true;
                        ChildPlaceLoop(g);
                       
                    }
                    else
                    {
                            //rejected because there is no place for kids left.
                        g.GroupState = GroupState.Rejected_Not_enough_Seats_Child;
                        RemoveGroup(g.Id);
                        done = true;
                        //remove other group member because rejected?
                    }
                }
                else
                {
                    done = true;
                }
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
            foreach (IGrid item in grids.Where(p => p.Open == true))
            {
                if (g.UnplacedCount > 0)
                {
                    item.TryPlaceUnplacedGroupAll(g);
                }
            }
            bool done = false;
            while (!done)
            {
                if (g.UnplacedCount > 0)
                {
                    if (grids.Where(grid => grid.Open == false).Count() > 0)
                    {
                        grids.Where(grid => grid.Open == false).FirstOrDefault().Open = true;
                        PlaceLoop(g);
                    }
                    else
                    {
                        //rejected because there is no place for all guests.
                        g.GroupState = GroupState.Rejected_Not_Enough_Seats;
                        RemoveGroup(g.Id);
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
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
