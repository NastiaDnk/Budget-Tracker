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
    /// Interaction logic for RateChanges.xaml
    /// </summary>
    public partial class RateChanges : UserControl
    {
        public RateChanges()
        {
            InitializeComponent();
        }

        private void CreateDateList(DateTime dt, ref List<ExpenseItem> dateList)
        {
            ExpenseItem fix = new ExpenseItem();
            for(int i=0; i< MainWindow.objExpenList.ExpenseList.Count; ++i)
            {
                if(dt == MainWindow.objExpenList.ExpenseList[i].Time.Date /*&& dt == MainWindow.objExpenList.ExpenseList[i+1].Time.Date*/)
                {
                    fix = MainWindow.objExpenList.ExpenseList[i];
                    for(int j=0; j< MainWindow.objExpenList.ExpenseList.Count; ++j)
                    {
                        if(fix.Currency == MainWindow.objExpenList.ExpenseList[j].Currency && fix.Time.Date == MainWindow.objExpenList.ExpenseList[j].Time.Date && fix.ExchangeRate!= MainWindow.objExpenList.ExpenseList[j].ExchangeRate)
                        {
                            dateList.Add(MainWindow.objExpenList.ExpenseList[j]);
                            return;
                        }
                    }
                    //if (MainWindow.objExpenList.ExpenseList[i].Currency == MainWindow.objExpenList.ExpenseList[i + 1].Currency)
                    //{
                    //    if (MainWindow.objExpenList.ExpenseList[i].ExchangeRate != MainWindow.objExpenList.ExpenseList[i + 1].ExchangeRate)
                    //    {
                            
                    //    }
                    //}
                }
            }
        }

        private void CreateResList(List<ExpenseItem> dateList, ref List<ExpenseItem> resList)
        {
            for(int i=0; i<dateList.Count; ++i)
            {
                for(int j=0; j< MainWindow.objExpenList.ExpenseList.Count; ++j)
                {
                    if (dateList[i].Time.Date == MainWindow.objExpenList.ExpenseList[j].Time.Date)
                        resList.Add(MainWindow.objExpenList.ExpenseList[j]);
                }
            }
        }

        private void CreateDateDictionary(ref SortedDictionary<DateTime, int> dateDiction)
        {
            for (int i = 0; i < MainWindow.objExpenList.ExpenseList.Count; ++i)
            {
                if (dateDiction.TryGetValue(MainWindow.objExpenList.ExpenseList[i].Time.Date, out int count))
                {
                    count++;
                    dateDiction[MainWindow.objExpenList.ExpenseList[i].Time.Date] = count;
                }
                else
                    dateDiction.Add(MainWindow.objExpenList.ExpenseList[i].Time.Date, 1);
              
            }
        }

        private void FindDates(SortedDictionary<DateTime, int> dateDiction, ref List<ExpenseItem> dateList)
        {
            foreach(KeyValuePair<DateTime, int> val in dateDiction)
            {
                if(val.Value >1)
                    CreateDateList(val.Key, ref dateList);
            }
        }

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

        
        //private void AddToDateList(ref List<ExpenseItem> dateList, DateTime dt)
        //{
        //    for(int i=0; i< MainWindow.objExpenList.ExpenseList.Count; ++i)
        //    {
        //        if (dt == MainWindow.objExpenList.ExpenseList[i].Time.Date)
        //            dateList.Add(MainWindow.objExpenList.ExpenseList[i]);
        //    }
        //}

        private void UpdateTable(List<ExpenseItem> list)
        {
            ExchangeTable.Items.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                ExchangeTable.Items.Add(list[i]);
            }

        }

        private void FindRateChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                CheckNull();
                MainWindow.objExpenList.SortDate();
                SortedDictionary<DateTime, int> dateDiction = new SortedDictionary<DateTime, int>();
                CreateDateDictionary(ref dateDiction);
                List<ExpenseItem> dateList = new List<ExpenseItem>();
                FindDates(dateDiction, ref dateList);
                if (dateList.Count == 0)
                    throw new Exception("There aren't any dates of currency fluctation!");
                List<ExpenseItem> resList = new List<ExpenseItem>();
                CreateResList(dateList, ref resList);
                UpdateTable(resList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
