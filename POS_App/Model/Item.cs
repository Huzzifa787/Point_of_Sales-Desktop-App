using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_App.Model
{
    public class Item : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private int quanity;
        private float price;
        private double total;
        
        private int tax;
        private string comment;

        public String Name {  get { return name; }  set { name = value; notifyfrontend("Name"); } }
        public String Comment {  get { return comment; }  set { comment = value; notifyfrontend("Comment"); } }
        public int Quanity { get {  return quanity; } set {  quanity = value; notifyfrontend("Quanity"); } }
        public float Price { get { return price; } set { price = value; notifyfrontend("Price"); } }
        public Double Total { get { return total; } set { total = value; notifyfrontend("Total"); } }
        
        public int Tax { get { return tax; } set { tax = value; notifyfrontend("Tax"); } }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        private void notifyfrontend(string proertyname)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(proertyname));
            }
        }
    }
}
