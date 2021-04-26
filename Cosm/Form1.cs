using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cosm
{
    public partial class Form1 : Form
    {
        string folder;

        Model11 db = new Model11();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "выбери фото товай";
            ofd.InitialDirectory = @"..\..\Товары салона красоты";
            ofd.Filter = "Файлы JPG, GIF, PNG|*.jpg;*.gif;*.png|Все файлы (*.*)|*.*";
            
            DialogResult rc = ofd.ShowDialog();
            if (rc == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                folder = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " " || textBox2.Text == " ")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            Product prd = new Product();
            int id = int.Parse(textBox1.Text);
            prd.ID = id;
            prd.Title = textBox2.Text;
            prd.Cost = Convert.ToDecimal(textBox3.Text);
            prd.Description = textBox4.Text;
            prd.IsActive =checkBox1.Checked;
            prd.ManufacturerID = Convert.ToInt32(textBox6.Text);

            ImageConverter conv = new ImageConverter();
            byte[] bImg = (byte[])conv.ConvertTo(pictureBox1.Image, typeof(byte[]));
            prd.MainImagePath = folder;
            db.Product.Add(prd);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ID не задан!");
                return;
            }
            int id = int.Parse(textBox1.Text);
            Product prd = db.Product.Find(id);
            if (prd == null)
            {
                MessageBox.Show("Товара с таким ID не существует!");
                return;
            }
            if (prd.MainImagePath == "" || prd.MainImagePath == null)
            {
                MessageBox.Show("У данного товара отсутствует изображение!");
                pictureBox1.Image = null;
            }
            else
            {
                //pictureBox1.Image = Image.FromFile($@"..\..\{prd.MainImagePath}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
    


