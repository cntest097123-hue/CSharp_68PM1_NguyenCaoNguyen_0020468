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
    public partial class UserControl2 : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public UserControl2()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            List<LopHoc> dssv = db.LopHocs.ToList();
            dataGridView1.DataSource = dssv;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
