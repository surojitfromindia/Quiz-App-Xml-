using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp_1._0
{
    public partial class WelcomeForm : Form
    {
        public static int NumberOFQ = 0;
        int Number=0;
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void btnQuiz_Click(object sender, EventArgs e) {

            try
            {
                Number = int.Parse(textBox1.Text);
                NumberOFQ = Number;
            }
            catch(FormatException)
            {
                Number = 5;
                NumberOFQ = Number;
            }


            Console.WriteLine(Number);
            
            QuizForm quizForm = new QuizForm();
            quizForm.Show();
            
          
        }

        private void btnEnterData_Click(object sender, EventArgs e)
        {
            EntryForm entryForm = new EntryForm();
            entryForm.Show();
        }
    }
}
