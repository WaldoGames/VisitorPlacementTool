using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisitorPlaceMentToolInterfaces
{
    public interface IManger
    {
        public List<IGrid> Grids { get; set; }
        public List<IGroup> Groups { get; set; }

        public void PlaceAllGroups();
        public void RemoveGroup(string groupID);
        public void TryPlaceGroup(IGroup group);
        public void ChildPlaceLoop(IGroup g);
        public void PlaceLoop(IGroup g);
        
        public void AddGroup(IGroup group);
    }
}
