using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuizApp_1._0
{
    public partial class EntryForm : Form
    {
        private int currentID;
        private string filename = "Q.xml";
        private XDocument doc;
        List<string> Ids;
        private bool autoID = false;
        public EntryForm()
        {
            InitializeComponent();
            Move thisform = new Move(this, panel1);
            thisform.MakeFromDraggableViaControlOr();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(textID.Text==""|textQes.Text=="" |
                textOp1.Text==""|textOp2.Text==""|
                textOp3.Text==""|textOp4.Text==""|
                textCorrect.Text=="")
            {
                markRedIFEmpty();
                MessageBox.Show("Fill All Box");
                
            }
            else
            {
                if (!Ids.Contains(textID.Text))
                {

                    XmlMethods.InsertNewField(filename, textID.Text,
                        textQes.Text, textOp1.Text, textOp2.Text, textOp3.Text,
                        textOp4.Text, textCorrect.Text, textDescrip.Text);
                    currentID++;
                    MessageBox.Show("Saved!");
                    cleanAll(); //clear all the option and question text box  for next entry
                    resetColor(); //reset the color to blue.
                    if (autoID ==false)
                    {
                        textID.Text = (getMaxID() + 1).ToString();
                    }
                    updateList();
                }
                else
                {
                    MessageBox.Show("ID Already There!");
                }
            }

            
        }



        public  int getNumberOfElement()
        {

           

            IEnumerable<string> names = from Contactlist in doc.Descendants("Question").Distinct()
                                        select Contactlist.FirstAttribute.
                                        Value;

            List<String> DistinctNames = new List<string>();
            foreach (var n in names.Distinct())
            {
                DistinctNames.Add(n);
            }
            int number = DistinctNames.Count;

            return number;
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            doc = XDocument.Load(filename);

           
            currentID =getMaxID();
           textID.Text = (currentID+1).ToString();

            Console.WriteLine("Max is {0}",getMaxID());
            
            Console.WriteLine("Next Id is {0}",getMaxID()+1);
            
            updateList();
           
            textID.Enabled = false;
            
        }


        private void cleanAll()
        {
            textQes.Text = "";
            textOp1.Text = "";
            textOp2.Text = "";
            textOp3.Text = "";
            textOp4.Text = "";
            textCorrect.Text = "";
            textDescrip.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Id = listBox1.SelectedItem.ToString();
            DialogResult deleteD = MessageBox.Show("Do You want to delete " + Id, "Delete",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(deleteD==DialogResult.Yes)
            {
                XmlMethods.DeleteField(filename, Id);
                updateList();     
                currentID = getMaxID()+1;
               


                textID.Text = currentID.ToString();

            }
        }

        private void updateList()
        {
            Ids = XmlMethods.getIDs(filename);
            listBox1.DataSource = Ids;
            listBox1.Sorted = true;
        }
        public int getMaxID()
        {
            List<int> id = new List<int>(100);
            Ids = XmlMethods.getIDs(filename);
            foreach (string item in Ids)
            {
                id.Add(int.Parse(item));

            }
            int getLastElement = id.Max();
           // Console.Write("max is" + getLastElement);
            return getLastElement;
        }
         private int howManyNumber2()
        {
            Ids = XmlMethods.getIDs(filename);
            int cur = Ids.Count();
            string getLastElement = Ids[cur-1];
            return int.Parse(getLastElement);
        }

        private void checkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if(autoID==false)
            {
                textID.Enabled = true;
                autoID = true;
            }
            else
            {
                textID.Enabled = false;
                textID.Text = (getMaxID() + 1).ToString();
                autoID = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IDs = (string)listBox1.SelectedItem;
            toolTip1.Show(string.Format("Question No {0}\n{1}\n{2}\n{3}\n{4}\n{5}\nAns: {6}", IDs,
                XmlMethods.getQuention(filename, "Qs", IDs),
                XmlMethods.getQuention(filename, "Op1", IDs),
                XmlMethods.getQuention(filename, "Op2", IDs),
                XmlMethods.getQuention(filename, "Op3", IDs),
                XmlMethods.getQuention(filename, "Op4", IDs),
                XmlMethods.getQuention(filename, "Ans", IDs)

                ),listBox1,1000);
           
        }
        private void markRedIFEmpty()
        {

            if (textQes.Text == "") pA1.BackColor = Color.Red; else pA1.BackColor = Color.LimeGreen; 
            if ( textOp1.Text == "") pA2.BackColor = Color.Red;else pA2.BackColor = Color.LimeGreen;
            if ( textOp2.Text == "") pA3.BackColor = Color.Red;else pA3.BackColor = Color.LimeGreen;
            if ( textOp3.Text == "") pA4.BackColor = Color.Red;else pA4.BackColor = Color.LimeGreen;
            if ( textOp4.Text == "") pA5.BackColor = Color.Red;else pA5.BackColor = Color.LimeGreen;
            if ( textCorrect.Text == "") pA6.BackColor = Color.Red; else pA6.BackColor = Color.LimeGreen;
            if (textID.Text == "") pA7.BackColor = Color.Red;else pA7.BackColor = Color.LimeGreen;
        }
        private void resetColor()
        {

            if (textQes.Text == "") pA1.BackColor = Color.DodgerBlue;
            if (textOp1.Text == "") pA2.BackColor = Color.DodgerBlue;
            if (textOp2.Text == "") pA3.BackColor = Color.DodgerBlue;
            if (textOp3.Text == "") pA4.BackColor = Color.DodgerBlue;
            if (textOp4.Text == "") pA5.BackColor = Color.DodgerBlue;
            if (textCorrect.Text == "") pA6.BackColor = Color.DodgerBlue;
            if (textID.Text == "") pA7.BackColor = Color.DodgerBlue; 
        }

        private void textOp3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pA6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pA7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pA3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pA4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textDescrip_TextChanged(object sender, EventArgs e)
        {

        }

        private void pA5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pA1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pA2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textOp4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textQes_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textOp1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textCorrect_TextChanged(object sender, EventArgs e)
        {

        }

        private void textOp2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
