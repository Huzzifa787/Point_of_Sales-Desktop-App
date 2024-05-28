using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using POS_App.Views;
using Microsoft.Data.SqlClient;

namespace POS_App.Model
{
    public class DbHandler:INotifyPropertyChanged
    {
        ObservableCollection<Item> item_list = new ObservableCollection<Item>();
        private double total_am = 0;
        private double discount = 0;
        private double sub_total = 0;
        private double tax = 0;
        private double result = 0;
        private double total_pay = 0;
        private int types = 0;
       

        public event PropertyChangedEventHandler? PropertyChanged;

        public Double Total_Am
        {
            set { total_am = value; notifyfrontend("Total_Am"); }
            get { return total_am; }
        } 
        public Double Tax
        {
            set { tax = value; notifyfrontend("Tax"); }
            get { return tax; }
        }
        public Double Discount
        {
            set { discount = value; notifyfrontend("Discount"); }
            get { return discount; }
        }
        public Double Sub_Total
        {
            set { sub_total = value; notifyfrontend("Sub_Total"); }
            get { return sub_total; }
        }
        public Double Total_Pay
        {
            set { total_pay = value; notifyfrontend("Total_Pay"); }
            get { return total_pay; }
        }
        public int Types
        {
            set { types = value; notifyfrontend("Types"); }
            get { return types; }
        }
        private void notifyfrontend(string proertyname)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(proertyname));
            }
        }
        public ObservableCollection<Item> getItem()
        {
            return item_list;
        }
        public void AddItem(Item item)
        {
            Item existingItem = item_list.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                // Item with the same name already exists, update the quantity
                existingItem.Quanity += 1;
                existingItem.Tax = 1;
                existingItem.Total = existingItem.Price * existingItem.Quanity;

                Total_Am += item.Price;
                
                Tax += existingItem.Tax;
                result = 14.45 / Tax;
                Tax = Math.Round(result, 2);
                if (Total_Am > 2000)
                {
                    Discount = 100;
                }
                Sub_Total = Total_Am - Discount;
                Total_Pay = Sub_Total + Tax;
            }
            else
            {
                // Item with this name doesn't exist, add it to the collection
                item.Total = item.Price * item.Quanity;
                Total_Am += item.Total;
                item_list.Add(item);
                if (Total_Am > 2000)
                {
                    Discount = 100;
                }
                Sub_Total = Total_Am - Discount;
                Total_Pay = Sub_Total + Tax;
                Types += 1;
            }
            
        }
        
        
        public void InsertBillintoDB(String total, List<Item> items)
        {
            try
            {
                string conString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=POS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    // Insert into Order Table
                    string orderQuery = "INSERT INTO dbo.[Order](Order_date, Total_Amount) VALUES (GETDATE(), @T); SELECT SCOPE_IDENTITY();";
                    SqlParameter p2 = new SqlParameter("@T", total);

                    using (SqlCommand sqlCommand = new SqlCommand(orderQuery, connection))
                    {
                        sqlCommand.Parameters.Add(p2);

                        // Get the newly inserted Order ID
                        int orderId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        // Insert each item into the Item Table
                        string itemQuery = "INSERT INTO dbo.Item(Item_Name, Item_Qty, Item_Price,Order_id) VALUES ( @N, @Q, @P,@OrderID)";
                        foreach (Item item in items)
                        {
                            using (SqlCommand itemCommand = new SqlCommand(itemQuery, connection))
                            {
                                itemCommand.Parameters.AddWithValue("@N", item.Name);
                                itemCommand.Parameters.AddWithValue("@Q", item.Quanity);
                                itemCommand.Parameters.AddWithValue("@P", item.Price);
                                itemCommand.Parameters.AddWithValue("@OrderID", orderId);

                                itemCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void DelItem(Item item)
        {
            item_list.Remove(item);
            
            Total_Am -= item.Total;
            if (Total_Am > 2000)
            {
                Discount = 100;
            }
            else
            {
                Discount = 0;
            }
            Tax = 0;
            Sub_Total = Total_Am - Discount;
            Total_Pay = Sub_Total + Tax;
            Types -= 1;
        }

       


    }
}
