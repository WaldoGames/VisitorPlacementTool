using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;
using VisitorPlaceMentToolInterfaces;

namespace VisitorPlacementToolController.Objects
{
    public class Group : IGroup
    {

        private List<IPerson> people;
        public List<IPerson> People { get { return people; } set { people = value; } }

        private GroupState groupState { get; set; }
        public GroupState GroupState { get { return groupState; } set { groupState = value; } }


        private string id;
        public string Id { get { return id; } set { id = value; } }

        public Group()
        {
            GenerateID();
            GroupState = GroupState.Unplaced;
            people = new List<IPerson>();
        }

        public bool ContainsChild { 
            get {
                if (People.Where(p => p.Child == true).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }           
            } }

        public int UnplacedChildCount
        {
            get
            {
                return People.Where(p => p.Child == true && p.Placed == false && p.Cancelled == false).Count();
            }
        }
        public int UnplacedCount
        {
            get
            {
                return People.Where(p => p.Placed == false && p.Cancelled == false).Count();
            }
        }

        public List<IPerson>? GetChildGroup
        {
            get
            {
                List<IPerson> ChildGroup = new List<IPerson>();

                IPerson? adult = people.Where(p => p.Child == false && p.Placed == false && p.Cancelled == false).FirstOrDefault();
                if (adult == null)
                {
                    return null;
                }
                ChildGroup.Add(adult);
                ChildGroup.AddRange(people.Where(p => p.Child == true && p.Cancelled == false && p.Placed == false));

                return ChildGroup;
            }
        }


        public void GenerateID()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
