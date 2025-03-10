using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emp_eval_full_version
{
    internal class Create_folder_class
    {
        string mainFolderPath = @"" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\Employee-Evaluation-Report";
        string reportFolderPath, allPictureFolderPath,excelFileFolderPath, pictureFolderPath, readMeFolderPath, employeeFolderPath, getEmployeeName;       
        public string setEmployeeName(string employeeName)
        {
            getEmployeeName = employeeName;            
            return getEmployeeName;
        }

        public string MainFolderFunction()
        {
            return mainFolderPath;
        }
        public string ReadMeFolderFunction()
        {
            return readMeFolderPath = MainFolderFunction() + "\\ReadMe";
        }
        public string ReportFolderFunction() 
        {  return reportFolderPath = MainFolderFunction() + "\\Report on - " + DateTime.Now.ToString("MM-dd-yyyy"); }

        public string AllPictureFolderFunction()
        { return allPictureFolderPath = ReportFolderFunction() + "\\All-Output-Pictures"; }
        
        public string employeeFolderFunction(string employeeName) 
        {
            employeeName = getEmployeeName;
            return employeeFolderPath = ReportFolderFunction() + "\\Evaluation of - " + employeeName; 
        }
        public string pictureFolderFunction()
        { return pictureFolderPath = employeeFolderFunction(getEmployeeName) + "\\Graphs"; }
        public string ExcelFileFolderFunction()
        { return excelFileFolderPath = employeeFolderFunction(getEmployeeName) + "\\Excel Report"; }

        //main folder = Employee-Evaluation-Report
        public void createMainFolderClass()
        {
            // this checks if the folder has been created. and if not create one
            // compilation of system output 
            if (!Directory.Exists(MainFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(MainFolderFunction());
                MessageBox.Show("Folder is created @ \n" + MainFolderFunction());
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        // readme folder
        public void createReadMeFolderClass()
        {
            // this checks if the folder has been created. and if not create one
            // compilation of system output 
            if (!Directory.Exists(ReadMeFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(ReadMeFolderFunction());
                MessageBox.Show("Folder is created @ \n" + ReadMeFolderFunction());
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        //Report on - date!
        public void createProjectFolderClass()
        {
            if (!Directory.Exists(ReportFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(ReportFolderFunction());
                MessageBox.Show("Folder is created @ \n" + ReportFolderFunction());
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        public void createProjectPictureFolderClass()
        {
            if (!Directory.Exists(AllPictureFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(AllPictureFolderFunction());
                MessageBox.Show("Folder is created @ \n" + AllPictureFolderFunction());
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        //employeename folder
        public void createEmployeeFolderClass()
        {
            if (!Directory.Exists(employeeFolderFunction(getEmployeeName)))
            {
                // create folder
                Directory.CreateDirectory(employeeFolderFunction(getEmployeeName));
                MessageBox.Show("Folder(employee) Directory has been created \n @" + employeeFolderFunction(getEmployeeName), "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        //graph inside the employee folder.
        public void createPictureFolderClass()
        {
            if (!Directory.Exists(pictureFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(pictureFolderFunction());
                MessageBox.Show("Folder(picture folder) Directory has been created \n @" + pictureFolderFunction(), "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // delete
                /*
                Directory.Delete(pictureFolderFunction());
                Directory.CreateDirectory(pictureFolderFunction());
                MessageBox.Show("Folder(picture folder) Directory has been refreshed \n @" + pictureFolderFunction(), "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                */
            }
        }

        public void createExcelFolderClass()
        {
            if (!Directory.Exists(ExcelFileFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(ExcelFileFolderFunction());
                MessageBox.Show("Folder(excel folder) Directory has been created \n @" + ExcelFileFolderFunction(), "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }
    }
}
