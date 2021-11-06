using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    class MonthTable
    {
        private string _month;
        private int _expenseCount;

        public string Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
            }
        }

        public int ExpenseCount
        {
            get
            {
                return _expenseCount;
            }
            set
            {
                _expenseCount = value;
            }
        }

        public MonthTable()
        {

        }

        public MonthTable(int month, int count)
        {
            _expenseCount = count;
            if (month == 1)
            {
                _month = "January";
            }
            else if (month == 2)
            {
                _month = "February";
            }
            else if (month == 3)
            {
                _month = "March";
            }
            else if (month == 4)
            {
                _month = "April";
            }
            else if (month == 5)
            {
                _month = "May";
            }
            else if (month == 6)
            {
                _month = "June";
            }
            else if (month == 7)
            {
                _month = "July";
            }
            else if (month == 8)
            {
                _month = "August";
            }
            else if (month == 9)
            {
                _month = "September";
            }
            else if (month == 10)
            {
                _month = "October";
            }
            else if (month == 11)
            {
                _month = "November";
            }
            else if (month == 12)
            {
                _month = "December";
            }
        }
    }
}
