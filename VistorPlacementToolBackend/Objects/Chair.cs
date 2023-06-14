using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisitorPlacementToolBackend.Interface;

namespace VisitorPlacementToolBackend.Objects
{
    internal class Chair : IChair
    {
		private string visitorIcon="B1-1";

		public string VisitorIcon
        {
			get { return visitorIcon; }
            set { visitorIcon = value; NotifyPropertyChanged(); }
		}

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
