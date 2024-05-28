using POS_App.Model;
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

namespace POS_App.Views
{
    /// <summary>
    /// Interaction logic for HotWings.xaml
    /// </summary>
    public partial class HotWings : Page
    {

        DbHandler db;

        private string selectedItemName;
        private int selectedItemQuantity;

        private float selectedItemPrice;
        public HotWings(DbHandler dbHandler)
        {
            InitializeComponent();
            db = dbHandler;
        }
        private void ItemBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border clickedBorder = (Border)sender;
            StackPanel stackPanel = (StackPanel)clickedBorder.Child;

            // Extract information from the Labels
            Label nameLabel = stackPanel.Children.OfType<Label>().FirstOrDefault();
            Label quantityLabel = stackPanel.Children.OfType<Label>().Skip(1).FirstOrDefault();
            Label priceLabel = stackPanel.Children.OfType<Label>().Skip(2).FirstOrDefault();

            if (nameLabel != null && quantityLabel != null && priceLabel != null)
            {
                selectedItemName = nameLabel.Content.ToString();
                selectedItemQuantity = int.Parse(quantityLabel.Content.ToString().Split(':')[1].Trim());
                selectedItemPrice = float.Parse(priceLabel.Content.ToString().Split(':')[1].Trim());

            }
            if (selectedItemQuantity > 0)
            {
                selectedItemQuantity -= 1;
                if (selectedItemName == "5 Hot Wings")
                {
                    QuantityLabel.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "10 Hot Wings")
                {
                    QuantityLabel2.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "5 Oven Wings")
                {
                    QuantityLabel3.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "10 Oven Wings")
                {
                    QuantityLabel4.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "Wings")
                {
                    QuantityLabel5.Content = "Qty : " + selectedItemQuantity;
                }
                else
                {
                    QuantityLabel6.Content = "Qty : " + selectedItemQuantity;
                }
                Item item = new Item { Name = selectedItemName, Quanity = 1, Price = selectedItemPrice };

                db.AddItem(item);
                

            }


        }

        // Update the selected item quantity when an item is deleted
        private int qty = 24;
        public void UpdateSelectedItemQuantity(Item deletedItem)
        {
            //MessageBox.Show(""+selectedItemName);
            if (deletedItem.Name == "5 Hot Wings")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "10 Hot Wings")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;

                QuantityLabel2.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "5 Oven Wings")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel3.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "10 Oven Wings")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel4.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "Wings")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel5.Content = "Qty : " + selectedItemQuantity;
            }
            else
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel6.Content = "Qty : " + selectedItemQuantity;
            }
        }
    }
}
