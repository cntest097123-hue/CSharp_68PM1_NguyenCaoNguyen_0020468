using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1 .Rows[e.RowIndex];

                textBox1.Text = row.Cells["ma_sv"].Value.ToString();
                textBox2.Text = row.Cells["ho_ten"].Value.ToString();
                comboBox1.Text = row.Cells["gioi_tinh"].Value.ToString();

                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ngay_sinh"].Value);

                comboBox2.SelectedValue = row.Cells["ma_lop"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String ma_sv = textBox1.Text;

                SinhVien sv = db.SinhViens.FirstOrDefault(x => x.ma_sv == ma_sv);

                if (sv != null)
                {
                    sv.ho_ten = textBox2.Text;
                    sv.gioi_tinh =  comboBox1.Text;
                    sv.ngay_sinh = dateTimePicker1.Value;
                    sv.ma_lop = comboBox2.SelectedValue.ToString();
                    db.SubmitChanges();
                    MessageBox.Show("Cap nhat thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Khong tim thay sinh vien!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
