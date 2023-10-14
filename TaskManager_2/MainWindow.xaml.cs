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
using TaskManager_2.DB_data;

namespace TaskManager_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (MainContext db = new MainContext())
            {
                var tasks = db.Tasks.ToList();
                toDoTasks.ItemsSource = tasks;
            }
        }

        private void testButton_Click_1(object sender, RoutedEventArgs e)
        {
            using (MainContext db = new MainContext())
            {
                DB_data.Task task = new DB_data.Task();
                db.Tasks.Add(task);
            }
        }
        private void Lb1_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listBoxItem)
            {
                toDoTasks.Items.Add(listBoxItem);
            }
        }
        private void Lb2_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listBoxItem)
            {
                wrkInPrgsTasks.Items.Add(listBoxItem);
            }
        }

        private void Lb1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void Lb2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void toDoTasks_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point Pos = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(Pos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(Pos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selected = (ListBoxItem)toDoTasks.SelectedItems;
                    toDoTasks.Items.Remove(selected);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selected), DragDropEffects.Copy);
                    if (selected.Parent == null)
                    {
                        toDoTasks.Items.Add(selected);
                    }
                }
                catch { }
            }
        }
    }
}
