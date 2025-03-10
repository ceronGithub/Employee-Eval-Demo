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
using System.Reflection;

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
        List<string> employeeList = new List<string>();
        List<int> ttlNumbers = new List<int>();        

        public int chartGenerator(Form form, int locationCord, int loopData, int ttlOfEmployees, int ttlOfNumberOfSkills, List<string> empName, string[] empGrade, List<string> skillHeaderContent)
        {
            int locationCoordinates = locationCord == 0 ? locationCord = 76
                                    : locationCord == 76 ? locationCord += 506
                                    : locationCord = locationCord + 506;

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
                chartGenerator[loopData].Height = 500;
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
                
                legendSingle.BorderColor = Color.AntiqueWhite;
                legendSingle.BorderWidth = 2;
                legendSingle.BorderDashStyle = ChartDashStyle.Solid;
                legendSingle.InterlacedRows = true;
                legendSingle.InterlacedRowsColor = Color.NavajoWhite;

                legendSingle.Font = new Font("Arial", 8,FontStyle.Bold);
                legendSingle.ForeColor = Color.Black;

                legendSingle.ShadowColor = Color.Red;

                legendSingle.TitleFont = new Font("Arial", 18, FontStyle.Italic);                
                legendSingle.TableStyle = LegendTableStyle.Tall;                
                chartGenerator[loopData].Legends.Add(legendSingle);

                chartGenerator[loopData].Invalidate();

                chartGenerator[loopData].ChartAreas.Clear();

                chartArea[loopData] = new ChartArea();
                chartArea[loopData].Name = "ChartArea #" + loopData;

                chartArea[loopData].AxisX.Minimum = 0;
                chartArea[loopData].AxisX.Maximum = ttlOfNumberOfSkills + 2;

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

                // add stripline to y axis
                StripLine excellentLine = new StripLine();
                excellentLine.Interval = 0;
                excellentLine.IntervalOffset = (highest * .9);
                excellentLine.StripWidth = 10;
                excellentLine.BackColor = Color.NavajoWhite;
                excellentLine.TextLineAlignment = StringAlignment.Far;
                excellentLine.Font = new Font("Arial", 12, FontStyle.Bold);
                excellentLine.Text = "Out-standing";
                chartArea[loopData].AxisY.StripLines.Add(excellentLine);

                StripLine passLine = new StripLine();
                passLine.Interval = 0;
                passLine.IntervalOffset = (highest * .8);
                passLine.StripWidth = ((highest * .9)- (highest * .8));
                passLine.BackColor = Color.GhostWhite;
                passLine.TextLineAlignment = StringAlignment.Far;
                passLine.Font = new Font("Arial", 12,FontStyle.Bold);
                passLine.Text = "Pass-Margin";
                chartArea[loopData].AxisY.StripLines.Add(passLine);

                StripLine failLine = new StripLine();
                failLine.Interval = 0;
                failLine.IntervalOffset = 0;
                failLine.StripWidth = highest * .8;
                failLine.BackColor = Color.AntiqueWhite;
                failLine.TextLineAlignment = StringAlignment.Far;
                failLine.Font = new Font("Arial", 12, FontStyle.Bold);
                failLine.Text = "Failed-Margin";
                chartArea[loopData].AxisY.StripLines.Add(failLine);

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

                chartArea[loopData].AxisX.ScaleView.Position = 0;
                employeeList.Add(empNameContent[loopData]);
                ttlNumbers.Add(ttlNumberOfImages);
                ttlNumbers.Add(ttlOfEmployees);
            }            
            return locationCoordinates;       
        }        

        private void dynamicButtons(Form form, int ttlOfButtons, int loopData, int ttlOfImages, int locationCoordinates, Chart chart, string employeeName, List<Image> imageChart)
        {
            int height = 40;
            // print button
            Button[] printBtn = new Button[ttlOfButtons];
            printBtn[loopData] = new Button();
            printBtn[loopData].Text = "Print";
            printBtn[loopData].Width = 70;
            printBtn[loopData].Height = height;

            printBtn[loopData].Image = (new Bitmap(Resource1.printer, new Size(30, 20)));
            printBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            printBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            printBtn[loopData].Location = new System.Drawing.Point(150, locationCoordinates + 7);

            //printBtn[loopDate].Click += new EventHandler(print_click, loopDate);                      

            // savefile button
            Button[] saveFileBtn = new Button[ttlOfButtons];
            saveFileBtn[loopData] = new Button();
            saveFileBtn[loopData].Text = "Save-File as image";
            saveFileBtn[loopData].Width = 130;
            saveFileBtn[loopData].Height = height;

            saveFileBtn[loopData].Image = (new Bitmap(Resource1.image, new Size(30, 20)));
            saveFileBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            saveFileBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            saveFileBtn[loopData].Location = new System.Drawing.Point(230, locationCoordinates + 7);

            // excel button
            Button[] excelBtn = new Button[ttlOfButtons];
            excelBtn[loopData] = new Button();
            excelBtn[loopData].Text = "Export to";
            excelBtn[loopData].Width = 85;
            excelBtn[loopData].Height = height;

            excelBtn[loopData].Image = (new Bitmap(Resource1.excel_one, new Size(30, 20)));
            excelBtn[loopData].ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            excelBtn[loopData].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            excelBtn[loopData].Location = new System.Drawing.Point(370, locationCoordinates + 7);

            //Clicked event
            printBtn[loopData].Click += (sender, e) => print_click(sender, e, loopData);
            saveFileBtn[loopData].Click += (sender, e) => saveFile_click(sender, e, chart, imageChart, folderCreation.ReportFolderFunction(), ".jpg", employeeName, (loopData + 1), ttlOfImages);
            excelBtn[loopData].Click += (sender, e) => exportToExcel_click(sender, e);

            //add chart to the form
            form.Controls.Add(excelBtn[loopData]);
            form.Controls.Add(saveFileBtn[loopData]);
            form.Controls.Add(printBtn[loopData]);
        }

        public void dynamicRadioButton(Form form)
        {
            RadioButton[] rd = new RadioButton[8];
            for(int i = 0; i < rd.Length; i++)
            {                
                switch(i)
                {                    
                    case 0:
                        rd[i] = new RadioButton();
                        rd[i].Checked = true;
                        rd[i].Name = "rdColumns";
                        rd[i].Text = "Columns Chart";
                        rd[i].Location = new Point(500, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 80;                        
                        form.Controls.Add(rd[i]);
                    break;

                    case 1:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdClusteredColumn";
                        rd[i].Text = "Clustered Column Chart";
                        rd[i].Location = new Point(580, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 120;
                        form.Controls.Add(rd[i]);
                        break;

                    case 2:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdBarChart";
                        rd[i].Text = "Bar Chart";
                        rd[i].Location = new Point(700, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 50;
                        form.Controls.Add(rd[i]);
                        break;

                    case 3:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdAreaChart";
                        rd[i].Text = "Area Chart";
                        rd[i].Location = new Point(760, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 50;
                        form.Controls.Add(rd[i]);
                        break;

                    case 4:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdLineChart";
                        rd[i].Text = "Line Chart";
                        rd[i].Location = new Point(820, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 50;
                        form.Controls.Add(rd[i]);
                        break;

                    case 5:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdPieChart";
                        rd[i].Text = "Pie Chart";
                        rd[i].Location = new Point(880, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 50;
                        form.Controls.Add(rd[i]);
                        break;

                    case 6:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdScatterChart";
                        rd[i].Text = "Scatter Chart";
                        rd[i].Location = new Point(940, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 70;
                        form.Controls.Add(rd[i]);
                        break;

                    case 7:
                        rd[i] = new RadioButton();
                        rd[i].Name = "rdBubbleChart";
                        rd[i].Text = "Bubble Chart";
                        rd[i].Location = new Point(1010, 32);
                        rd[i].Height = 50;
                        rd[i].Width = 70;
                        form.Controls.Add(rd[i]);
                        break;                    
                }
            }
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
                MessageBox.Show("Folder(picture folder) Directory has been updated \n @" + path, "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        // ===========================================ABOUT IMAGES=========================================
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
                hheight += finalSize.Height;
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
        }


        //============================================EXPORT===============================================
        public void CHECKIFEXPORTMENUSTRIP()
        {
            //MessageBox.Show(string.Join("", imageList.Count));
            //create folder
            folderCreation.createProjectPictureFolderClass();
            //goal is to get the index 0 and 1 of the ttlNumberList
            saveAllFile(imageList, employeeList, ttlNumbers[0], ttlNumbers[1]);            
        }

        public void saveAllFile(List<Image> imageList, List<string> employeeName, int ttlOfSkills, int ttlOfEmp)
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].Save(folderCreation.AllPictureFolderFunction() + "\\output -" + i + "- chart.png", ImageFormat.Png);
            }             
            MessageBox.Show("Folder(All-Output-Pictures) Directory has been created/updated \n @" + folderCreation.AllPictureFolderFunction(), "Note!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}