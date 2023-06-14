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
    internal class Section: ISection
    {
		public Section() {
			Guid test = Guid.NewGuid();
			string tests = test.ToString();
			int i = 1;
		}

		private int height;

		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		private string sectionSymbol;

		public string SectionSymbol
        {
			get { return sectionSymbol; }
			set { sectionSymbol = value; }
		}

		private ObservableCollection<IRow> rows;

		public ObservableCollection<IRow> Rows
		{
			get { return rows; }
			set { rows = value; }
		}

        public void CreateRows(int Width, int Heigth)
        {
            Rows = new ObservableCollection<IRow>();

			for (int i = 0; i < Heigth; i++)
			{
				Row r = new Row(i+1);

				r.CreateChairs(Width);

                Rows.Add(r);
            }
        }

    }
}
