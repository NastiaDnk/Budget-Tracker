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
    /// Interaction logic for TimespanPanel.xaml
    /// </summary>
    public partial class TimespanPanel : UserControl
    {
        public TimespanPanel()
        {

            InitializeComponent();
            DateTime now = DateTime.Now.AddDays(1);
            MonthCalendar.BlackoutDates.Add(new CalendarDateRange(now, new DateTime(2100, 3, 7)));
            MonthCalendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 12, 31), new DateTime(2020, 12, 31)));
        }

        const int comp = 200;
        private void CheckDate(DateTime dt, ref Expenses dayExpenses)
        {
            for (int i = 0; i < MainWindow.objExpenList.ExpenseList.Count; ++i)
            {
                if (dt == MainWindow.objExpenList.ExpenseList[i].Time.Date)
                {
                    dayExpenses.ExpenseList.Add(MainWindow.objExpenList.ExpenseList[i]);
                }
            }
            if (dayExpenses.ExpenseList.Count == 0)
                throw new Exception("Oops...\nThere aren't any expenses done on " + dt.ToString("dd.MM.yyyy") + "!\n" +
                    "Please select another date!");
        }

        private void FindSpans(Expenses dayExpenses, ref int[] arr1, ref int[] arr2, ref int[] arr3)
        {
            for (int i = 0; i < dayExpenses.ExpenseList.Count; ++i)
            {
                if (dayExpenses.ExpenseList[i].FindSumUAH() < comp)
                {
                    DetectSpan(dayExpenses.ExpenseList[i], ref arr1);
                }
                else
                {
                    DetectSpan(dayExpenses.ExpenseList[i], ref arr2);
                }
                DetectSpan(dayExpenses.ExpenseList[i], ref arr3);
            }
        }

        private void DetectSpan(ExpenseItem expense, ref int[] arr)
        {
            DateTime span1 = new DateTime(2010, 2, 2, 6, 0, 0), span2 = new DateTime(2010, 2, 2, 12, 0, 0), span3 = new DateTime(2010, 2, 2, 18, 0, 0);
            if (expense.Time.Hour < span1.Hour)
                arr[0]++;
            else if ((expense.Time.Hour > span1.Hour && expense.Time.Hour < span2.Hour) || (expense.Time.Hour == span1.Hour && expense.Time.Minute > span1.Minute))
                arr[1]++;
            else if ((expense.Time.Hour > span2.Hour && expense.Time.Hour < span3.Hour) || (expense.Time.Hour == span2.Hour && expense.Time.Minute > span2.Minute))
                arr[2]++;
            else
                arr[3]++;
        }

        private int FindMax(int [] arr)
        {
            int max = arr[0], maxIndex=0;
            for(int i=1; i<arr.Length; ++i)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                    maxIndex = i;
                }
            }
            if (max == 0)
                maxIndex = 4;
            return maxIndex;
        }

        private void CheckEqual(int index, int []arr, string key)
        {
            if (index >= arr.Length)
                return;
            int max = arr[index];
            for(int i=0; i<arr.Length; ++i)
            {
                if(i!=index && max == arr[i])
                {
                    string st = "There are some equal spans with number of " + key + " expenses.\nOnly the first one will be shown!";
                    MessageBox.Show(st, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private string MakeString(int index)
        {
            DateTime span1 = new DateTime(2010, 2, 2, 0, 0, 0), span2 = new DateTime(2010, 2, 2, 6, 0, 0), span3 = new DateTime(2010, 2, 2, 12, 0, 0), span4 = new DateTime(2010, 2, 2, 18, 0, 0);
            string rez = "";
            if (index == 0)
                rez = "00:00-06:00";
            else if (index == 1)
                rez = "06:00-12:00";
            else if (index == 2)
                rez = "12:00-18:00";
            else if (index == 3)
                rez = "18:00-24:00";
            else if (index == 4)
                rez = "No expenses";
            return rez;
        }

        private void ResSpans(int[] less, int[] more, int[] all)
        {
            
            int index = FindMax(less);
            CheckEqual(index, less, "small");
            SmallRes.Text = MakeString(index);
            index = FindMax(more);
            CheckEqual(index, more, "large");
            LargeRes.Text = MakeString(index);
            index = FindMax(all);
            CheckEqual(index, all, "large");
            AllRes.Text = MakeString(index);
        }

        private void MonthCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime dt = new DateTime(MonthCalendar.SelectedDate.Value);
            DateTime dt = new DateTime(MonthCalendar.SelectedDate.Value.Year, MonthCalendar.SelectedDate.Value.Month, MonthCalendar.SelectedDate.Value.Day);
            //DateTime dt = MonthCalendar.SelectedDate.Value.Date;
            string st = dt.ToString();
            try
            {
                Expenses dayExpenses = new Expenses();
                CheckDate(dt, ref dayExpenses);
                int [] less = new int[4], more = new int[4], all = new int[4];
                FindSpans(dayExpenses, ref less, ref more, ref all);
                ResSpans(less, more, all);
                UpdateTable(dayExpenses);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTable(Expenses ex)
        {
            ex.SortDate();
            TimeSpanTable.Items.Clear();
            for (int i = 0; i < ex.ExpenseList.Count; ++i)
            {
                TimeSpanTable.Items.Add(ex.ExpenseList[i]);
            }
        }
    }
}
