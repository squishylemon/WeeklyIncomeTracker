using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeeklyIncomeTracker
{

    public partial class IncomeTracker : Form
    {
        public string CurName;
        public float AmEarned;
        public string curItem;
        public float HrsWorked;
        public float Payout;
        public float Payed;
        public float addfund;
        public float ExtraFUnds;
        public string fixtext;
        public ArrayList AlDateSaveItems;

        public IncomeTracker()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveMods();
            this.Close();
        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
           



            Form2 Frm2 = new Form2();
            Frm2.Awnsered = false;
            Frm2.dq = true;
            Frm2.DESCRIPTION = "What is the name of the completed task. e.g. Dishes Washed";
            Frm2.timeoption = false;
            Frm2.ShowDialog();
            Frm2.Refresh();
            if (Frm2.OUTPUT != null || Frm2.OUTPUT2 != null)
            {
                if (Frm2.OUTPUT2 != null)
                {
                    if (CurName == "Alexander")
                    {
                        Properties.Settings.Default.AlexanderTasksName.Add(Frm2.OUTPUT);
                        Properties.Settings.Default.AlexanderTasksTime.Add(Frm2.OUTPUT2);
                        Properties.Settings.Default.AlexanderTasksDate.Add("" + DateTime.Now);
                        LoadAlex();
                        SaveSetts();
                    }
                    else
                    {
                        Properties.Settings.Default.AwrynTasksName.Add(Frm2.OUTPUT);
                        Properties.Settings.Default.ArwynTasksTime.Add(Frm2.OUTPUT2);
                        Properties.Settings.Default.ArwynTasksDate.Add("" + DateTime.Now);
                        LoadArwyn();
                        SaveSetts();
                    }

                    RefreshData();
                } else
                {
                     
                    
                        MessageBox.Show("Invalid Time Input!");
                    
                }
            }
            else
            {
                MessageBox.Show("Invalid Name Input!");
            }
            
            
        }
        public void RefreshData()
        {
            AmEarned = 0;
            foreach (var item in TimeToCompleteTask.Items)
            {
                AmEarned = AmEarned + float.Parse(item.ToString());
            }
            HrsWorked = AmEarned;
            AmEarned = AmEarned * 15;
            if (CurName == "Alexander")
            {
                AmEarned = AmEarned + Properties.Settings.Default.AlexanderAddedAmount;
                AmEarned = AmEarned - Properties.Settings.Default.AlexanderPayedAmount;
            } else
            {
                AmEarned = AmEarned + Properties.Settings.Default.ArwynAddedAmount;
                AmEarned = AmEarned - Properties.Settings.Default.ArwynPayedAmount;
            }
          
            label1.Text = "" + CurName + " Has Earned " + AmEarned.ToString("c") + " For " + HrsWorked + "Hrs Of Work";
            if (CurName == "Alexander")
            {
                label2.Text = "Paid : " + Properties.Settings.Default.AlexanderPayedAmount.ToString("C") + " Extra Funds : " + Properties.Settings.Default.AlexanderAddedAmount.ToString("C");
            } else
            {
                label2.Text = "Paid : " + Properties.Settings.Default.ArwynPayedAmount.ToString("C") + " Extra Funds : " + Properties.Settings.Default.ArwynAddedAmount.ToString("C");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            LoadArwyn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            LoadAlex();
        }

        public void LoadAlex()
        {


            DateOfCompletionBox.DataSource = null;
            DateOfCompletionBox.DataSource = Properties.Settings.Default.AlexanderTasksDate;
            TimeToCompleteTask.DataSource = null;
            TimeToCompleteTask.DataSource = Properties.Settings.Default.AlexanderTasksTime;
            CompletedTaskName.DataSource = null;
            CompletedTaskName.DataSource = Properties.Settings.Default.AlexanderTasksName;
            


            CurName = "Alexander";
            RefreshData();
        }
        public void LoadArwyn()
        {


            DateOfCompletionBox.DataSource = null;
            DateOfCompletionBox.DataSource = Properties.Settings.Default.ArwynTasksDate;
            TimeToCompleteTask.DataSource = null;
            TimeToCompleteTask.DataSource = Properties.Settings.Default.ArwynTasksTime;
            CompletedTaskName.DataSource = null;
            CompletedTaskName.DataSource = Properties.Settings.Default.AwrynTasksName;



            CurName = "Arwyn";
            RefreshData();
        }
        public void SaveSetts()
        {
            String hourMinute = DateTime.Now.ToString("HH:mm");
            label3.Text = "Last Save - " + hourMinute;
            Properties.Settings.Default.LastSave = label3.Text;
            Properties.Settings.Default.Save();
            

           
            
            
        }
        

        public void SaveMods()
        {

            if (CurName != null)
            {
                if (CurName == "Alexander")
                {
                    var strsdate = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in DateOfCompletionBox.Items)
                        strsdate.Add(obj.ToString());
                    Properties.Settings.Default.AlexanderTasksDate = strsdate;
          
                    var strsname = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in CompletedTaskName.Items)
                        strsname.Add(obj.ToString());
                    Properties.Settings.Default.AlexanderTasksName = strsname;
                  
                    var strstime = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in TimeToCompleteTask.Items)
                        strstime.Add(obj.ToString());
                    Properties.Settings.Default.AlexanderTasksTime = strstime;
                    SaveSetts();
                  
                }
                else
                {
                    var strsdate = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in DateOfCompletionBox.Items)
                        strsdate.Add(obj.ToString());
                    Properties.Settings.Default.ArwynTasksDate = strsdate;

                    var strsname = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in CompletedTaskName.Items)
                        strsname.Add(obj.ToString());
                    Properties.Settings.Default.AwrynTasksName = strsname;

                    var strstime = new System.Collections.Specialized.StringCollection();
                    foreach (var obj in TimeToCompleteTask.Items)
                        strstime.Add(obj.ToString());
                    Properties.Settings.Default.ArwynTasksTime = strstime;
                    SaveSetts();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Payout = float.Parse(textBox2.Text);
            if (CurName == "Alexander")
            {
                Properties.Settings.Default.AlexanderPayedAmount = Properties.Settings.Default.AlexanderPayedAmount + Payout;
            } else
            {
                Properties.Settings.Default.ArwynPayedAmount = Properties.Settings.Default.ArwynPayedAmount + Payout;
            }
            textBox2.Text = "";
            SaveSetts();
            RefreshData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addfund = float.Parse(textBox3.Text);
            if (CurName == "Alexander")
            {
                Properties.Settings.Default.AlexanderAddedAmount = Properties.Settings.Default.AlexanderAddedAmount + addfund;
            } else
            {
                Properties.Settings.Default.ArwynAddedAmount = Properties.Settings.Default.ArwynAddedAmount + addfund;
            }
            
            textBox3.Text = "";
            SaveSetts();
            RefreshData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void IncomeTracker_Load(object sender, EventArgs e)
        {
            label3.Text = Properties.Settings.Default.LastSave;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete all tasks for " + CurName + "? Doing so will remove all settings inclduing recived money to tasks ect", "Delete " + CurName + "'s Tasks", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (CurName == "Alexander")
                {
                    Properties.Settings.Default.AlexanderAddedAmount = 0.0f;
                    Properties.Settings.Default.AlexanderPayedAmount = 0.0f;
                    Properties.Settings.Default.AlexanderTasksDate.Clear();
                    Properties.Settings.Default.AlexanderTasksName.Clear();
                    Properties.Settings.Default.AlexanderTasksTime.Clear();
                    SaveSetts();
                    LoadAlex();

                }
                else
                {
                    Properties.Settings.Default.ArwynAddedAmount = 0.0f;
                    Properties.Settings.Default.ArwynPayedAmount = 0.0f;
                    Properties.Settings.Default.ArwynTasksDate.Clear();
                    Properties.Settings.Default.AwrynTasksName.Clear();
                    Properties.Settings.Default.ArwynTasksTime.Clear();
                    SaveSetts();
                    LoadArwyn();
                }
            }
            
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove every saved task?", "Deleted Everything", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Properties.Settings.Default.AlexanderAddedAmount = 0.0f;
                Properties.Settings.Default.AlexanderPayedAmount = 0.0f;
                Properties.Settings.Default.AlexanderTasksDate.Clear();
                Properties.Settings.Default.AlexanderTasksName.Clear();
                Properties.Settings.Default.AlexanderTasksTime.Clear();
                SaveSetts();
                Properties.Settings.Default.ArwynAddedAmount = 0.0f;
                Properties.Settings.Default.ArwynPayedAmount = 0.0f;
                Properties.Settings.Default.ArwynTasksDate.Clear();
                Properties.Settings.Default.AwrynTasksName.Clear();
                Properties.Settings.Default.ArwynTasksTime.Clear();
                SaveSetts();
            }
            
        }
    }
}
