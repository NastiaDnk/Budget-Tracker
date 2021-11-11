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
            MoveCursorMenu(index);
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
                case 4:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new TimespanPanel());
                    break;
                case 5:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new FoodExpenses());
                    break;
                case 6:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new FiveSmallestPanel());
                    break;
                case 7:
                    ChangeActions.Children.Clear();
                    ChangeActions.Children.Add(new RateChanges());
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 0 + (80 * index), 0, 0); // move left menu line
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
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void UpdateTable()
        {
            ExpensesTable.Items.Clear();
            for (int i = 0; i < objExpenList.ExpenseList.Count; ++i)
            {
                ExpensesTable.Items.Add(objExpenList.ExpenseList[i]);
            }
        }

        private void Check()
        {
            if (objExpenList.ExpenseList.Count == 0)
            {
                throw new Exception("You haven't entered any expenses yet.\nYou can do it by yourself in " +
                    "\"Input your data\" section\nor read data from file in section \"Files\".");
            }
        }

        private void SortType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                objExpenList.SortType();//Sorting by type
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortSubtype_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                objExpenList.SortSubtype();//Sorting by subtype
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                objExpenList.SortDate();//Sorting by date
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortCurrency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                objExpenList.SortCurrency();//Sorting by currency
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortSum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                objExpenList.SortSum();//Sorting by sum
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Check();
                if (MessageBox.Show(
                        String.Format($"Are you sure to delete all the expenses from your list? You cannot turn it back"),
                        "Delete expenses",
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question) == MessageBoxResult.Yes) // ask user if he is sure about deleting
                    objExpenList.Clear();//Clear the list
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                            "The program \"Budget tracker\" is created to hepl people:\n" +
                            "   ● Store their expenses conveniently\n" +
                            "   ● Sort the expenses by different criterion\n" +
                            "   ● Find smallest and largest expenses\n" +
                            "   ● Track exepenses made on a particular day\n" +
                            "   ● Track exepenses of a particulat type\n\n" +
                            "The program \"Budget tracker\" is created by Anastasiia Danko",
                            "About",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information); // information about program
        }

        private void ExpensesTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
