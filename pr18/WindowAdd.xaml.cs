using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace pr18
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        Db18Context db18Context = new();
        Tbl tbl;
        bool isEditing = false;

        public WindowAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            

            using (Db18Context _db = new())
            {
                DateOnly dateProd = DateOnly.FromDateTime(Convert.ToDateTime(dpProd.SelectedDate)),
                        dateEx = DateOnly.FromDateTime(Convert.ToDateTime(dpEx.SelectedDate));

                if (isEditing)
                {
                    int id = Data.tbl.Id;

                    int count = _db.Database.ExecuteSql($"UPDATE tbl SET NameOfL = {tbName.Text}, SumOfL = {Convert.ToInt32(tbPrice.Text)}, DateOfProd = {dateProd}, ExpDate = {dateEx}, NameOfFabric = {tbNamOfCorp.Text}, AdresOfFabric = {tbAdresOfCorp.Text} WHERE Id = {id}");
                }
                else
                {
                    int count = _db.Database.ExecuteSql($"INSERT INTO tbl (NameOfL, SumOfL, CountOfL, DateOfProd, ExpDate, NameOfFabric, AdresOfFabric) VALUES({tbName.Text}, {tbPrice.Text}, {tbCount.Text}, {dateProd}, {dateEx}, {tbNamOfCorp.Text}, {tbAdresOfCorp.Text})");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти? Данные не сохранятся", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void WindowAdd_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.tbl == null)
            {
                this.Title = "Добавление записи";
                btn.Content = "Добавить";
                tbl = new();
                isEditing = false;
            }
            else
            {
                this.Title = "Изменить запись";
                btn.Content = "Изменить";
                tbl = db18Context.Tbls.Find(Data.tbl.Id);
                isEditing = true;
                GetValuesFromTable();
            }
        }

        private void GetValuesFromTable()
        {
            TimeOnly temp = new();
            
            tbName.Text = tbl.NameOfL;
            tbPrice.Text = tbl.SumOfL.ToString();
            tbCount.Text = tbl.CountOfL.ToString();

            dpProd.SelectedDate = tbl.DateOfProd.ToDateTime(temp);
            dpEx.SelectedDate = tbl.ExpDate.ToDateTime(temp);

            tbNamOfCorp.Text = tbl.NameOfFabric;

            if (tbl.AdresOfFabric != null)
            {
                tbAdresOfCorp.Text = tbl.AdresOfFabric;
            }
        }
    }
}
