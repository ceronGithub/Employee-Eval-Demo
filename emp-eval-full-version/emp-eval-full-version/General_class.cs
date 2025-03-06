using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//chart
using System.Xml;
using System.Diagnostics.Eventing.Reader;
// right-click references search "System.Windows.Forms.DataVisualization" then click "System.Windows.Forms.DataVisualization"
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace emp_eval_full_version
{
    internal class General_class
    {
        Path_class paths = new Path_class();
        Create_folder_class folderCreation = new Create_folder_class();

        public List<string> headerContent(List<string> fileContents)
        {
            List<string> headercontent = fileContents.Take(1).ToList();
            return headercontent;
        }

        public List<string> skillHeaderContent(List<string> fileContents)
        {
            List<string> skillcontent = new List<string>();
            foreach (string item in fileContents)
            {
                string[] skillHeader = item.Split(',').Skip(3).ToArray();
                string putComma = string.Join(",", skillHeader);
                skillcontent.Add(putComma);
            }
            return skillcontent;
        }

        public int skillCount(List<string> fileContents)
        {
            int skillcontentcount = 0;
            foreach (string items in fileContents)
            {
                skillcontentcount = items.Split(',').Count();
            }
            return skillcontentcount;
        }

        public int empCount(List<string> fileContents)
        {
            int count = 0;
            foreach (string items in fileContents)
            {
                count += items.Split('/').Count() - 1;
            }
            return count;
        }




        public List<string> empHeaderContent(List<string> fileContents)
        {
            // emp = employee
            List<string> empcontent = new List<string>();
            foreach (string item in fileContents)
            {
                string[] empHeader = item.Split(',').Take(3).ToArray();
                string putComma = string.Join(",", empHeader);
                empcontent.Add(putComma);
            }
            return empcontent;
        }

        public List<string> employeeContent(List<string> fileContents)
        {
            List<string> empContent = new List<string>();
            foreach (string item in fileContents.Skip(1).ToList())
            {
                string[] empContentArr = item.Split(',').ToArray();
                // put comma inbetween datas
                string putComma = string.Join(",", empContentArr);
                // put comma to each end of row data.
                empContent.Add(putComma + "/");
            }
            return empContent;
        }

        public string employeeNameContent(List<string> fileContents)
        {
            string personNames = "";
            foreach (string employeeName in fileContents)
            {
                string[] seperateName = employeeName.Split(',');
                string[] getName = { string.Concat(seperateName[2], " ", seperateName[0], seperateName[1]) + "," };
                personNames += string.Concat(getName);
                //MessageBox.Show(string.Join(",", personInfo));
            }
            return personNames;
        }

        public string employeeGradeContent(List<string> fileContents)
        {
            string empGradeCollection = "";
            foreach (string skillItem in fileContents)
            {
                //MessageBox.Show(skillItem);
                // skips the 1-3 index of array
                // output : 123/
                string[] seperateSkillContent = skillItem.Split(',').Skip(3).ToArray();
                // output : 1,2,3/
                empGradeCollection += string.Join(",", seperateSkillContent);
                //MessageBox.Show(string.Join("", empGradeCollection));
            }
            return empGradeCollection;
        }

        // count how many emp are listed
        public int employeeContentCount(List<string> fileContents)
        {
            int empCount = 0;
            foreach (string item in fileContents)
            {
                string[] split = item.Split('/');
                empCount += split.Count() - 1;
            }
            return empCount;
        }

        //
        public int countGradeContent(List<string> fileContents)
        {
            int skillCount = 0;
            foreach (string item in fileContents)
            {
                string[] seperateSkillContent = item.Split(',').Skip(3).ToArray();
                skillCount = seperateSkillContent.Count();
            }
            return skillCount;
        }

        List<Image> imageList = new List<Image>();
        public int chartGenerator(Form form, int locationCord, int loopData, int ttlOfEmployees, int ttlOfNumberOfSkills, List<string> empName, string[] empGrade, List<string> skillHeaderContent)
        {
            int locationCoordinates = locationCord == 0 ? locationCord = 76
                                    : locationCord == 76 ? locationCord += 406
                                    : locationCord = locationCord + 406;

            Random randomClr = new Random();
            Chart[] chartGenerator = new Chart[ttlOfEmployees];
            ChartArea[] chartArea = new ChartArea[ttlOfEmployees];
            Series[] series = new Series[ttlOfNumberOfSkills];
            Legend[] legend = new Legend[ttlOfNumberOfSkills];
            int highest = 0, lowest = 0, currenthighest = 0;


            foreach (string empItem in empName)
            {
                string[] empNameContent = empItem.Split(',');

                chartGenerator[loopData] = new Chart();
                chartGenerator[loopData].Name = empNameContent[loopData] + " - Chart";
                chartGenerator[loopData].Titles.Add(string.Join("", empNameContent[loopData] + " - Chart"));
                chartGenerator[loopData].Height = 400;
                chartGenerator[loopData].Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
                chartGenerator[loopData].Location = new System.Drawing.Point(0, locationCoordinates);

                chartGenerator[loopData].Series.Clear();

                var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chartGenerator[loopData].Series.Add(series1);

                for (int s = 0; s < ttlOfNumberOfSkills; s++)
                {
                    Color colr = Color.FromArgb(randomClr.Next(1, 255), randomClr.Next(1, 255), randomClr.Next(1, 255));

                    foreach (string headerItem in skillHeaderContent)
                    {
                        string[] skillHeaderName = headerItem.Split(',');
                        /*
                         * This overlay the layout of the series.
                         * This only shows the legend/s of the series.
                         */
                        series[s] = new Series
                        {
                            Name = skillHeaderName[s],
                            Color = colr,
                            IsValueShownAsLabel = true,
                        };
                        chartGenerator[loopData].Series.Add(series[s]);

                        foreach (string item in empGrade)
                        {
                            string[] empGrades = item.Split(',');
                            if (Convert.ToInt32(empGrades[s]) > currenthighest)
                            {
                                currenthighest = Convert.ToInt32(empGrades[s]);
                                highest = currenthighest;
                            }

                            series1.Points.AddXY(skillHeaderName[s], empGrades[s]);
                            series1.Points[s].Color = colr;
                            series1.Points[s].Label = skillHeaderName[s] + " : " + empGrades[s];
                        }
                    }
                }


                Legend legendSingle = new Legend();
                legendSingle.Title = "Skill Legend/s";
                legendSingle.LegendStyle = LegendStyle.Table;
                chartGenerator[loopData].Legends.Add(legendSingle);

                chartGenerator[loopData].Invalidate();

                chartGenerator[loopData].ChartAreas.Clear();

                chartArea[loopData] = new ChartArea();
                chartArea[loopData].Name = "ChartArea #" + loopData;

                chartArea[loopData].AxisX.Minimum = 0;
                chartArea[loopData].AxisX.Maximum = ttlOfNumberOfSkills + 1;

                /*
                 * this makes the x-axis string customize.
                chartArea[loopData].AxisX.Title = "xxx";
                chartArea[loopData].AxisX.TitleAlignment = StringAlignment.Center;
                chartArea[loopData].AxisX.TextOrientation = TextOrientation.Rotated90;
                chartArea[loopData].AxisX.Interval = 1;
                */

                chartArea[loopData].AxisY.Minimum = lowest;
                chartArea[loopData].AxisY.Maximum = highest + 2;

                chartArea[loopData].AxisX.ScaleView.Zoom(0, 8);
                chartArea[loopData].AxisX.ScaleView.MinSize = 0;
                chartArea[loopData].CursorX.AutoScroll = true;
                chartArea[loopData].CursorX.IsUserEnabled = true;
                chartArea[loopData].AxisX.ScaleView.Zoomable = true;
                chartArea[loopData].AxisX.ScrollBar.Enabled = true;
                chartArea[loopData].AxisX.ScrollBar.IsPositionedInside = true;
                chartArea[loopData].AxisX.ScrollBar.Size = 20;
                chartArea[loopData].AxisX.ScrollBar.ButtonColor = Color.Silver;
                chartArea[loopData].AxisX.ScrollBar.LineColor = Color.Black;

                chartArea[loopData].AxisX.Title = "Grade Value (on each skill)";
                chartArea[loopData].AxisX.TitleAlignment = StringAlignment.Center;
                chartArea[loopData].AxisX.TitleForeColor = Color.Red;
                chartArea[loopData].AxisX.TitleFont = new Font("Sans Serif", 10, FontStyle.Bold);

                chartArea[loopData].AxisY.Title = "Grade Evaluation \n -------------- \n " + empNameContent[loopData];
                chartArea[loopData].AxisY.TitleAlignment = StringAlignment.Center;

                chartGenerator[loopData].ChartAreas.Add(chartArea[loopData]);

                chartGenerator[loopData].Visible = true;

                int ttlNumberOfImages = ttlOfNumberOfSkills - 5;
                dynamicButtons(form, ttlOfEmployees, loopData, ttlNumberOfImages, locationCoordinates, chartGenerator[loopData], string.Join("", empNameContent[loopData]), imageList);

                form.Controls.Add(chartGenerator[loopData]);

                // Convert chart to images.
                for (int si = 1; si <= ttlNumberOfImages; si++)
                {
                    chartArea[loopData].AxisX.ScaleView.Size = 5;
                    chartArea[loopData].AxisX.ScaleView.Position = si;

                    using (var ms = new MemoryStream())
                    {
                        chartGenerator[loopData].SaveImage(ms, ChartImageFormat.Png);
                        var bmp = System.Drawing.Bitmap.FromStream(ms);
                        imageList.Add(bmp);
                    }
                }
            }
            return locationCoordinates;
        }

        private void dynamicButtons(Form form, int ttlOfButtons, int loopData, int ttlOfImages, int locationCoordinates, Chart chart, string employeeName, List<Image> imageChart)
        {
            // print button
            Button[] printBtn = new Button[ttlOfButtons];
            printBtn[loopData] = new Button();
            printBtn[loopData].Text = "Print";
            printBtn[loopData].Width = 70;
            printBtn[loopData].Height = 30;

            printBtn[loopData].Image = (new Bitmap(Resource1.printer, new Size(30, 20)));
            printBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            printBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            printBtn[loopData].Location = new System.Drawing.Point(150, locationCoordinates + 7);

            //printBtn[loopDate].Click += new EventHandler(print_click, loopDate);                      

            // savefile button
            Button[] saveFileBtn = new Button[ttlOfButtons];
            saveFileBtn[loopData] = new Button();
            saveFileBtn[loopData].Text = "Save-File as image";
            saveFileBtn[loopData].Width = 140;
            saveFileBtn[loopData].Height = 30;

            saveFileBtn[loopData].Image = (new Bitmap(Resource1.savefile, new Size(30, 20)));
            saveFileBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            saveFileBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            saveFileBtn[loopData].Location = new System.Drawing.Point(230, locationCoordinates + 7);

            // excel button
            Button[] excelBtn = new Button[ttlOfButtons];
            excelBtn[loopData] = new Button();
            excelBtn[loopData].Text = "Export to";
            excelBtn[loopData].Width = 80;
            excelBtn[loopData].Height = 30;

            excelBtn[loopData].Image = (new Bitmap(Resource1.excel, new Size(30, 20)));
            excelBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            excelBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            excelBtn[loopData].Location = new System.Drawing.Point(380, locationCoordinates + 7);

            //Clicked event
            printBtn[loopData].Click += (sender, e) => print_click(sender, e, loopData);
            saveFileBtn[loopData].Click += (sender, e) => saveFile_click(sender, e, chart, imageChart, folderCreation.ReportFolderFunction(), ".jpg", employeeName, (loopData + 1), ttlOfImages);
            excelBtn[loopData].Click += (sender, e) => exportToExcel_click(sender, e);

            //add chart to the form
            form.Controls.Add(excelBtn[loopData]);
            form.Controls.Add(saveFileBtn[loopData]);
            form.Controls.Add(printBtn[loopData]);
        }





        // ======================================== CLICK-EVENT===========================================
        protected void print_click(object sender, EventArgs e, int employeeNumber)
        {
            printMethod(employeeNumber);
        }

        protected void saveFile_click(object sender, EventArgs e, Chart chart, List<Image> chartImages, string path, string fileExtension, string employeeName, int employeeNumber, int ttlOfImages)
        {
            //int ttlOfImageCollection = chartImages.Count();

            string addedPath = path + "\\Evaluation of - " + employeeName + "\\Graphs";
            //creates folder
            folderCreation.setEmployeeName(employeeName);
            folderCreation.createEmployeeFolderClass();
            folderCreation.createPictureFolderClass();
            folderCreation.createExcelFolderClass();

            Image finalImage = mergeImages(chartImages, employeeNumber, ttlOfImages, folderCreation.ReadMeFolderFunction(), employeeName, fileExtension);            

            //checks if image is existing.
            if (!File.Exists(addedPath + "\\single - " + employeeName + fileExtension))
            {
                finalImage.Save(addedPath + "\\single - " + employeeName + fileExtension, ImageFormat.Png);
                multipleImageSave(chartImages, employeeNumber, ttlOfImages, addedPath, employeeName, fileExtension);
            }
            else
            {
                //delete
                File.Delete(addedPath + "\\single - " + employeeName + fileExtension);               
                //create
                finalImage.Save(addedPath + "\\single - " + employeeName + fileExtension, ImageFormat.Png);
                multipleImageSave(chartImages, employeeNumber, ttlOfImages, addedPath, employeeName, fileExtension);                
            }
            //saveFile(chart, addedPath, fileExtension, employeeName);            
        }

        protected void exportToExcel_click(object sender, EventArgs e)
        {
            exportToExcel();
        }


        // ======================================== METHOD-EVENT===========================================
        private void printMethod(int employeeNumber)
        {
            MessageBox.Show("Print " + ((employeeNumber + 1) * 5));
        }
        private void saveFile(Chart chart, string folderPath, string fileExtension, string employeeName)
        {
            chart.SaveImage(folderPath + "\\" + employeeName + fileExtension, ChartImageFormat.Jpeg);
        }
        private void exportToExcel()
        {
            MessageBox.Show("Succesfully export to excel.");
        }

        public Image mergeImages(List<Image> imageList, int employeeNumber, int ttlOfImagess, string path, string employeeName, string fileExtension)
        {
            int startImageRange = (((employeeNumber * ttlOfImagess)) - ttlOfImagess);
            int endImageRange = (employeeNumber * ttlOfImagess);
            //MessageBox.Show("start : " + startImageRange + "\n end : " + endImageRange);

            // merge multiple images into 1 picture file.
            var finalSize = new Size();
            foreach (var image in imageList.Take(endImageRange).Skip(startImageRange))
            {
                if (image.Width > finalSize.Width)
                {
                    finalSize.Width = image.Width;
                }
                finalSize.Height += image.Height;
            }
            var outputImage = new Bitmap(finalSize.Width, finalSize.Height);
            using (var gfx = Graphics.FromImage(outputImage))
            {
                var y = 0;
                foreach (var image in imageList.Take(endImageRange).Skip(startImageRange))
                {
                    gfx.DrawImage(image, 0, y);
                    y += image.Height;
                }
            }
            return outputImage;
        }

        private void multipleImageSave(List<Image> imageList, int employeeNumber, int ttlOfImagess, string path, string employeeName, string fileExtension)
        {
            int startImageRange = (((employeeNumber * ttlOfImagess)) - ttlOfImagess);
            int endImageRange = (employeeNumber * ttlOfImagess);
            //Image[] outputImage = new Image[ttlOfImagess];
            
            var finalSize = new Size();
            foreach (var image in imageList.Take(endImageRange).Skip(startImageRange))
            {                                
                if (image.Width > finalSize.Width)
                {
                    finalSize.Width = image.Width;
                }
                finalSize.Height = image.Height ;               
            } 

            int y = 0;
            int hheight = 0;
            for (int im = 0; im < ttlOfImagess; im++)
            {
                y = 0;
                hheight += 400;
                var outputImage = new Bitmap(finalSize.Width, hheight);                
                using (var gfx = Graphics.FromImage(outputImage))
                {                                                                              
                    foreach (var image in imageList.Take(endImageRange).Skip(startImageRange))
                    {                        
                        gfx.DrawImage(image, 0, y);
                        y += image.Height;                       
                    }
                    outputImage.Save(path + "\\multi - " + im + employeeName + fileExtension, ImageFormat.Png);
                }                
            }

            // save individual
            List<Image> trial = new List<Image>(imageList.Take(endImageRange).Skip(startImageRange));
            for(int im = 0; im < ttlOfImagess; im++)
            {
                trial[im].Save(path + "\\indi - " + im + employeeName + fileExtension, ImageFormat.Png);
            }

            MessageBox.Show("Folder(picture folder) Directory has been updated \n @" + path, "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}