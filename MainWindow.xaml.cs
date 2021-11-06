using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BudgetTracker.Views;
using BudgetTracker.Models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Communication communication = null;
    //static public Expenses objExpenList = null;
    public partial class MainWindow : Window
    {
        Communications communication = null;
        static public Expenses objExpenList = null;
        public MainWindow()
        {
            objExpenList = new Expenses();
            communication = new Communications();
            Communications.ShowTable += UpdateTable;
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new AddFromKeyboard());
                    break;
                case 1:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new FilesPanel());
                    break;
                case 2:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new SortPanel());
                    break;
                case 3:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new DatesUSD());
                    break;
                case 5:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new FoodExpenses());
                    break;
                case 6:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new FiveSmallestPanel());
                    break;
            }
        }

        private void OpenMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuBtn.Visibility = Visibility.Collapsed;
            CloseMenuBtn.Visibility = Visibility.Visible;
        }

        private void CloseMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuBtn.Visibility = Visibility.Visible;
            CloseMenuBtn.Visibility = Visibility.Collapsed;
        }

        private void OffBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Mainwindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void UpdateTable(Expenses exObj)
        {
            ExpensesTable.Items.Clear();
            for (int i = 0; i < exObj.ExpenseList.Count; ++i)
            {
                ExpensesTable.Items.Add(exObj.ExpenseList[i]);
            }
        }

        private void UpdateTable()
        {
            ExpensesTable.Items.Clear();
            for (int i = 0; i < objExpenList.ExpenseList.Count; ++i)
            {
                ExpensesTable.Items.Add(objExpenList.ExpenseList[i]);
            }
        }
    }
}
