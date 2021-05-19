using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Schedule
{

        public partial class Schedule : Form
        {

            TableLayoutPanel newtablelayoutPanel = new TableLayoutPanel();
            Label dateInfo = new Label();
            public void ChangeSize(int width = 850, int height = 650) { this.Size = new Size(width, height); }//Define Winform Size
            public Schedule()
            {
                InitializeComponent();
                ChangeSize();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
               // CreateMenu();
                PanelAdder();
            }

            private void CreateMenu()
            {
                const int n = 5;
                string[] itemMenu;
                itemMenu = new string[n] { "Timetable", "Credits Calculator", "Menu 3", "Menu 4", "Menu 5" };

                MenuStrip strip = new MenuStrip();

                ToolStripMenuItem menuList = new ToolStripMenuItem("&MENU");
                menuList.BackColor = Color.OrangeRed;
                menuList.ForeColor = Color.White;


                for (int i = 0; i < n; i++)
                {
                    ToolStripMenuItem menuRow = new ToolStripMenuItem(itemMenu[i]);
                    menuRow.Click += new EventHandler(menuRow_Click);
                    menuList.DropDownItems.Add(menuRow);
                    strip.Items.Add(menuList);
                    this.Controls.Add(strip);
                }
            }

            // An event that is called from menu item.
            private void menuRow_Click(object sender, EventArgs e)
            {

            }


            private void PanelAdder()
            {
                int Winwidth = this.Size.Width;
                int Winheight = this.Size.Height;

                // Build current time  label
                dateInfo.Name = "TimeDateTable";
                DayOfWeek wk = DateTime.Today.DayOfWeek;
                string strFormat = "dd/MM/yy";
                dateInfo.Text = DateTime.Now.ToString(strFormat) + " " + wk.ToString();
                dateInfo.Location = new Point((Winwidth / 2) - (dateInfo.Width / 2), 100);
                Controls.Add(dateInfo);


                // Build schedule Table
                newtablelayoutPanel.Name = "ScheduleTable";
                newtablelayoutPanel.Size = new Size(700, 400);
                newtablelayoutPanel.ColumnCount = 7;
                newtablelayoutPanel.RowCount = 10;

                for (int i = 0; i < newtablelayoutPanel.ColumnCount; i++)
                {
                    if (i == 0)
                        newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
                    else
                        newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                }

                for (int i = 0; i < newtablelayoutPanel.RowCount; i++)
                {
                    if (i == 0)
                        newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
                    else
                        newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
                }
                newtablelayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                // Fill rows & calls
                DayOfWeek[] days = {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday };

                CultureInfo ci = new CultureInfo("en-US");
                for (int i = 0; i < newtablelayoutPanel.ColumnCount; i++)
                {
                    if (i == 0)
                        continue;
                    var abbr = ci.DateTimeFormat.GetAbbreviatedDayName(days[i]);
                    newtablelayoutPanel.Controls.Add(new Label() { Text = abbr.ToString(), Name = "Week_" + i, TextAlign = ContentAlignment.MiddleCenter }, i, 0);
                }

                for (int i = 0; i < newtablelayoutPanel.RowCount; i++)
                {
                if (i == 0)
                {
                    newtablelayoutPanel.Controls.Add(new Label() { Text = " ", Name = "ClassHour_" + i, TextAlign = ContentAlignment.BottomCenter }, 0, i);
                    continue;
                }
                newtablelayoutPanel.Controls.Add(new Label() { Text = (i-1).ToString(), Name = "ClassHour_" + i, TextAlign = ContentAlignment.BottomCenter }, 0, i);
            }

                newtablelayoutPanel.Location = new Point(((Winwidth / 2) - (newtablelayoutPanel.Width / 2)), ((Winheight / 2) - (newtablelayoutPanel.Height / 2)));

                var test = 0;
                switch (wk.ToString())
                {
                    case "Monday":
                        test = 1;
                        break;
                    case "Tuesday":
                        test = 2;
                        break;
                    case "Wednesday":
                        test = 3;
                        break;
                    case "Thursday":
                        test = 4;
                        break;
                    case "Friday":
                        test = 5;
                        break;
                    case "Saturday":
                        test = 6;
                        break;
                    default:
                        test = 0;
                        break;
                }

                var labels = newtablelayoutPanel.Controls.Find("Week_" + test, true);
                if (labels.Length > 0)
                {
                    var label = (Label)labels[0];
                    label.BackColor = System.Drawing.Color.FromName("Red");

                }
                panelFill();
            }
            private void panelFill()
            {
                Person[] peopleArray = new Person[9]
              {
               //Test case  Input Format => class name, auditory, prof. name, day week1, start time, end time, day week2, start time, end time, cell num from color list
                  new Person("Client research", "405", "Prof. Casimir","Monday",2,2,"Wednesday",1,1,1),
                  new Person("Music and the Brain Symposium","404", "Prof. Boleslav", "Monday", 3,3, "Wednesday", 4,4,2),
                  new Person("Aesthetics & the Philosophy of Art","567","Prof. Vedette","Tuesday",4,4," ", 0, 0, 0),
                  new Person("Italian Renaissance","203", "Prof. Miloslava","Tuesday", 5, 5,"Thursday", 6,6,3),
                  new Person("Roman Architecture","906", "Prof. Marcus","Wednesday", 7,8,"Friday",5,5,4),
                  new Person("Public Speaking II", "126", "Prof. Ruggero","Friday",0,2," ",0,0,8),
                  new Person("Digital Design","101B", "Prof. Bronislaw","Monday",6,6," ",0,0,5),
                  new Person("Economics of Money and Banking","402", "Teresa","Tuesday",0,2," ",0,0,6),
                  new Person("Financial Markets","108B","Prof. Zephyrus","Saturday",5,6," ",0,0,7),
              };
                People peopleList = new People(peopleArray);

                foreach (Person p in peopleList)
                {
                    if (p.LectureName == " ")
                        continue;
                    else if (p.Day2 == " " && p.Day2_strt_time == 0 && p.Day2_end_time == 0)
                    {
                        switch (p.Day1)
                        {
                            case "Monday":
                                scheduleBuider(1, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;

                            case "Tuesday":
                                scheduleBuider(2, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;

                            case "Wednesday":
                                scheduleBuider(3, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;

                            case "Thursday":
                                scheduleBuider(4, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;

                            case "Friday":
                                scheduleBuider(5, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;

                            case "Saturday":
                                scheduleBuider(6, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                                break;
                            default:
                                MessageBox.Show("Error!");
                                break;
                        }

                        continue;
                    }
                    switch (p.Day1)
                    {
                        case "Monday":
                            scheduleBuider(1, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Tuesday":
                            scheduleBuider(2, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Wednesday":
                            scheduleBuider(3, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Thursday":
                            scheduleBuider(4, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Friday":
                            scheduleBuider(5, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Saturday":
                            scheduleBuider(6, p.Day1_strt_time, p.Day1_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;
                        default:
                            MessageBox.Show("Error!");
                            break;

                    }
                    switch (p.Day2)
                    {
                        case "Monday":
                            scheduleBuider(1, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Tuesday":
                            scheduleBuider(2, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Wednesday":
                            scheduleBuider(3, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Thursday":
                            scheduleBuider(4, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Friday":
                            scheduleBuider(5, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;

                        case "Saturday":
                            scheduleBuider(6, p.Day2_strt_time, p.Day2_end_time, p.LectureName, p.clrN, p.place, p.prof);
                            break;
                        default:
                            MessageBox.Show("Error!");
                            break;
                    }
                }
            }

            private void scheduleBuider(int Colum, int rowStart, int rowEnd, string lecName, int colorNum, string place, string prof)
            {
                string BackColor;

                List<string> colors = new List<string>();
                colors.Add("Silver");
                colors.Add("LavenderBlush");
                colors.Add("DarkSeaGreen");
                colors.Add("Brown");
                colors.Add("Teal");

                colors.Add("Lavender");
                colors.Add("LemonChiffon");
                colors.Add("Coral");
                colors.Add("ForestGreen");
                colors.Add("Gold");

                colors.Add("Lavender");
                colors.Add("OliveDrab");
                colors.Add("Moccasin");
                colors.Add("MediumVioletRed");
                colors.Add("RosyBrown");


                colors.Add("YellowGreen");
                colors.Add("Goldenrod");
                colors.Add("DarkSalmon");
                colors.Add("CornflowerBlue");
                colors.Add("PeachPuff");

                colors.Add("Crimson");
                colors.Add("Tomato");
                colors.Add("MediumSlateBlue");
                colors.Add("DarkCyan");
                colors.Add("DarkSlateGray");

                colors.Add("DarkRed");
                colors.Add("Indigo");
                colors.Add("Orchid");
                colors.Add("LightSkyBlue");
                colors.Add("Gainsboro");


                BackColor = colors[colorNum];
                int count = 0;
                for (int i = rowStart; i <= rowEnd; i++)
                {
                    Label lbl = new Label();
                    Label one = new Label();

                    if ((rowEnd - rowStart) != 0)
                    {
                        count++;

                        if (count == 3)
                        {
                            one.Text = lecName + " (" + place + "/" + prof + ")";
                            one.Dock = DockStyle.Fill;
                            newtablelayoutPanel.Controls.Add(one, Colum, rowStart + 1);
                            one.BackColor = System.Drawing.Color.FromName(BackColor);
                            newtablelayoutPanel.SetRowSpan(one, 3);
                            one.Click += lbl_click;
                        }

                        else if (count == 2 && (rowEnd - rowStart) == 1)
                        {
                            one.Text = lecName + " (" + place + "/" + prof + ")";
                            one.Dock = DockStyle.Fill;
                            newtablelayoutPanel.Controls.Add(one, Colum, rowStart + 1);
                            one.BackColor = System.Drawing.Color.FromName(BackColor);
                            newtablelayoutPanel.SetRowSpan(one, 2);
                            one.Click += lbl_click;
                        }
                    }

                    else if ((rowEnd - rowStart) == 0)
                    {
                        lbl.Text = lecName + " (" + place + "/" + prof + ")";
                        lbl.Dock = DockStyle.Fill;
                        newtablelayoutPanel.Controls.Add(lbl, (Colum), (i+1));
                        lbl.BackColor = System.Drawing.Color.FromName(BackColor);
                        lbl.Click += lbl_click;
                    }
                }

                this.Controls.Add(newtablelayoutPanel);
            }

            // Event to each label
            protected void lbl_click(object sender, EventArgs e)
            {
                Label lbl = (Label)sender;
                MessageBox.Show(lbl.Text);

            }

        }

        public class Person
        {
            public Person(string LectureName, string place, string prof, string Day1, int Day1_strt_time, int Day1_end_time, string Day2, int Day2_strt_time, int Day2_end_time, int clrNum)
            {
                this.LectureName = LectureName;
                this.place = place;
                this.prof = prof;
                this.Day1 = Day1;
                this.Day1_strt_time = Day1_strt_time;
                this.Day1_end_time = Day1_end_time;
                this.Day2 = Day2;
                this.Day2_strt_time = Day2_strt_time;
                this.Day2_end_time = Day2_end_time;
                this.clrN = clrNum;

            }
            public string LectureName;
            public string Day1;
            public string Day2;
            public int Day1_strt_time;
            public int Day1_end_time;
            public int Day2_strt_time;
            public int Day2_end_time;
            public int clrN;
            public string prof;
            public string place;
        }

        public class People : IEnumerable
        {
            private Person[] _people;
            public People(Person[] pArray)
            {
                _people = new Person[pArray.Length];

                for (int i = 0; i < pArray.Length; i++)
                {
                    _people[i] = pArray[i];
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public PeopleEnum GetEnumerator()
            {
                return new PeopleEnum(_people);
            }
        }
        public class PeopleEnum : IEnumerator
        {
            public Person[] _people;
            int position = -1;

            public PeopleEnum(Person[] list)
            {
                _people = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _people.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public Person Current
            {
                get
                {
                    try
                    {
                        return _people[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
}













/*
 * 
 * 
 * newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            newtablelayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
 * 
 *   newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            newtablelayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
 * 
            newtablelayoutPanel.Controls.Add(new Label() { Text = abbr.ToString(), Name = "s", ForeColor = Color.Red }, 1, 0);
         newtablelayoutPanel.Controls.Add(new Label() { Text = "Tue" }, 2, 0);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "Wed" }, 3, 0);
         newtablelayoutPanel.Controls.Add(new Label() { Text = "Thur" }, 4, 0);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "Fri" }, 5, 0);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "Sat" }, 6, 0);

           newtablelayoutPanel.Controls.Add(new Label() { Text = "0" }, 0, 1);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "1" }, 0, 2);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "2" }, 0, 3);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "3" }, 0, 4);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "4" }, 0, 5);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "5" }, 0, 6);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "6" }, 0, 7);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "7" }, 0, 8);
          newtablelayoutPanel.Controls.Add(new Label() { Text = "8" }, 0, 9);
          */








