using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKH
{
    public partial class Form1 : Form
    {
        List<KhachHang> danhSachKhachHang = new List<KhachHang>();

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormKhachHang_Load); 
        }
        private void SaveData()
        {
            using (FileStream fs = new FileStream("khachhang.dat", FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (var kh in danhSachKhachHang)
                {
                    writer.Write(kh.MaKH);
                    writer.Write(kh.HoTen);
                    writer.Write(kh.GioiTinh);
                    writer.Write(kh.DiaChi);
                    writer.Write(kh.DienThoai);
                }
            }
        }

        private void LoadData()
        {
            if (File.Exists("khachhang.dat"))
            {
                using (FileStream fs = new FileStream("khachhang.dat", FileMode.Open))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    danhSachKhachHang.Clear(); 

                    while (fs.Position < fs.Length)
                    {
                        KhachHang kh = new KhachHang
                        {
                            MaKH = reader.ReadString(),
                            HoTen = reader.ReadString(),
                            GioiTinh = reader.ReadString(),
                            DiaChi = reader.ReadString(),
                            DienThoai = reader.ReadString()
                        };
                        danhSachKhachHang.Add(kh); 
                    }
                }
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Mã KH", 100);
            listView1.Columns.Add("Họ tên", 150);
            listView1.Columns.Add("Giới tính", 100);
            listView1.Columns.Add("Địa chỉ", 200);
            listView1.Columns.Add("Điện thoại", 120);


            LoadData();
            HienThiListView();
        }


        private void HienThiListView()
        {
            listView1.Items.Clear(); 

            foreach (var kh in danhSachKhachHang)
            {
                ListViewItem item = new ListViewItem(kh.MaKH); 
                item.SubItems.Add(kh.HoTen);  
                item.SubItems.Add(kh.GioiTinh);  
                item.SubItems.Add(kh.DiaChi);  
                item.SubItems.Add(kh.DienThoai); 

                listView1.Items.Add(item); 
            }
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void them_Click(object sender, EventArgs e)
        {
            
            if (danhSachKhachHang.Any(khach => khach.MaKH == txtmkh.Text))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!");
                return;
            }

            
            KhachHang kh = new KhachHang
            {
                MaKH = txtmkh.Text,
                HoTen = txtht.Text,
                GioiTinh = comboBox1.Text,
                DiaChi = txtdc.Text,
                DienThoai = txtdt.Text
            };

            danhSachKhachHang.Add(kh);
            HienThiListView();  
        }


        private void xoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string maKH = listView1.SelectedItems[0].Text;
                var kh = danhSachKhachHang.FirstOrDefault(k => k.MaKH == maKH);

                if (kh != null)
                {
                    danhSachKhachHang.Remove(kh);
                    HienThiListView();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!");
            }
        }

        private void sua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string maKH = listView1.SelectedItems[0].Text;
                var kh = danhSachKhachHang.FirstOrDefault(k => k.MaKH == maKH);

                if (kh != null)
                {
                    kh.HoTen = txtht.Text;
                    kh.GioiTinh = comboBox1.Text;
                    kh.DiaChi = txtdc.Text;
                    kh.DienThoai = txtdt.Text;
                    HienThiListView();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!");
            }
        }

        private void timkiem_Click(object sender, EventArgs e)
        {
            string maKH = txtmkh.Text;
            var kh = danhSachKhachHang.FirstOrDefault(k => k.MaKH == maKH);

            if (kh != null)
            {
                MessageBox.Show($"Tìm thấy khách hàng: {kh.HoTen}, {kh.GioiTinh}, {kh.DiaChi}, {kh.DienThoai}");
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng!");
            }
        }

        private void thoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn lưu dữ liệu trước khi thoát?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SaveData();  
            }

            this.Close();
        }
       


    }
}
