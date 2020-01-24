using System;
using System.Windows.Forms;

namespace QuizApp_1._0
{
    public partial class QuizForm : Form
    {
       
        public string file = "Q.xml";     //the filename \ file path
        private string question = "Qs";  // the question child tag
        private string Op_1 = "Op1";    // the question child tag
        private string Op_2 = "Op2";   // the question child tag
        private string Op_3 = "Op3";  // the question child tag
        private string Op_4 = "Op4"; // the question child tag
        private string Ans = "Ans"; // the question child tag
        private string Des = "Des";// the question child tag
        private bool isAlreadySeen = false; //if true the answ button will be disable
        private bool isAnsShown = false; //for showing\hiding the answar panel
        public string givnAns; //the given answ by the user
        private int FinalVerdict = 0; //the current\final score
        public string[] ID;/*{ "1", "2", "3", "4","5","6","7","8","9","10","11","12","13" }; //question numbers use as ID tag*/
        int Ite = 1; //iterator for ID array
        public bool Op1, Op2, Op3, Op4; //the ans represented as true or false
        public static int p = WelcomeForm.NumberOFQ;//number of queation you want from welcome screen
        public string[] JS=new string[p];
        

        


        public QuizForm()
        {
            InitializeComponent();
            ID = generateQNoArray(JS, p);
            Move thisForm = new Move(this,panel1);
            thisForm.MakeFromDraggableViaControlOr();
          


        }
        private void QuizForm_Load(object sender, EventArgs e)
        {
            
        


            XmlMethods.LoadXDocumnet(file); //Load the document (file creating remove when catch exception).
            setValuesToControl(0); //set the first question fro file
            lblDescription.Text = XmlMethods.getQuention(file, Des,"1");
            lblQRemaining.Text = "Question NO. : " + 1 + "/" + ID.Length; //set the first question number
            
    }



        



        /*----------------------Buttons Action-----------------------*/
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Ite > 1)
            {
                Ite--;

                setValuesToControl(Ite-1);
                lblQRemaining.Text = "Question NO. : " +
                   (Ite) + "/" + ID.Length;
                Console.WriteLine("Now I id {0}", Ite);
            }
            else
            {
                MessageBox.Show("Last One !");
            }
           

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Ite <=( ID.Length-1))

            {
                setValuesToControl(Ite); //set the values and ans to the control
                lblQRemaining.Text = "Question NO. : " + 
                    (Ite+1) + "/" + ID.Length; //chnge the current question number of label

                if (givnAns==Convert.ToString(true))// check if ans is true
                {
                    FinalVerdict+=1;
                    QuestCount.Text = Convert.ToString(FinalVerdict);
                    
                }
               
                Ite++;
                CleanAll(); //clear all option box

                /* To stop cheating and Warn! */
                if (isAnsShown == false)
                {
                    
                    EnableAll();
                }
                else MessageBox.Show("Answar Is Open ! Close It!!"); //enable the option box
                
            }
            else
            {
                MessageBox.Show("You Have Completed!");
            }


        }

        private void btnCleanAll_Click(object sender, EventArgs e) //clear the ans and selection
        {
            CleanAll();
            
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (givnAns == Convert.ToString(true)) MessageBox.Show(string.Format("You Got \n {0}", FinalVerdict + 1)); //for last question
            else MessageBox.Show(string.Format("You Got \n {0}", FinalVerdict));

        }
        /*----------------------End Buttons Action-----------------------*/






        /*------------------------------Option Action--------------------*/
        private void radioOption1_CheckedChanged(object sender, EventArgs e)
        {
            givnAns = Convert.ToString(checkAns(retunAns(lblAns), radioOption1.Text));
        }

        private void radioOption2_CheckedChanged(object sender, EventArgs e)
        {
            givnAns = Convert.ToString(checkAns(retunAns(lblAns), radioOption2.Text));
        }

        private void radioOption3_CheckedChanged(object sender, EventArgs e)
        {
            givnAns= Convert.ToString(checkAns(retunAns(lblAns), radioOption3.Text));
        }

        private void radioOption4_CheckedChanged(object sender, EventArgs e)
        {
        givnAns = Convert.ToString(checkAns(retunAns(lblAns), radioOption4.Text));

        }
       

        
        private void checkShowAns_Click(object sender, EventArgs e)
        {
            //show the answer panel
            // if shown the options will remain disable untill next question take
            //palce
            if (isAnsShown == false)
            {
                groupAns.Show();
                checkShowAns.Text = "Hide Answare";
                isAnsShown = true;
                isAlreadySeen = true;
                if (isAlreadySeen) DisableAll();
            }
            //hide the answer panel
            else { checkShowAns.Text = "Show Answare"; groupAns.Hide(); isAnsShown = false; }
        }
        private void checkShowMarks_Click(object sender, EventArgs e)
        {
            QuestCount.Show(); //show the current marks
            label2.Show();
        }
        /*----------------------------End Option Action------------------------*/







        /*-------------------------Methods-------------------------*/


        private string retunOpsAns(RadioButton btn)
        {
            string ans = btn.Text;
            return ans;
        } //get the ans from the radio button
        private string retunAns(Label AnsLbl)
        {
            string ans = AnsLbl.Text;
            return ans;
        } //get the ans from the label
        private bool checkAns(string CorrectAns, string GivenAns)
        {
            if (CorrectAns == GivenAns)
            {
                return true;
            }
            else
            {
                return false;
            }
        } //check the ans by comparing it with the ans
        private void setValuesToControl(int IDs)
        {
            lblQuestion.Text = ID[IDs] + ".\n" + XmlMethods.getQuention(file, question, ID[IDs]);
            radioOption1.Text = XmlMethods.getQuention(file, Op_1, ID[IDs]);
            radioOption2.Text = XmlMethods.getQuention(file, Op_2, ID[IDs]);
            radioOption3.Text = XmlMethods.getQuention(file, Op_3, ID[IDs]);
            radioOption4.Text = XmlMethods.getQuention(file, Op_4, ID[IDs]);
            lblAns.Text = XmlMethods.getQuention(file, Ans, ID[IDs]);
            lblDescription.Text = XmlMethods.getQuention(file, Des, ID[IDs]);
        } //set all ans and question to their control

        private void btnExit_Click(object sender, EventArgs e)
        {

            // this.Close();
            Application.Exit();
        }

        private void CleanAll()//clear the ans and selection
        {
            radioOption1.Checked = false;
            radioOption2.Checked = false;
            radioOption3.Checked = false;
            radioOption4.Checked = false;
            givnAns = string.Empty;

        }
        private void DisableAll()//disable this contol if the
        {
            radioOption1.Enabled = false;
            radioOption2.Enabled = false;
            radioOption3.Enabled = false;
            radioOption4.Enabled = false;


        }
        private void EnableAll()//clear the ans and selection
        {
            radioOption1.Enabled = true;
            radioOption2.Enabled = true;
            radioOption3.Enabled = true;
            radioOption4.Enabled = true;


        }
        private   string[] generateQNoArray(string[] Blank, int NoOFQ)
        {
           
            for(int i=0;i<NoOFQ;i++)
            {
                Blank[i] = (i+1).ToString();
               // Console.WriteLine(Blank[i]);

            }
            return Blank;

        }

        /*-------------------------Methods Ends-------------------------*/
    }
}
