using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.tbl = null;
                WindowAdd wadd = new();

                wadd.Owner = this;
                wadd.ShowDialog();

                LoadDbInDataGrid();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainDataGrid.SelectedItem != null)
                {
                    Data.tbl = (Tbl)MainDataGrid.SelectedItem;
                    WindowAdd wadd = new();

                    wadd.Owner = this;
                    wadd.ShowDialog();

                    LoadDbInDataGrid();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Tbl row = (Tbl)MainDataGrid.SelectedItem;

                    if (row != null)
                    {
                        using (Db18Context db = new())
                        {
                            db.Tbls.Remove(row);
                            db.SaveChanges();
                        }
                        LoadDbInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
            }
            else MainDataGrid.Focus();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbLogKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
            }
        }

        public void LoadDbInDataGrid()
        {
            using (Db18Context _db = new())
            {
                int selectedIndex = MainDataGrid.SelectedIndex;
                MainDataGrid.ItemsSource = _db.Tbls.ToList();

                if (selectedIndex != 1)
                {
                    if (selectedIndex == MainDataGrid.Items.Count)
                    {
                        selectedIndex--;
                    }

                    MainDataGrid.SelectedIndex = selectedIndex;
                }
                MainDataGrid.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDbInDataGrid();
        }
    }
}