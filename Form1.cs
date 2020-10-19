using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.Common;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Formula1
{
    public partial class Form1 : Form
    {
        private int rowsCount = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> cars = Program.Select("SELECT * FROM cars");

            for(int i = 0; i < cars.Count; i += 7)
            {
                Label label = new Label { Text = cars[i], Location = new Point(i / 7 % rowsCount * 300, ((i / 7 - (i / 7 % rowsCount))) * 120), Tag = i };
                PictureBox pictureBox = new PictureBox { Image = Image.FromFile("Pictures/" + cars[i + 1]), Location = new Point(i / 7 % rowsCount * 300 + 50, (i / 7 - (i / 7 % rowsCount)) * 120 + 20), Size = new Size(300, 200), SizeMode = PictureBoxSizeMode.StretchImage, Tag = i };

                Controls.AddRange(new Control[] { label, pictureBox });
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (((Form)sender).Size.Width < 300)
                return;

            if (rowsCount != (int)Math.Floor((double)(((Form)sender).Size.Width / 300)))
            {
                rowsCount = (int)Math.Floor((double)(((Form)sender).Size.Width / 300));

                foreach(Control control in Controls)
                {
                    int i = int.Parse(control.Tag.ToString());

                    if (control is PictureBox)
                        control.Location = new Point(i / 7 % rowsCount * 300 + 50, ((i / 7 - (i / 7 % rowsCount))) * 120 + 20);

                    else if (control is Label)
                        control.Location = new Point(i / 7 % rowsCount * 300, ((i / 7 - (i / 7 % rowsCount))) * 120);
                }
            }
        }
    }
}
