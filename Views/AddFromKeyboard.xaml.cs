using System;
using System.Windows;
using System.Windows.Controls;
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
            try
            {
                string sTime = dateTb.Text, sType = typeTb.Text, sSubtype = subtypeTb.Text, sSum = sumTb.Text, sCurrency = currencyTb.Text, sRate = rateTb.Text;
                if (sTime == "" || sType == "" || sSubtype == "" || sSum == "" || sCurrency == "" || sRate == "")
                    throw new Exception("You haven't entered enough data.\nPlease, try once more!");
                MainWindow.objExpenList.AddExpensesItem(sTime, sType, sSubtype, sSum, sCurrency, sRate); //call function to add an item
                communication.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
