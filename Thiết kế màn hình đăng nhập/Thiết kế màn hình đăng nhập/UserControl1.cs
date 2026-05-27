using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thiết_kế_màn_hình_đăng_nhập
{
    public partial class UserControl1 : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            List<SinhVien> dslh = db.SinhViens.ToList();
            dataGridView1.DataSource = dslh;
            LoadDSLH();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SinhVien sinhvien = new SinhVien();
            sinhvien.ma_sv = textBox1.Text;
            sinhvien.ho_ten = textBox2.Text;
            sinhvien.gioi_tinh = comboBox1.Text;
            sinhvien.ngay_sinh = DateTime.Parse(dateTimePicker1.Text);
            sinhvien.ma_lop = comboBox2.SelectedValue.ToString();
            try
            {
                db.SinhViens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Them moi sinh vien thanh cong.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData()
        {
            List<SinhVien> dssv = db.SinhViens.ToList();
            dataGridView1.DataSource = dssv;
        }
        public void LoadDSLH()
        {
            List<LopHoc> dslh = db.LopHocs.ToList();
            comboBox2.DataSource = dslh;
            comboBox2.DisplayMember = "ten_lop";
            comboBox2.ValueMember = "ma_lop";
        }
    }
}
