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
        string mainFolderPath = @"" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\Report";
        string reportFolderPath, excelFileFolderPath, pictureFolderPath, readMeFolderPath;

        public string MainFolderFunction()
        {
            return mainFolderPath;
        }
        public string ReadMeFolderFunction()
        {
            return readMeFolderPath = MainFolderFunction() + "\\ReadMe";
        }
        public string ReportFolderFunction() {  return reportFolderPath = MainFolderFunction() + "\\Report on - " + DateTime.Now.ToString("MM-dd-yyyy"); }
        public string ExcelFileFolderFunction() { return excelFileFolderPath = ReportFolderFunction() + "\\Excel Report"; }
        public string pictureFolderFunction() { return pictureFolderPath = ReportFolderFunction() + "\\Graphs"; }

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

        public void createExcelFolderClass()
        {
            if (!Directory.Exists(ExcelFileFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(ExcelFileFolderFunction());                
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }

        public void createPictureFolderClass()
        {
            if (!Directory.Exists(pictureFolderFunction()))
            {
                // create folder
                Directory.CreateDirectory(pictureFolderFunction());                
            }
            else
            {
                //MessageBox.Show("Folder compilation is existing.");
            }
        }
    }
}
