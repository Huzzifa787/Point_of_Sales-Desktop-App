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
using POS_App.Views;
using POS_App.Model;
using System;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace POS_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbHandler db = new DbHandler();
        
        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = db;
            ItemGrid.ItemsSource = db.getItem();
            ShowBurgerPage();
            
        }
        //By Default Items
        private void ShowBurgerPage()
        {
            Burger bg = new Burger(db);
            MainFrame.Content = bg;
        } 
        
        private void Burger_Click(object sender, RoutedEventArgs e)
        {
            Burger bg = new Burger(db);
            MainFrame.Content = bg;
            //Total_Label.Text = db.Total_Am.ToString();
        }
        
        private void Shourama_Click(object sender, RoutedEventArgs e)
        {
            Shourama sh = new Shourama(db);
            MainFrame.Content = sh;
        }
        private void Pizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza p = new Pizza(db);
            MainFrame.Content = p;
        }
        private void HotWings_Click(object sender, RoutedEventArgs e)
        {
            HotWings h = new HotWings(db);
            MainFrame.Content = h;
        }
        private void Nuggets_Click(object sender, RoutedEventArgs e)
        {
            Nugget n = new Nugget(db);
            MainFrame.Content = n;
        }
        private void Pasta_Click(object sender, RoutedEventArgs e)
        {
            Pasta n = new Pasta(db);
            MainFrame.Content = n;
        }
        private void Samosa_Click(object sender, RoutedEventArgs e)
        {
            Samosa n = new Samosa(db);
            MainFrame.Content = n;
        }
        private void Bryani_Click(object sender, RoutedEventArgs e)
        {
            Bryani n = new Bryani(db);
            MainFrame.Content = n;
        } 
        private void Drink_Click(object sender, RoutedEventArgs e)
        {
            ColdDrink n = new ColdDrink(db);
            MainFrame.Content = n;
        }
        private void IceCream_Click(object sender, RoutedEventArgs e)
        {
            IceCream n = new IceCream(db);
            MainFrame.Content = n;
        }
        private void Coffee_Click(object sender, RoutedEventArgs e)
        {
            Coffee n = new Coffee(db);
            MainFrame.Content = n;
        }
        private void Tea_Click(object sender, RoutedEventArgs e)
        {
            Tea n = new Tea(db);
            MainFrame.Content = n;
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            Item? item = ItemGrid.SelectedItem as Item;
            UpdateBurgerQuantityLabel(item);
            db.DelItem(item);
            
        }

        private void UpdateBurgerQuantityLabel(Item deletedItem)
        {
            // Check if the currently displayed page is Burger
            if (MainFrame.Content is Burger burgerPage)
            {
                // Check if the deleted item matches the displayed item
                burgerPage.UpdateSelectedItemQuantity(deletedItem);
                
            }
            if (MainFrame.Content is Bryani bryani)
            {
                // Check if the deleted item matches the displayed item
                bryani.UpdateSelectedItemQuantity(deletedItem);
            }
            if (MainFrame.Content is Coffee coffee)
            {
                // Check if the deleted item matches the displayed item
                coffee.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is ColdDrink coldDrink)
            {
                // Check if the deleted item matches the displayed item
                coldDrink.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is HotWings hotWings)
            {
                // Check if the deleted item matches the displayed item
                hotWings.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is IceCream iceCream)
            {
                // Check if the deleted item matches the displayed item
                iceCream.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Nugget nugget)
            {
                // Check if the deleted item matches the displayed item
                nugget.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Pasta pasta)
            {
                // Check if the deleted item matches the displayed item
                pasta.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Pizza pizza)
            {
                // Check if the deleted item matches the displayed item
                pizza.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Samosa samosa)
            {
                // Check if the deleted item matches the displayed item
                samosa.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Shourama shourama)
            {
                // Check if the deleted item matches the displayed item
                shourama.UpdateSelectedItemQuantity(deletedItem);

            }
            if (MainFrame.Content is Tea tea)
            {
                // Check if the deleted item matches the displayed item
                tea.UpdateSelectedItemQuantity(deletedItem);

            }

            // Update the Total_Label TextBlock

        }
        private Item selectedDataGridItem;

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the DataGrid
            selectedDataGridItem = (Item)ItemGrid.SelectedItem;

            // Get the CheckBox that triggered the event
            CheckBox checkBox = (CheckBox)sender;

            // Update the Comment property based on the CheckBox content
            if (selectedDataGridItem != null && checkBox.IsChecked != null)
            {
                selectedDataGridItem.Comment += $"{checkBox.Content}, ";

                
            }
            
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Document doc = new Document();
                //Create PDF Table  
                PdfPTable tableLayout = new PdfPTable(6);
                //Create a PDF file in specific path  
                string filePath = @"E:\Semesters\7th Semester\Enterprise Application Development\Mid Project\Bill.pdf";

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    PdfWriter.GetInstance(doc, fs);
                    // Rest of your PDF creation code here
                    //Open the PDF document  
                    doc.Open();
                    //Add Content to PDF  
                    doc.Add(Add_Content_To_PDF(tableLayout));
                    // Closing the document  
                    doc.Close();


                    MessageBox.Show("Bill generated successfully!");
                    ItemClear();
                    LabelsClear();
                    
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void ItemClear()
        {
            if(ItemGrid.ItemsSource is ObservableCollection<Item> item)
            {
                item.Clear();

            }
            
        }
        private void LabelsClear()
        {
            Total_Label.Text = "0";
            Discount.Text = "0";
            Sub_total.Text = "0";
            tax.Text = "0";
            Total_Pay.Text = "0";
            item_qty.Text = "0";
        }
        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {
            float[] headers = { 70, 50, 40, 60, 40, 50 }; // Header Widths
            tableLayout.SetWidths(headers);
            tableLayout.WidthPercentage = 50; // Set the PDF File width percentage

            // Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("POS Desktop App", new Font(Font.NORMAL, 13, 1, new iTextSharp.text.BaseColor(153, 51, 0))))
            {
                Colspan = 6,  // Update to 6
                Border = 0,
                PaddingBottom = 20,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            // Add current date and time
            tableLayout.AddCell(new PdfPCell(new Phrase($"Date: {DateTime.Now.ToShortDateString()} Time: {DateTime.Now.ToShortTimeString()}", new Font(Font.NORMAL, 10, Font.NORMAL, BaseColor.BLACK)))
            {
                Colspan = 6,  // Update to 6
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            // Add current date and time
            tableLayout.AddCell(new PdfPCell(new Phrase("Billing Details", new Font(Font.NORMAL, 13, Font.NORMAL, BaseColor.BLACK)))
            {
                Colspan = 6,  // Update to 6
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            // Add headers
            AddCellToHeader(tableLayout, "Item Name");
            AddCellToHeader(tableLayout, "Qty");
            AddCellToHeader(tableLayout, "Price");
            AddCellToHeader(tableLayout, "Total");
            AddCellToHeader(tableLayout, "Tax");
            AddCellToHeader(tableLayout, "Comment");
            List<Item> item_list = new List<Item>();
            // Add body
            foreach (Item item in db.getItem())
            {
                AddCellToBody(tableLayout, $"{item.Name}");
                AddCellToBody(tableLayout, $"{item.Quanity}");
                AddCellToBody(tableLayout, $"{item.Price}");
                AddCellToBody(tableLayout, $"{item.Total}");
                AddCellToBody(tableLayout, $"{item.Tax}");
                AddCellToBody(tableLayout, $"{item.Comment}");
                item_list.Add(item);
            }
            //Adding Total
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "Total");
            AddCellToBody(tableLayout, $"{Total_Label.Text}");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            //Adding Discount
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "Discount");
            AddCellToBody(tableLayout, $"{Discount.Text}");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            //Adding SubTotal
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "Sub_total");
            AddCellToBody(tableLayout, $"{Sub_total.Text}");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            //Adding Tax
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "Tax");
            AddCellToBody(tableLayout, $"{tax.Text}");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            //Adding Total Payable
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "Total_Pay");
            AddCellToBody(tableLayout, $"{Total_Pay.Text}");
            AddCellToBody(tableLayout, "");
            AddCellToBody(tableLayout, "");
            db.InsertBillintoDB(Total_Pay.Text.ToString(),item_list);
            return tableLayout;
        }

        // Method to add single cell to the header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.NORMAL, 8, 1, iTextSharp.text.BaseColor.WHITE)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102)
            });
        }
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.NORMAL, 6, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            });
        }


    }
}
