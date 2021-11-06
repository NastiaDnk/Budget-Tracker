using System;
using System.Collections.Generic;
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
using BudgetTracker.Models;

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for AddFromKeyboard.xaml
    /// </summary>
    public partial class AddFromKeyboard : UserControl
    {
        Communications communication = null;
        public AddFromKeyboard()
        {
            communication = new Communications();
            InitializeComponent();
        }

        private void Clearbtn_Click(object sender, RoutedEventArgs e)
        {
            dateTb.Clear();
            typeTb.Clear();
            subtypeTb.Clear();
            sumTb.Clear();
            currencyTb.Clear();
            rateTb.Clear();
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            string sTime = dateTb.Text, sType = typeTb.Text, sSubtype = subtypeTb.Text, sSum = sumTb.Text, sCurrency = currencyTb.Text, sRate = rateTb.Text;
            MainWindow.objExpenList.AddExpensesItem(sTime, sType, sSubtype, sSum, sCurrency, sRate); //call function to add an item
            communication.Update();
        }
    }
}
