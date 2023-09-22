using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisitorPlacementToolBackend;
using VisitorPlaceMentToolInterfaces;
using VisitorPlacementTool.TestData;

using VisitorPlacementToolController.Objects;
using Grid = VisitorPlacementToolController.Objects.Grid;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Event = VisitorPlacementToolController.Objects.Event;
using System.Collections.ObjectModel;

namespace VisitorPlacementTool 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IGrid selectedGrid;

        public IGrid SelectedGrid
        {
            get { return selectedGrid; }
            set { selectedGrid = value; }
        }


        private IChair selectedChair;

        public IChair SelectedChair
        {
            get { return selectedChair; }
            set { selectedChair = value; NotifyPropertyChanged(); }
        }

        IEvent @event = new Event();

        public IEvent Event
        {
            get { return @event; }
            set { @event = value; }
        }

        private ObservableCollection<IGroup> rejected;

        public ObservableCollection<IGroup> Rejected
        {
            get { return rejected; }
            set { rejected = value; }
        }




        public MainWindow()
        {
            Random r = new Random();
            InitializeComponent();
            DataContext = this;

            for (int i = 0; i < 7; i++)
            {
                @event.CreateGrid(10, 3);
            }
           /* @event.CreateGrid(4, 4);
            @event.CreateGrid(3, 3);
            @event.CreateGrid(10, 3);*/
            for (int i = 0; i < 1500; i++)
            {
                @event.AddGroup(GenerateGroupTest(r.Next(1,11)));
            }
            @event.PlaceAllGroups();
            Rejected = new ObservableCollection<IGroup>(@event.RejectedGroups);
           
            
        }
        public void TestPrint(string tmp)
        {
            MessageBox.Show(tmp);
        }
        private void ButtonCreatedByCode_Click(object sender, RoutedEventArgs e, int i, int j)
        {

            SelectedChair = SelectedGrid.Chairs[i, j];        
        }
        public void Test(IGrid g)
        {
            #region test code
            /*DataTable dt = new DataTable();
            int nbColumns = g.RowLength;
            int nbRows = g.CollumLength;
            for (int i = 0; i < nbColumns; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(IChair));
            }

            for (int row = 0; row < nbRows; row++)
            {
                DataRow dr = dt.NewRow();
                
                for (int col = 0; col < nbColumns; col++)
                {
                    dr[col] = g.Chairs[col, row];
                }
                dt.Rows.Add(dr);
            }

            myDataGrid.ItemsSource = new arrayDataView*/
            #endregion
            Main_Box.Children.Clear();

            for (int i = 0; i < g.CollumLength; i++)
            {
                StackPanel p = new StackPanel();
                p.Orientation = Orientation.Horizontal;
                
                for (int j = 0; j<g.RowLength; j++)
                {
                    Button b = new Button();
                    
                    b.Content = g.GridName+" "+ i+"-"+j;
                    
                    b.Width = 60;
                    b.Height = 30;
                    int tmp = i;
                    int tmp2 = j;
                    
                    b.Click += (sender, e) => { ButtonCreatedByCode_Click(sender, e, tmp2, tmp) ; };
                    p.Children.Add(b);
                }
                Main_Box.Children.Add(p);
                
            }
        }

        private void RegenrateGrid(object sender, SelectionChangedEventArgs e)
        {
            Test(selectedGrid);
        }

        public IGroup GenerateGroupTest(int groupSize)
        {
            IGroup g = new Group();
            for (int i = 0; i < groupSize; i++)
            {
                IPerson p = new Person(g);
                p.Name = NameGenerator.GetName();
                
                var randomTest = new Random();

                DateTime startDate = new DateTime(1950, 1,1);
                DateTime endDate = new DateTime(2020, 1, 1);
                

                TimeSpan timeSpan = endDate - startDate;
                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = startDate + newSpan;
                p.BirthDay = newDate;
                if(randomTest.Next(0,100) > 97)
                {
                    p.Cancelled = true;
                }
                g.People.Add(p);
            }
            return g;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
