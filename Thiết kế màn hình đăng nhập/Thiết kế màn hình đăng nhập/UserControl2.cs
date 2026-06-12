using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Thiết_kế_màn_hình_đăng_nhập
{
    public partial class UserControl2 : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        int currentPage = 1;
        int pageSize = 5;
        public UserControl2()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling(
            (double)db.LopHocs.Count() / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            List<LopHoc> dssv = db.LopHocs.ToList();
            dataGridView1.DataSource = dssv;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["id"].Value.ToString();
                textBox2.Text = row.Cells["ma_lop"].Value.ToString();
                textBox4.Text = row.Cells["ten_lop"].Value.ToString();
                textBox5.Text = row.Cells["ghi_chu"].Value.ToString(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LopHoc lopHoc = new LopHoc();
            lopHoc.id = textBox1.Text;
            lopHoc.ma_lop = textBox2.Text;
            lopHoc.ten_lop = textBox4.Text;
            lopHoc.ghi_chu = textBox5.Text;
            try
            {
                db.LopHocs.InsertOnSubmit(lopHoc);
                db.SubmitChanges();
                MessageBox.Show("Them moi lop hoc thanh cong.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData()
        {
            var dslh = db.LopHocs
            .OrderBy(x => x.id)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            dataGridView1.DataSource = dslh;

            int totalPages = (int)Math.Ceiling(
            (double)db.LopHocs.Count() / pageSize);

            label6.Text = $"Trang {currentPage}/{totalPages}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String id = textBox1.Text;

                LopHoc lh = db.LopHocs.FirstOrDefault(x => x.id == id);

                if (lh != null)
                {
                    lh.ma_lop = textBox2.Text;
                    lh.ten_lop = textBox4.Text;
                    lh.ghi_chu = textBox5.Text;
                    db.SubmitChanges();
                    MessageBox.Show("Cap nhat thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Khong tim thay lop hoc !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ban co chac chan muon xoa lop hoc nay?",
                "Xac nhan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                String id = (textBox1.Text);
                LopHoc lh = db.LopHocs
                                    .FirstOrDefault(x => x.id == id);

                if (lh != null)
                {
                    db.LopHocs.DeleteOnSubmit(lh);
                    db.SubmitChanges();
                    MessageBox.Show("Xoa thanh cong!");
                    LoadData();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string keyword = textBox3.Text.Trim();

            var ketQua = db.LopHocs
                           .Where(lh =>
                                lh.ma_lop.Contains(keyword) ||

                                lh.ten_lop.ToString().Contains(keyword) ||

                                lh.ghi_chu.Contains(keyword))
                           .ToList();

            dataGridView1.DataSource = ketQua;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentPage = TotalPages();
            LoadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }
        private int TotalPages()
        {
            return (int)Math.Ceiling(
                (double)db.LopHocs.Count() / pageSize);
        }
    }
}
