using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QuanLyKhachSan
{
    public partial class frmQuanLyKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public frmQuanLyKhachSan()
        {
            InitializeComponent();
        }

      

        private void frmQuanLyKhachSan_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();
            loatDaTaGridview();

        }
        private void loatDaTaGridview()
        {
            string sql = "select *from tblPhong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tabletblPhong = new DataTable();
            
            adp.Fill(tabletblPhong);
            dataGridView_tblPhong.DataSource = tabletblPhong;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Enabled = true;
            loatDaTaGridview();
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("bạn cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("bạn cần nhập tên phòng");
                txtTenPhong.Focus();
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("bạn cân phải nhập đơn giá");
                txtTenPhong.Focus();
            }

            //string sql = "select MaPhong From tblPhong Where MaPhong='" + txtMaPhong.Text + "'";
            

                string sql = "insert into tblPhong values ('" + txtMaPhong.Text + "','" + txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                    sql = sql + "," + txtDonGia.Text.Trim();

                sql = sql + ")";
            
                MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                loatDaTaGridview();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete From tblPhong Where MaPhong= '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loatDaTaGridview();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bạn có chắc chắn muốn thoát chương trình không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.Exit();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text= "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            btnHuy.Enabled = false;
            //btnSua.Enabled = true;
            //btnThem.Enabled = true;
            //btnXoa.Enabled = true;
            txtMaPhong.Enabled = false;
            loatDaTaGridview();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            
            
            if (txtMaPhong.Text == "") 
            {
                MessageBox.Show("Bạn nhập mã phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenPhong.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn chưa nhập tên phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblPhong SET TenPhong='" +
                txtTenPhong.Text.ToString() +"' , DonGia='"+txtDonGia.Text+
                "' WHERE MaPhong='" + txtMaPhong.Text + "'";
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            loatDaTaGridview();
            
        }

        private void dataGridView_tblPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView_tblPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView_tblPhong.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtDonGia.Text = dataGridView_tblPhong.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Enabled = false;
        }

        
    }
}
