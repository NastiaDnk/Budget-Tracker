using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;

namespace BudgetTracker.Models
{
    public class Expenses
    {
        private List<ExpenseItem> expenseList = null;

        public List<ExpenseItem> ExpenseList
        {
            get
            {
                return expenseList;
            }
            set
            {
                expenseList = value;
            }
        }

        public ExpenseItem this[int index] //Define the indexer
        {
            get
            {
                return expenseList[index];
            }
            set
            {
                expenseList[index] = value;
            }
        }


        public Expenses()
        {
            expenseList = new List<ExpenseItem>();
        }

        public Expenses(List<ExpenseItem> list)
        {
            expenseList = list;
        }

        public Expenses(Expenses list)
        {
            expenseList = new List<ExpenseItem>();
            for (int i = 0; i < list.expenseList.Count; ++i)
            {
                expenseList.Add(list.expenseList[i]);
            }
        }

        public void WriteToFile() //Writing to file
        {
            string path = @"C:\Users\Milka\Desktop\Uni\CourseWork\BudgetTracker\BudgetTracker\Files\Results.json";
            string expens = JsonConvert.SerializeObject(expenseList);
            File.WriteAllText(path, expens);
        }

        //public void ReadFromFile()//Reading from file
        //{
        //    string path = @"C:\Users\Milka\Desktop\Uni\CourseWork\BudgetTracker\BudgetTracker\Files\Data.json";
        //    List<ExpenseItem> expenseLst = new List<ExpenseItem>();
        //    using (StreamReader read = new StreamReader(path))
        //    {
        //        string json = read.ReadToEnd();
        //        if (json == string.Empty)
        //            throw new Exception("Your file is empty! Try to open another file.");
        //        expenseList = JsonConvert.DeserializeObject<List<ExpenseItem>>(json);
        //    }
        //}

        public void AddExpensesItem(string sTime, string sType, string sSubtype, string sSum, string sCurrency, string sRate)
        {
            try
            {
                ExpenseItem itm = new ExpenseItem(sTime, sType, sSubtype, sSum, sCurrency, sRate);
                itm.Number = expenseList.Count + 1;
                expenseList.Add(itm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void AddExpensesItem(Expenses ex)
        {
            for (int i = 0; i < ex.ExpenseList.Count; ++i)
            {
                ex.expenseList[i].ToStringDate();
                ex.expenseList[i].Number = expenseList.Count + 1;
                expenseList.Add(ex.ExpenseList[i]);
            }
        }

        public void SortDate() //Sort by date in descending order
        {
            QuickSortDate(0, expenseList.Count - 1);
            ChangeNumber();
        }

        public void ChangeNumber() //Change numbers in increasing order
        {
            for (int i = 0; i < expenseList.Count; ++i)
            {
                expenseList[i].Number = i + 1;
            }
        }

        void QuickSortDate(int lowerBound, int upperBound) //Sort by date in descending order with quick sort
        {
            if (upperBound == lowerBound) //End of recursion
                return;
            int reverseBound = upperBound;
            ExpenseItem temp = new ExpenseItem();
            DateTime mainElem = expenseList[lowerBound].Time;
            int fl = 0, split = 0;
            for (int i = lowerBound; i <= upperBound; ++i)
            {
                if (DateTime.Compare(expenseList[i].Time, mainElem) <= 0)
                {
                    for (int j = reverseBound; j >= lowerBound; --j) //Going from the opposite side of the list
                    {
                        if (DateTime.Compare(expenseList[j].Time, mainElem) > 0 && i < j)
                        {
                            temp = expenseList[j];
                            expenseList[j] = expenseList[i];
                            expenseList[i] = temp;
                            reverseBound = j - 1;
                            break;
                        }
                        else if (i > j) //Check indexes
                        {
                            split = j;
                            fl = 1;
                            break;
                        }
                    }
                }
                if (fl == 1)
                    break;
            }
            QuickSortDate(lowerBound, split);
            QuickSortDate(split + 1, upperBound);
            return;
        }

        void QuickSortType(int lowerBound, int upperBound) //Sort by type in increasing order with quick sort
        {
            if (upperBound == lowerBound) //End of recursion
                return;
            int reverseBound = upperBound;
            ExpenseItem temp = new ExpenseItem();
            string mainElem = expenseList[lowerBound].Type;
            int fl = 0, split = 0;
            for (int i = lowerBound; i <= upperBound; ++i)
            {
                if (String.Compare(expenseList[i].Type, mainElem) >= 0)
                {
                    for (int j = reverseBound; j >= lowerBound; --j) //Going from the opposite side of the list
                    {
                        if (String.Compare(expenseList[j].Type, mainElem) < 0 && i < j)
                        {
                            temp = expenseList[j];
                            expenseList[j] = expenseList[i];
                            expenseList[i] = temp;
                            reverseBound = j - 1;
                            break;
                        }
                        else if (i > j) //Check indexes
                        {
                            split = j;
                            fl = 1;
                            break;
                        }
                    }
                }
                if (fl == 1)
                    break;
            }
            QuickSortType(lowerBound, split);
            QuickSortType(split + 1, upperBound);
            return;
        }

        public void SortType() //Sort by date in descending order
        {
            QuickSortType(0, expenseList.Count - 1);
            ChangeNumber();
        }

        void QuickSortSubtype(int lowerBound, int upperBound) //Sort by subtype in increasing order with quick sort
        {
            if (upperBound == lowerBound) //End of recursion
                return;
            int reverseBound = upperBound;
            ExpenseItem temp = new ExpenseItem();
            string mainElem = expenseList[lowerBound].Subtype;
            int fl = 0, split = 0;
            for (int i = lowerBound; i <= upperBound; ++i)
            {
                if (String.Compare(expenseList[i].Subtype, mainElem) >= 0)
                {
                    for (int j = reverseBound; j >= lowerBound; --j) //Going from the opposite side of the list
                    {
                        if (String.Compare(expenseList[j].Subtype, mainElem) < 0 && i < j)
                        {
                            temp = expenseList[j];
                            expenseList[j] = expenseList[i];
                            expenseList[i] = temp;
                            reverseBound = j - 1;
                            break;
                        }
                        else if (i > j) //Check indexes
                        {
                            split = j;
                            fl = 1;
                            break;
                        }
                    }
                }
                if (fl == 1)
                    break;
            }
            QuickSortSubtype(lowerBound, split);
            QuickSortSubtype(split + 1, upperBound);
            return;
        }

        void QuickSortSum(int lowerBound, int upperBound) //Sort by sum in UAH in increasing order with quick sort
        {
            if (upperBound == lowerBound) //End of recursion
                return;
            int reverseBound = upperBound;
            ExpenseItem temp = new ExpenseItem();
            double mainElem = expenseList[lowerBound].FindSumUAH();
            int fl = 0, split = 0;
            for (int i = lowerBound; i <= upperBound; ++i)
            {
                if (expenseList[i].FindSumUAH() >= mainElem)
                {
                    for (int j = reverseBound; j >= lowerBound; --j) //Going from the opposite side of the list
                    {
                        if (expenseList[j].FindSumUAH() < mainElem && i < j)
                        {
                            temp = expenseList[j];
                            expenseList[j] = expenseList[i];
                            expenseList[i] = temp;
                            reverseBound = j - 1;
                            break;
                        }
                        else if (i > j) //Check indexes
                        {
                            split = j;
                            fl = 1;
                            break;
                        }
                    }
                }
                if (fl == 1)
                    break;
            }
            QuickSortSum(lowerBound, split);
            QuickSortSum(split + 1, upperBound);
            return;
        }

        public void SortSubtype() //Sort by subtype in increasing order
        {
            QuickSortSubtype(0, expenseList.Count - 1);
            ChangeNumber();
        }


        public void SortSum() //Sort by sum in UAH in increasing order
        {
            QuickSortSum(0, expenseList.Count - 1);
            ChangeNumber();
        }

        void QuickSortCurrency(int lowerBound, int upperBound) //Sort by sum in UAH in increasing order with quick sort
        {
            if (upperBound == lowerBound) //End of recursion
                return;
            int reverseBound = upperBound;
            ExpenseItem temp = new ExpenseItem();
            ExpenseCurrency mainElem = expenseList[lowerBound].Currency;
            int fl = 0, split = 0;
            for (int i = lowerBound; i <= upperBound; ++i)
            {
                if (expenseList[i].Currency >= mainElem)
                {
                    for (int j = reverseBound; j >= lowerBound; --j) //Going from the opposite side of the list
                    {
                        if (expenseList[j].Currency < mainElem && i < j)
                        {
                            temp = expenseList[j];
                            expenseList[j] = expenseList[i];
                            expenseList[i] = temp;
                            reverseBound = j - 1;
                            break;
                        }
                        else if (i > j) //Check indexes
                        {
                            split = j;
                            fl = 1;
                            break;
                        }
                    }
                }
                if (fl == 1)
                    break;
            }
            QuickSortCurrency(lowerBound, split);
            QuickSortCurrency(split + 1, upperBound);
            return;
        }

        public void SortCurrency() //Sort by sum in UAH in increasing order
        {
            QuickSortCurrency(0, expenseList.Count - 1);
            ChangeNumber();
        }
    }
}

