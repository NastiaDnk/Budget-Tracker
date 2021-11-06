using System;
using System.Collections.Generic;
using BudgetTracker.Models;
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

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for DatesUSD.xaml
    /// </summary>
    public partial class DatesUSD : UserControl
    {
        public DatesUSD()
        {
            InitializeComponent();
        }

        const int count = 3;

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

        private void CheckEnouhgUSD(Expenses usdExpensList) //Function to check if there are 3 dates with sums in USD
        {
            int tempCount = usdExpensList.ExpenseList.Count;
            if (tempCount < count && tempCount != 0)
            {
                Exception ex = new Exception("Less than " + count + " expenses are present in the table.\nOnly " + tempCount + " of them will be shown!");
                throw ex;
            }
        }

        private void CheckNullUSD(List<ExpenseUSD> usdExpensList) //Function to check if there aren't any dates with sums in USD
        {
            int tempCount = usdExpensList.Count;
            if (tempCount == 0)
            {
                Exception ex = new Exception("There aren't any expenses in USD! Please, enter some in \"Input your data\" section.");
                throw ex;
            }
        }

        private void FindUSDDates(ref List<ExpenseUSD> expensUSDList)
        {
            SortedDictionary<DateTime, double> usdCount = new SortedDictionary<DateTime, double>();

            Expenses usdExpenses = new Expenses();
            for (int i = 0; i < MainWindow.objExpenList.ExpenseList.Count; ++i)
            {
                if (MainWindow.objExpenList.ExpenseList[i].Currency == ExpenseCurrency.USD)
                {
                    usdExpenses.ExpenseList.Add(MainWindow.objExpenList.ExpenseList[i]);
                }
            }

            try
            {
                CheckEnouhgUSD(usdExpenses);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            for (int i = 0; i < usdExpenses.ExpenseList.Count; ++i)
            {
                if (usdCount.TryGetValue(usdExpenses[i].Time.Date, out double sum))
                {
                    usdCount[usdExpenses[i].Time.Date] = sum + usdExpenses.ExpenseList[i].Sum;
                }
                else usdCount.Add(usdExpenses[i].Time.Date, usdExpenses.ExpenseList[i].Sum);
            }

            foreach (KeyValuePair<DateTime, double> val in usdCount)
            {
                ExpenseUSD temp = new ExpenseUSD(val.Key, val.Value);
                expensUSDList.Add(temp);
            }

        }

        private void FindUSDBtn_Click(object sender, RoutedEventArgs e)
        {
            List<ExpenseUSD> expensUSDList = new List<ExpenseUSD>();

            try
            {
                CheckNull();

                FindUSDDates(ref expensUSDList);

                try
                {
                    CheckNullUSD(expensUSDList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                List<ExpenseUSD> resultList = new List<ExpenseUSD>();
                for (int i = 0; i < count; ++i)
                {
                    if (i > expensUSDList.Count)
                        break;
                    ExpenseUSD max = expensUSDList[0];
                    int maxIndex = 0;
                    for (int j = 1; j < expensUSDList.Count; ++j)
                    {
                        if (max < expensUSDList[j])
                        {
                            max = expensUSDList[j];
                            maxIndex = j;
                        }
                    }
                    resultList.Add(expensUSDList[maxIndex]);
                    expensUSDList.RemoveAt(maxIndex);
                    SearchSimilar(ref resultList, max, ref expensUSDList);
                }

                if (resultList.Count > count)
                {
                    string ex = "More than 3 dates were found. \nAll of them will be shown in the table!";
                    MessageBox.Show(ex, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                UpdateTable(resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchSimilar(ref List<ExpenseUSD> list, ExpenseUSD max, ref List<ExpenseUSD> expensUSDList)
        {
            for (int i = 0; i < expensUSDList.Count; ++i)
            {
                if (expensUSDList[i] == max)
                {
                    list.Add(expensUSDList[i]);
                    expensUSDList.RemoveAt(i);
                }
            }
        }

        private void UpdateTable(List<ExpenseUSD> list)
        {
            USDTable.Items.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                USDTable.Items.Add(list[i]);
            }

        }
    }
}
