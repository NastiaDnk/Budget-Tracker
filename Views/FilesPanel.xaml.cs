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
using System.IO;
using Newtonsoft.Json;
using BudgetTracker.Models;
using System.Diagnostics;
using Microsoft.Win32;

namespace BudgetTracker.Views
{
    /// <summary>
    /// Interaction logic for FilesPanel.xaml
    /// </summary>
    public partial class FilesPanel : UserControl
    {
        Communications communication = null;
        public FilesPanel()
        {
            communication = new Communications();
            InitializeComponent();
        }
        
        private bool CheckNull() //Function to check if there aren't any dates at all
        {
            int tempCount = MainWindow.objExpenList.ExpenseList.Count;
            if (tempCount == 0)
            {
                return true;
            }
            return false;
        }

        private void ReadAppendRewrite(int i)
        {
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Text files|*.json*.*";
            fileDialog.DefaultExt = ".txt";
            Nullable<bool> dialogOk = fileDialog.ShowDialog();
            string path = string.Empty;
            if (dialogOk == true)
            {
                path = fileDialog.FileNames[0];
            }
            if (!path.Contains(".json"))
            {
                throw new Exception("Sorry this file has got a wrong format! Please try once more!");
            }
            Expenses tempExp = new Expenses();
            using (StreamReader read = new StreamReader(path))
            {
                string json = read.ReadToEnd();
                if (json == string.Empty)
                    throw new Exception("Your file is empty! Try to open another file.");
                tempExp = JsonConvert.DeserializeObject<Expenses>(json);
            }
            if (i == 0)
                MainWindow.objExpenList.AddExpensesItem(tempExp);
            else
                MainWindow.objExpenList.AddRewrite(tempExp);
        }

        private void ReadFromFileBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckNull())
                {
                    MessageBoxResult result = MessageBox.Show("You have some data in your table. \nIf you want to add data from file - click on \"Yes\".\n" +
                        "If you want to rewrite the data - click on \"No\".", "Add some data?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result.ToString() == "Yes")
                    {
                        ReadAppendRewrite(0);
                    }
                    else
                    {
                        ReadAppendRewrite(1);
                    }
                }
                else
                    ReadAppendRewrite(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            communication.Update(); //Call function to update the table with the event
        }

        private void WriteToFileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckNull())
                    MainWindow.objExpenList.WriteToFile();//Call function to write info to file
                else
                    throw new Exception("You haven't entered any expenses yet.\nYou can do it by yourself in " +
                    "\"Input your data\" section\nor read data from file in section \"Files\".");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
