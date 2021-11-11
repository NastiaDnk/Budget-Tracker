using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    public enum ExpenseCurrency { UAH, USD, EUR }
    public class ExpenseItem
    {
        private int _number;
        private DateTime _time;
        private string _type;
        private string _subtype;
        private double _sum;
        private ExpenseCurrency _currency;
        private double _exchangeRate;
        private string _displayDate;

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string Subtype
        {
            get
            {
                return _subtype;
            }
            set
            {
                _subtype = value;
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

        public ExpenseCurrency Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;
            }
        }

        public double ExchangeRate
        {
            get
            {
                return _exchangeRate;
            }
            set
            {
                _exchangeRate = value;
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
        public ExpenseItem()
        {

        }

        public ExpenseItem(string sTime, string sType, string sSubtype, string sSum, string sCurrency, string sRate)//Constructor with strings
        {
            if (!DateTime.TryParse(sTime, out _time))
            {
                Exception ex = new Exception("Invalid date and time. Please, check your data!");
                throw ex;
            }
            _displayDate = _time.ToString("dd.MM.yyyy HH:mm:ss");
            _type = sType;
            _subtype = sSubtype;
            if (!double.TryParse(sSum, out _sum))
            {
                Exception ex = new Exception("Invalid sum. Please, check your data!");
                throw ex;
            }

            if (sCurrency == "USD")
                _currency = ExpenseCurrency.USD;
            else if (sCurrency == "UAH")
                _currency = ExpenseCurrency.UAH;
            else if (sCurrency == "EUR")
                _currency = ExpenseCurrency.EUR;
            else
            {
                Exception ex = new Exception("Invalid currency. Please, check your data!");
                throw ex;
            }

            if (!double.TryParse(sRate, out _exchangeRate))
            {
                Exception ex = new Exception("Invalid exchange rate. Please, check your data!");
                throw ex;
            }
        }
        public ExpenseItem(int number, DateTime time, string type, string subtype, double sum, ExpenseCurrency currency, double exchangeRate)//Constructor with data
        {
            _number = number;
            _time = time;
            _type = type;
            _subtype = subtype;
            _sum = sum;
            _currency = currency;
            _exchangeRate = exchangeRate;
            _displayDate = _time.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public ExpenseItem(ExpenseItem other) //Copy constructor
        {
            _number = other._number;
            _time = other._time;
            _type = other._type;
            _subtype = other._subtype;
            _sum = other._sum;
            _currency = other._currency;
            _exchangeRate = other._exchangeRate;
            _displayDate = other._displayDate;
        }

        public double FindSumUAH()
        {
            double rez;
            rez = Sum * ExchangeRate;
            return rez;
        }

        public void ToStringDate()
        {
            _displayDate = _time.ToString("dd.MM.yyyy HH:mm:ss");
        }


        public static bool operator <(ExpenseItem ex1, ExpenseItem ex2)
        {
            if (ex1.Sum < ex2.Sum)
                return true;
            return false;
        }

        public static bool operator >(ExpenseItem ex1, ExpenseItem ex2)
        {
            if (ex1.Sum > ex2.Sum)
                return true;
            return false;
        }

        public static bool operator ==(ExpenseItem ex1, ExpenseItem ex2)
        {
            if (ex1.Sum == ex2.Sum)
                return true;
            return false;
        }

        public static bool operator !=(ExpenseItem ex1, ExpenseItem ex2)
        {
            if (ex1.Sum != ex2.Sum)
                return true;
            return false;
        }
    }
}
