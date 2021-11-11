using System;
using System.Collections.Generic;
using System.Windows;
using BudgetTracker.Models;
using System.Windows.Controls;

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for FoodExpenses.xaml
    /// </summary>
    public partial class FoodExpenses : UserControl
    {
        public FoodExpenses()
        {
            InitializeComponent();
        }

        const string typeConst = "Food";

        private void CheckNull() //Function to check if there aren't any dates at all
        {
            int tempCount = MainWindow.objExpenList.ExpenseList.Count;
            if (tempCount == 0)
            {
                Exception ex = new Exception("You haven't entered any expenses yet.\nYou can do it by yourself in " +
                    "\"Input your data\" section\nor read data from file in section \"Files\".");
                throw ex;
            }
        }

        private void CreateMonthDictionary(ref SortedDictionary<int, int> monthDictionary) //Create dictionary with sums less than 20 UAH
        {
            for (int i = 0; i < MainWindow.objExpenList.ExpenseList.Count; ++i)
            {
                int keyMonth = MainWindow.objExpenList.ExpenseList[i].Time.Month;
                if (MainWindow.objExpenList.ExpenseList[i].Type == typeConst)
                {
                    if (MainWindow.objExpenList.ExpenseList[i].FindSumUAH() < 20)
                    {
                        if (monthDictionary.TryGetValue(keyMonth, out int count))
                        {
                            count++;
                            monthDictionary[keyMonth] = count;
                        }
                        else monthDictionary.Add(keyMonth, 1);
                    }
                }
            }
            if (monthDictionary.Count == 0)
                throw new Exception("There aren't any food expenses less than 20 UAH!\nPlease, enter some at \"Input your data\" section!");
        }

        private void FindAverage(SortedDictionary<int, int> monthDictionary)
        {
            int totalSum = 0;
            foreach (KeyValuePair<int, int> val in monthDictionary)
            {
                totalSum += val.Value;
            }
            totalSum /= monthDictionary.Count;
            if (totalSum == 1)
                Result.Text = totalSum.ToString() + " expense";
            else
                Result.Text = totalSum.ToString() + " expenses";
        }
        private void UpdateTable(SortedDictionary<int, int> monthDictionary)
        {
            List<MonthTable> monthList = new List<MonthTable>();
            foreach (KeyValuePair<int, int> val in monthDictionary)
            {
                monthList.Add(new MonthTable(val.Key, val.Value));
            }
            averageRez.Items.Clear();
            averageRez.Items.Refresh();
            for (int j = 0; j < monthList.Count; ++j)
            {
                averageRez.Items.Add(monthList[j]);
            }
        }

        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckNull();
                SortedDictionary<int, int> monthDictionary = new SortedDictionary<int, int>();
                CreateMonthDictionary(ref monthDictionary);
                FindAverage(monthDictionary);
                UpdateTable(monthDictionary);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
