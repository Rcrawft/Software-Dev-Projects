//Ryan Crawford
//12/4/2024
//4143 Dr. Stringfellow
//This is the code for the main menu form, where the user 
//uses menus to bring up child customer forms and the table
//form.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantControl;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace C__Prog_8
{
    public partial class RestaurantForm : Form
    {
        //create table form
        TableForm table = new TableForm();

        //dictionaries for holding the name and price of each type
        //of menu item: beverage, appetizer, entree, and dessert
        Dictionary<string, double> bev = new Dictionary<string, double>();
        Dictionary<string, double> app = new Dictionary<string, double>();
        Dictionary<string, double> ent = new Dictionary<string, double>();
        Dictionary<string, double> des = new Dictionary<string, double>();

        //number of customer forms
        public static int cNum = 0;
        //determines whether the menu has been loaded or not
        public bool menuLoaded = false;

        public RestaurantForm()
        {
            InitializeComponent();
        }

        //event handler for clicking load menu
        private void loadMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string for each line in txt file
            string line;
            //lets user search for a menu to load
            OpenFileDialog ofd = new OpenFileDialog();

            DialogResult dr = ofd.ShowDialog();

            //if user clicks cancel, returns
            if (dr != DialogResult.OK)
                return;

            //try to process each line of menu 
            try
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                line = sr.ReadLine();

                //loop for processing each line until end of file
                while(line != null)
                {
                    //split each line by double space
                    string[] args = line.Split(new[] { "  " }, StringSplitOptions.None);
                    
                    //if statements determine item type and puts item in
                    //appropriate dictionary
                    if (args[1] == "Beverage")
                    {
                        bev.Add(args[0], Double.Parse(args[2]));
                    }
                    if (args[1] == "Appetizer")
                    {
                        app.Add(args[0], Double.Parse(args[2]));
                    }
                    if (args[1] == "Entree")
                    {
                        ent.Add(args[0], Double.Parse(args[2]));
                    }
                    if (args[1] == "Dessert")
                    {
                        des.Add(args[0], Double.Parse(args[2]));
                    }

                    //reads next line
                    line = sr.ReadLine();
                }

               
                sr.Close();

                //menu has been loaded, can now add customers
                menuLoaded = true;
            }

            //catches any exception and displays appropriate error message
            catch(Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Exception Thrown", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        //event handler for clicking add customer
        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if menu has not been loaded, cannot add customer. error message
            if(!menuLoaded)
            {
                MessageBox.Show("Must load a menu before adding a customer", "No Menu Loaded", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if already have 4 customers, cannot add any more. error message
            if(cNum == 4)
            {
                MessageBox.Show("Cannot have more than 4 customers", "Max Customers Reached", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //increment customer counter
            cNum++;

            //updating labels for customers on table form
            if(cNum == 1)
                TableForm.tform.clabel.Text = cNum.ToString() + " customer";
            else
                TableForm.tform.clabel.Text = cNum.ToString() + " customers";

            //changes layout of child forms
            this.LayoutMdi(MdiLayout.ArrangeIcons);

            //instantiates new child form with name "customer" + child 
            //number. passes each dictionary to the child form so they can be
            //used
            CustomerForm customer = new CustomerForm("Customer " + cNum, bev, app,
                ent, des);
            customer.MdiParent = this;
            //shows child form
            customer.Show();           
        }

        //event handler for clicking exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes app
            this.Close();
        }

        //event handler for clicking show table
        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //shows table form
            table.Show();
        }

        //event handler for clicking save table order
        private void saveTableOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //allows user to choose file to save to
            SaveFileDialog sfd = new SaveFileDialog();
            DialogResult dr = sfd.ShowDialog();

            //if user clicks cancel, return
            if (dr != DialogResult.OK)
                return;

            //tries to print data from table form to output file
            try
            {
                //allows writing to output file
                FileStream output = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(output);

                //for each item in listview on table form, write each item and price
                //to output file and format
                foreach(ListViewItem item in TableForm.tform.lv1.Items)
                {
                    sw.WriteLine(item.Text.PadRight(20) + item.SubItems[1].Text.PadLeft(5));
                }

                sw.WriteLine("\n\n\n\n");

                //prints out final totals to output file
                sw.WriteLine("Subtotal:".PadRight(20) + TableForm.tform.rcon.GetSub().PadLeft(5));
                sw.WriteLine("Tax:".PadRight(20) + TableForm.tform.rcon.GetTax().PadLeft(5));
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Total:".PadRight(20) + TableForm.tform.rcon.GetTot().PadLeft(5));

                sw.Close();
            }

            //catches all exceptions and displays appropriate error message
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Exception Thrown", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //event handler for clicking about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //message tells user about the app
            MessageBox.Show("This is an MDI app that manages restaurant table orders.\n" +
                "Developed by Ryan Crawford at MSU Texas", "About this app", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
