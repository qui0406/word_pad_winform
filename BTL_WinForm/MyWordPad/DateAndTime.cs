using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace MyWordPad
{
    public partial class DateAndTime : Form
    {
        DateTime dt = DateTime.Now;
        public static String getDate = "";
        public DateAndTime()
        {
            InitializeComponent();
        }


        private void LoadListBox()
        {
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            String dayOfWeek = dt.DayOfWeek.ToString();
            listDateTime.Items.Add(String.Format(@"{0}/{1}/{2}", day.ToString(), month.ToString(), year.ToString()));
            listDateTime.Items.Add(String.Format(@"{0}/{1}/{2}", day.ToString(), month.ToString(), year.ToString().Substring(2)));
            listDateTime.Items.Add(String.Format(@"{0}-{1}-{2}", day.ToString(), month.ToString(), year.ToString().Substring(2)));
            listDateTime.Items.Add(String.Format(@"{0}-{1}-{2}", day.ToString(), month.ToString(), year.ToString()));
            listDateTime.Items.Add(dt.ToLocalTime().ToString().Split(' ')[1]);
        }

        private void DateTime_Load(object sender, EventArgs e)
        {
            LoadListBox();          
        }

        private void listDateTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listDateTime_SelectedValueChanged(object sender, EventArgs e)
        {
            getDate=listDateTime.SelectedItem.ToString();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getDate="";
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
