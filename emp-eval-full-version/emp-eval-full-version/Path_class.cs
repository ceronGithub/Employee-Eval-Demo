using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emp_eval_full_version
{
    internal class Path_class
    {
        // allow the user to browse files
        OpenFileDialog browseFile = new OpenFileDialog();
        public string browseFileFunction()
        {
            string path = "";
            // allow text, and excel file only
            browseFile.Filter = "Document File |*.txt;*.xlsx";       
            // if click ok          
            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                // the path will show on the textbox
                path = Path.GetFullPath(browseFile.FileName);
            }
            return path;
        }

        public List<string> fileContent(string path)
        {            
            // store all data to array.
            return File.ReadLines(path).ToList();
        }        
    }
}
