using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BudgetTracker.Models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for FiveSmallestPanel.xaml
    /// </summary>
    public partial class FiveSmallestPanel : UserControl
    {
        public FiveSmallestPanel()
        {
            InitializeComponent();
        }

        const int count = 5;

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

        private void CheckEnouhg() //Function to check if there are 3 dates with sums in USD
        {
            int tempCount = MainWindow.objExpenList.ExpenseList.Count;
            if (tempCount < count && tempCount != 0)
            {
                Exception ex = new Exception("Less than " + count + " expenses are present in the table.\nOnly " + tempCount + " of them will be shown!");
                throw ex;
            }
        }

        private void FillFive()
        {
            Expenses tempExpenseList = new Expenses(MainWindow.objExpenList);
            UpdateTable(ref tempExpenseList);
        }

        private void FindFiveBtn_Click(object sender, RoutedEventArgs e)
        {
            Expenses tempExpenseList = new Expenses(MainWindow.objExpenList);

            try
            {
                CheckNull();
                try
                {
                    CheckEnouhg();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillFive();
                    return;
                }

                Expenses resultList = new Expenses();
                for (int i = 0; i < count; ++i)
                {
                    if (i > tempExpenseList.ExpenseList.Count)
                        break;
                    ExpenseItem min = tempExpenseList[0];
                    int minIndex = 0;
                    for (int j = 1; j < tempExpenseList.ExpenseList.Count; ++j)
                    {
                        if (min > tempExpenseList[j])
                        {
                            min = tempExpenseList[j];
                            minIndex = j;
                        }
                    }
                    resultList.ExpenseList.Add(tempExpenseList[minIndex]);
                    tempExpenseList.ExpenseList.RemoveAt(minIndex);
                    SearchSimilar(ref resultList, min, ref tempExpenseList);
                }

                if (resultList.ExpenseList.Count > count)
                {
                    string ex = "More than 5 types and subtypes were found. \nAll of them will be shown in the table!";
                    MessageBox.Show(ex, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                UpdateTable(ref resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SearchSimilar(ref Expenses list, ExpenseItem min, ref Expenses expensUSDList)
        {
            for (int i = 0; i < expensUSDList.ExpenseList.Count; ++i)
            {
                if (expensUSDList[i] == min)
                {
                    list.ExpenseList.Add(expensUSDList[i]);
                    expensUSDList.ExpenseList.RemoveAt(i);
                }
            }
        }

        private void UpdateTable(ref Expenses list)
        {
            list.ChangeNumber();
            FiveTypeTable.Items.Clear();
            for (int i = 0; i < list.ExpenseList.Count; ++i)
            {
                FiveTypeTable.Items.Add(list[i]);
            }

        }
    }
}
