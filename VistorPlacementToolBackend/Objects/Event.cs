using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisitorPlacementToolBackend.Interface;

namespace VisitorPlacementToolBackend.Objects
{
    public class Event : IEvent
    {
        public Event()
        {
            Sections = new ObservableCollection<ISection>();
        }

        private int maxVisitors;
        public int MaxVisitors
        {
            get { return maxVisitors; }
            set { maxVisitors = value; }
        }

        private ObservableCollection<ISection> sections;

        public ObservableCollection<ISection> Sections
        {
            get { return sections; }
            set { sections = value; }
        }

        public void CreateSection(int Width, int Height)
        {
            Section s = new Section();

            s.CreateRows(Width, Height);

            Sections.Add(s);
        }

    }
}
