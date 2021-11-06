using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    class ExpenseUSD //Class to store info about expenses in USD
    {
        private DateTime _date;
        private double _sum;
        private string _displayDate;

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public double Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        }

        public string DisplayDate
        {
            get
            {
                return _displayDate;
            }
            set
            {
                _displayDate = value;
            }
        }

        public ExpenseUSD(DateTime time, double sum)
        {
            _date = time.Date;
            _sum = sum;
            _displayDate = _date.ToString("dd/MM/yyyy");
        }

        public static bool operator <(ExpenseUSD ex1, ExpenseUSD ex2)
        {
            if (ex1.Sum < ex2.Sum)
                return true;
            return false;
        }

        public static bool operator >(ExpenseUSD ex1, ExpenseUSD ex2)
        {
            if (ex1.Sum > ex2.Sum)
                return true;
            return false;
        }

        public static bool operator ==(ExpenseUSD ex1, ExpenseUSD ex2)
        {
            if (ex1.Sum == ex2.Sum)
                return true;
            return false;
        }

        public static bool operator !=(ExpenseUSD ex1, ExpenseUSD ex2)
        {
            if (ex1.Sum != ex2.Sum)
                return true;
            return false;
        }
    }
}
