using System;
using System.Windows;
using System.Windows.Controls;
using BudgetTracker.Models;

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for SortPanel.xaml
    /// </summary>
    public partial class SortPanel : UserControl
    {
        Communications communication = new Communications();
        public SortPanel()
        {
            InitializeComponent();
        }
        private void Check() //Function to check if there is at least on expense
        {
            if (MainWindow.objExpenList.ExpenseList.Count == 0)
            {
                throw new Exception("You haven't entered any expenses yet.\nYou can do it by yourself in " +
                    "\"Input your data\" section\nor read data from file in section \"Files\".");
            }
        }
        private void DateSortBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                MainWindow.objExpenList.SortDate(); //Sorting by date
                communication.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TypeSortBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                MainWindow.objExpenList.SortType();//Sorting by type
                communication.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SubtypeSortBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                MainWindow.objExpenList.SortSubtype();//Sorting by subtype
                communication.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CurrencySortBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                MainWindow.objExpenList.SortCurrency();//Sorting by currency
                communication.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SumSortBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check();
                MainWindow.objExpenList.SortSum();//Sorting by sum in UAH
                communication.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
