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
       
        public Form1()
        {
            InitializeComponent();

            //Compilation of reports
            folderCreation.createMainFolderClass();
            folderCreation.createReadMeFolderClass();
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
            if (button1.Text == "Reset")
            {
                // reset the form, to its original form
                this.WindowState = FormWindowState.Normal;
                this.Controls.Clear();
                this.InitializeComponent();
            }
            else
            {
                button1.Text = "Reset";                

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
                
                // adjust form design
                this.WindowState = FormWindowState.Maximized;
                this.MaximizeBox = false;

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
                            getCord = generalClass.chartGenerator(this, 0, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] } , skillHeaderContent);
                        }
                        else if (getCord == 76)
                        {
                            //MessageBox.Show("B");
                            getCord = generalClass.chartGenerator(this, 76, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] }, skillHeaderContent);
                        }
                        else if (getCord != 76)
                        {
                            //MessageBox.Show("C");
                            getCord = generalClass.chartGenerator(this, getCord, i, empCount, skillCount, empNameList, new string[] { splitGrade[i] }, skillHeaderContent);
                        }
                    }
                }                                                       
            }
        }

        
    }
}
