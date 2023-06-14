using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisitorPlacementToolBackend.Interface;

namespace VisitorPlacementToolBackend.Objects
{
    internal class Row: IRow
    {


		private int width;

		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		private bool frontRow;

		public bool FrontRow
		{
			get { return frontRow; }
			set { frontRow = value; }
		}

		private ObservableCollection<IChair> chairs;

		public ObservableCollection<IChair> Chairs
		{
			get { return chairs; }
			set { chairs = value; }
		}

		private int rowNumber;

		public int RowNumber
        {
			get { return rowNumber; }
			set { rowNumber = value; }
		}

		public Row(int rowN)
        {
			RowNumber = rowN;
        }

        public void CreateChairs(int Width)
        {
            Chairs = new ObservableCollection<IChair>();
            for (int i = 0; i < Width; i++)
            {
                Chairs.Add(new Chair());

            }
        }
    }
}
