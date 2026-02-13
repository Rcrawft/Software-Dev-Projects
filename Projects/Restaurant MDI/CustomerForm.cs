//Ryan Crawford
//12/4/2024
//4143 Dr. Stringfellow
//This is the code for the child customer form which is used
//to submit orders for the table.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__Prog_8
{
    public partial class CustomerForm : Form
    {
        //doubles for holding values of total price for each menu item, as 
        //well as subtotal, tax, and final total
        public double btot = 0, atot = 0, etot = 0, dtot = 0, tot = 0, stot = 0, tax = 0;

        //these dictionaries will have the contents of dictionaries
        //from main form copied into them
        Dictionary<string, double> bev;
        Dictionary<string, double> app;
        Dictionary<string, double> ent;
        Dictionary<string, double> des;

        //constructor for customer form: receives dictionaries and title 
        //from main form
        public CustomerForm(string title, Dictionary<string, double> b,
            Dictionary<string, double> a, Dictionary<string, double> e,
            Dictionary<string, double> d)
        {
            InitializeComponent();

            //form text field is same as title that is passed in from
            //main form
            Text = title;

            //copies contents of main form dictionaries to this form's
            //dictionaries
            bev = b;
            app = a;
            ent = e;
            des = d;

            //each combo box has choice "None"
            bevComboBox.Items.Add("None");
            appComboBox.Items.Add("None");
            entComboBox.Items.Add("None");
            desComboBox.Items.Add("None");

            //foreach loops add key value (item name) of each dictionary
            //to appropriate combo boxes for each item type
            foreach (KeyValuePair<string, double> p in bev)
            {
                bevComboBox.Items.Add(p.Key);
            }
            foreach (KeyValuePair<string, double> p in app)
            {
                appComboBox.Items.Add(p.Key);
            }
            foreach (KeyValuePair<string, double> p in ent)
            {
                entComboBox.Items.Add(p.Key);
            }
            foreach (KeyValuePair<string, double> p in des)
            {
                desComboBox.Items.Add(p.Key);
            }

        }

        //event handler for changing text in appetizer combo box
        private void appComboBox_TextChanged(object sender, EventArgs e)
        {
            //name of item
            string s = appComboBox.Text;
            //appetizer total is 0 if no appetizer chosen
            if (s == "None")
            {
                atot = 0;
            }
            //if not none, then find price of item
            else
            {
                //searches for price that corresponds with item name
                //in each dictionary pair
                foreach (KeyValuePair<string, double> p in app)
                {
                    if (p.Key == s)
                    {
                        //asigns price to appetizer total
                        atot = p.Value;
                    }
                }
            }

            //updates price labels
            updateUserLabels(totalsControl);
        }

        //event handler for changing text in entree combo box
        private void entComboBox_TextChanged(object sender, EventArgs e)
        {
            //name of item
            string s = entComboBox.Text;
            //entree total is 0 if no entree chosen
            if (s == "None")
            {
                etot = 0;
            }
            //if not none, then find price of item
            else
            {
                //searches for price that corresponds with item name
                //in each dictionary pair
                foreach (KeyValuePair<string, double> p in ent)
                {
                    if (p.Key == s)
                    {
                        //asigns price to entree total
                        etot = p.Value;
                    }
                }
            }

            //updates price labels
            updateUserLabels(totalsControl);
        }

        //event handler for loading customer form
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            //each combo box set to none by default
            bevComboBox.Text = "None";
            appComboBox.Text = "None";
            entComboBox.Text = "None";
            desComboBox.Text = "None";
        }

        //event handler for changing text in dessert combo box
        private void desComboBox_TextChanged(object sender, EventArgs e)
        {
            //name of item
            string s = desComboBox.Text;
            //dessert total is 0 if no dessert chosen
            if (s == "None")
            {
                dtot = 0;
            }
            //if not none, then find price of item
            else
            {
                //searches for price that corresponds with item name
                //in each dictionary pair
                foreach (KeyValuePair<string, double> p in des)
                {
                    if (p.Key == s)
                    {
                        //asigns price to dessert total
                        dtot = p.Value;
                    }
                }
            }

            //updates price labels
            updateUserLabels(totalsControl);
        }

        //event handler for clear button
        private void clearButton_Click(object sender, EventArgs e)
        {
            //each combo box set back to none
            bevComboBox.Text = "None";
            appComboBox.Text = "None";
            entComboBox.Text = "None";
            desComboBox.Text = "None";
        }

        //event handler for submit button
        private void submitButton_Click(object sender, EventArgs e)
        {
            //if item is in bev combo box, submit to table form
            if(bevComboBox.Text != null && bevComboBox.Text != "None")
            { 
                //creates ListViewItem that contains name and price of item
                ListViewItem item = new ListViewItem(new[] { bevComboBox.Text, btot.ToString("C") });
                //adds item to table form ListView
                TableForm.tform.lv1.Items.Add(item);
            }

            //if item is in app combo box, submit to table form
            if (appComboBox.Text != null && appComboBox.Text != "None")
            {
                //creates ListViewItem that contains name and price of item
                ListViewItem item = new ListViewItem(new[] { appComboBox.Text, atot.ToString("C") });
                //adds item to table form ListVieww
                TableForm.tform.lv1.Items.Add(item);
            }

            //if item is in ent combo box, submit to table form
            if (entComboBox.Text != null && entComboBox.Text != "None")
            {
                //creates ListViewItem that contains name and price of item
                ListViewItem item = new ListViewItem(new[] { entComboBox.Text, etot.ToString("C") });
                //adds item to table form ListVieww
                TableForm.tform.lv1.Items.Add(item);
            }

            //if item is in des combo box, submit to table form
            if (desComboBox.Text != null && desComboBox.Text != "None")
            {
                //creates ListViewItem that contains name and price of item
                ListViewItem item = new ListViewItem(new[] { desComboBox.Text, dtot.ToString("C") });
                //adds item to table form ListVieww
                TableForm.tform.lv1.Items.Add(item);
            }

            //get current subtotal from table form too add to new subtotal
            double stot1 = Double.Parse(TableForm.tform.rcon.GetSub(), NumberStyles.Currency);
            //calculate new subtotal
            stot = btot + atot + etot + dtot + stot1;
            //calculate new tax
            tax = stot * .0825;
            //calculate new total
            tot = stot + tax;

            //update table form price labels on restaurant control
            TableForm.tform.rcon.SetSub(stot.ToString("C"));
            TableForm.tform.rcon.SetTax(tax.ToString("C"));
            TableForm.tform.rcon.SetTot(tot.ToString("C"));
        }

        //event handler for changing text in beverage combo box
        private void bevComboBox_TextChanged(object sender, EventArgs e)
        {
            //name of item
            string s = bevComboBox.Text;
            //beverage total is 0 if no beverage chosen
            if (s == "None")
            {
                btot = 0;
            }
            //if not none, then find price of item
            else
            {
                //searches for price that corresponds with item name
                //in each dictionary pair
                foreach (KeyValuePair<string, double> p in bev)
                {
                    if (p.Key == s)
                    {
                        //asigns price to beverage total
                        btot = p.Value;
                    }
                }
            }

            //updates price labels
            updateUserLabels(totalsControl);
        }

        //function for updating price labels on restaurant control
        private void updateUserLabels(RestaurantControl.RestaurantControl r)
        {
            //computes subtotal
            stot = btot + atot + etot + dtot;
            //computes tax at 8.25%
            tax = stot * .0825;
            //computes total
            tot = stot + tax;

            //updates each label with new values
            r.SetSub(stot.ToString("C"));
            r.SetTax(tax.ToString("C"));
            r.SetTot(tot.ToString("C"));
        }

    }
}
