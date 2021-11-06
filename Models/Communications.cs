using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    class Communications
    {

            public delegate void MethodContainer();
            public static event MethodContainer ShowTable;
            public static event MethodContainer WriteTo;
            public static event MethodContainer Check;

            public delegate void Create(string sTime, string sType, string sSubtype, string sSum, string sCurrency, string sRate);
            public static event Create CreateList;

            public delegate void UpdateContainer(Expenses ex);
            public static event UpdateContainer ShowTableCategory;

            public void Update()
            {
                ShowTable?.Invoke();
            }

            public void Update(Expenses ex)
            {
                ShowTableCategory?.Invoke(ex);
            }

            public void WriteToFile()
            {
                WriteTo?.Invoke();
            }

            public void CheckNull()
            {
                Check?.Invoke();
            }

            public void CreateNewList(string sTime, string sType, string sSubtype, string sSum, string sCurrency, string sRate)
            {
                CreateList?.Invoke(sTime, sType, sSubtype, sSum, sCurrency, sRate);
            }
        
    }
}
