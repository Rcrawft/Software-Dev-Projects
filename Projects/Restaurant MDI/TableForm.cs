//Ryan Crawford
//12/4/2024
//4143 Dr. Stringfellow
//This is the code for the table form which shows the 
//final list of items and prices for the table.

using RestaurantControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__Prog_8
{
    public partial class TableForm : Form
    { 
        //create static reference for TableForm to be 
        //used by other forms
        public static TableForm tform;
        //create reference for ListView to be used by
        //other forms
        public ListView lv1;
        //create reference for RestaurantControl to be used 
        //by other forms
        public RestaurantControl.RestaurantControl rcon;
        //create reference for customer label to be used 
        //by other forms
        public Label clabel;
        
        //constructor for TableForm
        public TableForm()
        {
            InitializeComponent();
            //displays columns of ListView
            tableListView.View = View.Details;
            //reference for TableForm to be used by other forms
            tform = this;
            //reference for tableListView to be used by other forms
            lv1 = tableListView;
            //reference for tableTotalsControl to be used by other forms
            rcon = tableTotalsControl;
            //reference for customersLabel to be used by other forns
            clabel = customersLabel;
        }

        private void TableForm_Load(object sender, EventArgs e)
        {

        }
    }
}
