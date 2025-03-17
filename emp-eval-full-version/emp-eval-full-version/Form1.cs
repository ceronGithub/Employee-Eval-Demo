using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace emp_eval_full_version
{
    public partial class Form1 : Form
    {

        // class/es
        General_class generalClass = new General_class();
        Create_folder_class folderCreation = new Create_folder_class();
        Path_class paths = new Path_class();
        int hValueGrade = 0, lValueGrade = 0;
        public Form1()
        {
            InitializeComponent();

            design();
            //Compilation of reports
            folderCreation.createMainFolderClass();
            folderCreation.createReadMeFolderClass();            
            //generalClass.dynamicRadioButton(this);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            design();
        }

        public (int setHGrade, int setLGrade) Reciever(string hValue, string lValue)
        {
            int getHGrade = 0, getLGrade = 0;
            getHGrade = Convert.ToInt32(hValue);
            getLGrade = Convert.ToInt32(lValue);
            hValueGrade = getHGrade; lValueGrade = getLGrade;
            highestGradeToolStripMenuItem.Text = "" + hValueGrade;
            lowestGradeToolStripMenuItem.Text = "" + lValueGrade;
            return (getHGrade, getLGrade);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            textBox1.Text = paths.browseFileFunction();
            button1.Enabled = true;
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" + folderCreation.MainFolderFunction());
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            int getCord = 0;
            if (button1.Text == "Reset Data")
            {                
                // reset the form, to its original form
                this.WindowState = FormWindowState.Normal;
                this.Controls.Clear();                
                this.InitializeComponent();
                design();
            }
            else
            {
                //change the button1 test
                button1.Text = "Reset Data";
                button1.Image = (new Bitmap(Resource1.reset, new Size(30, 20)));

                button2.Visible = true;

                // adjust form design
                this.WindowState = FormWindowState.Maximized;
                this.MaximizeBox = false;

                // fname, mname, lname, grades 
                List<string> headerContent = generalClass.headerContent(paths.fileContent(textBox1.Text));

                // grades header
                List<string> skillHeaderContent = generalClass.skillHeaderContent(headerContent);

                // fname, mname, lname
                List<string> empHeaderContent = generalClass.empHeaderContent(headerContent);

                //ceron matthew, pantino, calsena, 1,2,3,4,5/
                List<string> empInfoContent = generalClass.employeeContent(paths.fileContent(textBox1.Text));

                // number of skills : 5
                int skillCount = generalClass.skillCount(skillHeaderContent);
                
                // number of employees : 5
                int empCount = generalClass.empCount(empInfoContent);                

                // lname, fname + mname
                string nameItem = generalClass.employeeNameContent(empInfoContent);                
                List<string> empNameList = new List<string> { nameItem };

                // 1,2,3,4,5/
                string empGrade = generalClass.employeeGradeContent(empInfoContent);
                List<string> empGradeList = new List<string> { empGrade };                               

                List<string> test = new List<string>();
                foreach(string item in empGradeList)
                {
                    string[] splitGrade = item.Split('/');                    
                    // create chart according to how many employee is recorded
                    for (int i = 0; i < empCount; i++)
                    {                        
                        //MessageBox.Show(""+ splitGrade[i]);
                        empGradeList = new List<string>() { splitGrade[i] };
                        if (getCord == 0)
                        {
                            //MessageBox.Show("A");
                            getCord = generalClass.chartGenerator(this, 0, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] } , skillHeaderContent, hValueGrade, lValueGrade);
                        }
                        else if (getCord == 76)
                        {
                            //MessageBox.Show("B");
                            getCord = generalClass.chartGenerator(this, 76, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] }, skillHeaderContent, hValueGrade, lValueGrade);
                        }
                        else if (getCord != 76)
                        {
                            //MessageBox.Show("C");
                            getCord = generalClass.chartGenerator(this, getCord, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] }, skillHeaderContent, hValueGrade, lValueGrade);
                        }
                    }
                }                                                       
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generalClass.CHECKIFEXPORTMENUSTRIP();
        }

        private void setGradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }
        
        public void design()
        {
            button1.Height = 30;
            button1.Image = (new Bitmap(Resource1.investigate, new Size(30, 20)));
            button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            button2.Text = "Export-All-Image/s";
            button2.Height = 30;
            button2.Width = 135;
            button2.Image = (new Bitmap(Resource1.picture, new Size(30, 20)));
            button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        }
    }
}
