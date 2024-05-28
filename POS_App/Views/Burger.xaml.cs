using POS_App.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static POS_App.Model.DbHandler;

namespace POS_App.Views
{
    /// <summary>
    /// Interaction logic for Burger.xaml
    /// </summary>
    public partial class Burger : Page
    {
        DbHandler db;
        
        private string selectedItemName;
        private int selectedItemQuantity;
        
        private float selectedItemPrice;
        public Burger(DbHandler dbHandler)
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
                if(selectedItemName == "Zinger Burger")
                {
                    QuantityLabel.Content = "Qty : " + selectedItemQuantity;
                }
                else if(selectedItemName == "Grilled Burger")
                {
                    QuantityLabel2.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "Chicken Burger")
                {
                    QuantityLabel3.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "Cheese Burger")
                {
                    QuantityLabel4.Content = "Qty : " + selectedItemQuantity;
                }
                else if (selectedItemName == "Full Cheese Burger")
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
        private int qty=24;
        public void UpdateSelectedItemQuantity(Item deletedItem)
        {
            //MessageBox.Show(""+selectedItemName);
            if (deletedItem.Name == "Zinger Burger")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "Grilled Burger")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                
                QuantityLabel2.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "Chicken Burger")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel3.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "Cheese Burger")
            {
                selectedItemQuantity = qty - deletedItem.Quanity;
                selectedItemQuantity += deletedItem.Quanity;
                QuantityLabel4.Content = "Qty : " + selectedItemQuantity;
            }
            else if (deletedItem.Name == "Full Cheese Burger")
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
