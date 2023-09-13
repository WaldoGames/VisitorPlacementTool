using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;

namespace VisitorPlaceMentToolInterfaces
{
    public interface IGroup
    {
        public List<IPerson> People { get; set; }
        public string Id { get; set; }
        public bool ContainsChild { get; }
        public int UnplacedChildCount { get; }
        public int UnplacedCount { get; }

        public List<IPerson>? GetChildGroup { get; }
        public GroupState GroupState { get; set; }
        public void GenerateID();
    
    }
}
