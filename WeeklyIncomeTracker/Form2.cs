using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WeeklyIncomeTracker
{
    public partial class Form2 : Form
    {
        public string OUTPUT;
        public string OUTPUT2;
        public float timeval;
        public bool  timeoption;
        public string DESCRIPTION;
        public bool IsHrs;
        public bool Awnsered;
        public bool dq;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Must Have Input");
              
            }else
            {
                if (dq == true)
                {
                    dq = false;
                    OUTPUT = textBox1.Text;

                    textBox1.Text = "";
                    Awnsered = false;
                    DESCRIPTION = "How Long Did it take? (if awnsering in hours click hrs if awnsering in minutes click mins)";
                    timeoption = true;
                    RefreshMe();


                }
                else
                {

                    if (textBox1.Text.Trim() != "")

                    {

                        try

                        {

                            int i = Convert.ToInt32(textBox1.Text.Trim());
                            if (IsHrs == true)
                            {
                                OUTPUT2 = textBox1.Text;
                                Awnsered = true;


                                this.Close();
                            }
                            else if (IsHrs == false)
                            {
                                timeval = float.Parse(textBox1.Text);
                                timeval = timeval / 60;
                                OUTPUT2 = timeval.ToString("n2");
                                Awnsered = true;
                                Form Frm = new Form();

                                this.Close();
                            }
                        }

                        catch
                        {

                            MessageBox.Show("Please enter a valid number");

                            textBox1.Text = "";

                        }

                    }
                    

                }
            }
          
       
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshMe();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsHrs = true;
            label2.Text = "Selected - Hrs";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsHrs = false;
            label2.Text = "Selected - Mins";
        }
        public void RefreshMe()
        {
            Awnsered = false;
            if (timeoption == false)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            label1.Text = DESCRIPTION;
            textBox1.Select();
        }
    }
}
