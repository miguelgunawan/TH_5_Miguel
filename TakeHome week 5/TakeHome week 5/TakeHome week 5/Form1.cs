using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeHome_week_5
{
    public partial class Welcome : Form
    {
        List<Product> products = new List<Product>();
        List<Catagory>listcatagory=new List<Catagory>();
        List<string> catagoryname = new List<string>();
        List<int> catagoryid = new List<int>();
        DataTable dtProdukSimmpan = new DataTable();
        DataTable dtcatagory = new DataTable();
        DataTable dtProdukTampil = new DataTable();
        int count = 6;
        int counter = 0;
        string catagory = "";
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "C1","Jas"
            },
            {
                "C2","T-Shirt"
            },
            {
                "C3","Rok"
            },
            {
                "C4","Celana"
            },
            {
                "C5","Cawat"
            },
        };
        public Welcome()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Detail tidak lengkap", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                addproduct();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show(" anam catagory tidak ada", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dic.Add("C" + count, textBox4.Text);
                catagorylist(textBox4.Text);
                textBox4.Text = "";
            }

        }
        public void catagorylist(string input)
        {
            bool ada = false;
            dtcatagory.Rows.Clear();
            foreach(Catagory sakarepmu in listcatagory)
            {
                if (sakarepmu.katagoryname.Contains(input))
                {
                    ada = true;
                    break;
                }
            }
            if(ada==false)
            {
                listcatagory.Add(new Catagory { katagoryid = "C" + count, katagoryname = input });
                count++;
            }
            //if (!sakarepmu.Catagoryname.Contains(input))
            //{
            //    catagoryname.Add(input);
            //    count++;
            //    catagoryid.Add(count);
            //}
            else
            {
                MessageBox.Show("Catagory existed, try another one", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            comboBox1.Items.Clear();
            foreach(Catagory katagori in listcatagory) 
            {
                dtcatagory.Rows.Add(katagori.katagoryid,katagori.katagoryname);
                comboBox1.Items.Add(katagori.katagoryname);
            }
        }
        private void addproduct()
        {
            Product newproduct = new Product(textBox1.Text,textBox2.Text,textBox3.Text,catagory);
            products.Add(newproduct);
            for (int k = 65; k <= 90; k++)
            {
                foreach (Product product in products)
                {
                    if (product.namaproduct[0] == Convert.ToChar(k))
                    {
                        counter++;
                        product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
                    }
                }
                counter = 0;
            }
            dtProdukSimmpan.Clear();
            foreach (Product produk in products)
            {
                dtProdukSimmpan.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.namacatagory);
            }
            //for (int k = 65; k <= 90; k++)
            //{
            //    if (product.namaproduct[0] == Convert.ToChar(k))
            //    {
            //        foreach (Product produk in products)
            //        {
            //            if (produk.namaproduct[0] == product.namaproduct[0])
            //            {
            //                counter++;
            //                product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
            //            }
            //        }
            //    }
            //    counter = 0;
            //}
            //foreach (Product produk in products)
            //{
            //    for (int i = 0; i <count; i++)
            //    {
            //        if (produk.namacatagory == dtcatagory.Rows[i][1].ToString())
            //        {
            //            product.idcatagory = dtcatagory.Rows[i][0].ToString();
            //        }
            //    }
            //}
            //dtProdukSimmpan.Rows.Add(product.Idproduct, product.namaproduct, product.hargaproduct, product.stockproduct, product.idcatagory);
        }
        private void removeproduct()
        {
            DataGridViewRow datagridview = dataproduct1.Rows[dataproduct1.CurrentCell.RowIndex];
            string store = datagridview.Cells["Nama Product"].Value.ToString();
            for(int i = 0; i <products.Count; i++)
            {
                if (products[i].namaproduct==store)
                {
                    products.RemoveAt(i);
                    break;
                }
            }
            dtProdukSimmpan.Clear();
            foreach(Product product in products)
            {
                dtProdukSimmpan.Rows.Add(product.Idproduct, product.namaproduct, product.hargaproduct, product.stockproduct, product.namacatagory);
            }


        }

      

        private void Welcome_Load(object sender, EventArgs e)
        {
            dtProdukSimmpan.Columns.Add("ID Product");
            dtProdukSimmpan.Columns.Add("Nama Product");
            dtProdukSimmpan.Columns.Add("Harga");
            dtProdukSimmpan.Columns.Add("Stock");
            dtProdukSimmpan.Columns.Add("ID Catagory");
            dataproduct1.DataSource = dtProdukSimmpan;
            dtProdukTampil.Columns.Add("ID Product");
            dtProdukTampil.Columns.Add("Nama Product");
            dtProdukTampil.Columns.Add("Harga");
            dtProdukTampil.Columns.Add("Stock");
            dtProdukTampil.Columns.Add("ID Catagory");

            dtcatagory.Columns.Add("ID Catagory");
            dtcatagory.Columns.Add("Nama Catagory");
            dataGridView2.DataSource = dtcatagory;

            products.Add(new Product("Jas Hitam", "100000", "10", "C1"));
            products.Add(new Product("T-Shirt Black Pink", "70000", "20", "C2"));
            products.Add(new Product("T-Shirt Obsessive", "75000", "16", "C2"));
            products.Add(new Product("Rok Mini", "82000", "26", "C3"));
            products.Add(new Product("Jeans Biru", "90000", "5", "C4"));
            products.Add(new Product("Celana Pendek Coklat", "60000", "14", "C4"));
            products.Add(new Product("Cawat Blink-Blink", "100000", "1", "C5"));
            products.Add(new Product("Rocca Shirt", "50000", "8", "C2"));

            listcatagory.Add(new Catagory { katagoryid = "C1", katagoryname = "Jas" });
            listcatagory.Add(new Catagory { katagoryid = "C2", katagoryname = "T-Shirt" });
            listcatagory.Add(new Catagory { katagoryid = "C3", katagoryname = "Rok" });
            listcatagory.Add(new Catagory { katagoryid = "C4", katagoryname = "celana" });
            listcatagory.Add(new Catagory { katagoryid = "C5", katagoryname = "cawat" });

            //foreach (Product product in products)
            //{
            //    if (!catagoryname.Contains(product.namacatagory)){
            //        catagoryname.Add(product.namacatagory);
            //        count++;
            //        catagoryid.Add(count);
            //    }
            //}
            //for (int i = 0; i < count; i++)
            //{
            //    dtcatagory.Rows.Add("C" + catagoryid[i], catagoryname[i]);
            //}
            foreach(Catagory katagori in listcatagory)
            {
                dtcatagory.Rows.Add(katagori.katagoryid, katagori.katagoryname);
                comboBox1.Items.Add(katagori.katagoryname);
                comboBox2.Items.Add(katagori.katagoryname);
            }
            for (int k = 65; k <= 90; k++)
            {
                foreach(Product product in products)
                {
                    if (product.namaproduct[0]==Convert.ToChar(k))
                    {
                        counter++;
                        product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
                    }
                }
                counter = 0;
            }
            foreach (Product produk in products)
            {
                dtProdukSimmpan.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.namacatagory);
            }
            comboBox2.DataSource = dtcatagory;
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (produk.namacatagory == dtcatagory.Rows[i][1].ToString())
            //        {
            //            produk.idcatagory = dtcatagory.Rows[i][0].ToString();
            //        }
            //    }
            //dtProdukSimmpan.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.idcatagory);
            //}
            //for (int l = 0; l < count; l++)
            //{
            //    comboBox1.Items.Add(dtcatagory.Rows[l][1].ToString());
            //}
            //for(int j=0;j<count; j++)
            //{
            //    comboBox2.Items.Add(dtcatagory.Rows[j][1].ToString());
            //}
            //foreach (Product produk in products)
            //{
            //    for (int p = 0; p < count; p++)
            //    {
            //        if (comboBox2.SelectedItem.ToString() == dtcatagory.Rows[p][1].ToString())
            //        {
            //            if(produk.namacatagory==dtcatagory.Rows[p][1].ToString())
            //            {

            //            }
            //        }
            //    }
            //}
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }



        private void button5_Click(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            //comboBox2.DataSource = dtcatagory;
            comboBox2.DisplayMember = "Nama Catagory";
            comboBox2.ValueMember = "ID Catagory";
            comboBox2.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            removeproduct();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataGridViewRow edit = dataproduct1.Rows[dataproduct1.CurrentCell.RowIndex];
            string penampung = edit.Cells["Nama Product"].Value.ToString();
            string store = "";
            for(int i = 0; i < products.Count; i++)
            {
                if (products[i].namaproduct==penampung)
                {
                    foreach(Catagory setnama in listcatagory)
                    {
                        if(setnama.katagoryname==comboBox1.Text)
                        {
                            store = setnama.katagoryid;
                        }
                    }
                    if(Convert.ToInt32(textBox3.Text)<=0)
                    {
                        products.RemoveAt(i);
                    }
                    else
                    {
                        products[i].namaproduct=textBox1.Text;
                        products[i].hargaproduct = textBox2.Text;
                        products[i].stockproduct = textBox3.Text;
                        products[i].namacatagory = store;
                    }
                }
            }
            for (int k = 65; k <= 90; k++)
            {
                foreach (Product product in products)
                {
                    if (product.namaproduct[0] == Convert.ToChar(k))
                    {
                        counter++;
                        product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
                    }
                }
                counter = 0;
            }
            dtProdukSimmpan.Clear();
            foreach (Product produk in products)
            {
                dtProdukSimmpan.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.namacatagory);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<Product> produks = new List<Product>();
            DataGridViewRow hello = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex];
            string penampung = hello.Cells["ID Catagory"].Value.ToString();
            foreach(Product produk in products)
            {
                if(produk.namacatagory.Contains(penampung))
                {
                    produks.Add(produk);
                }
            }
            foreach(Product produk in produks)
            {
                products.Remove(produk);
            }
            listcatagory.RemoveAt(dataGridView2.CurrentCell.RowIndex);
            for (int k = 65; k <= 90; k++)
            {
                foreach (Product product in products)
                {
                    if (product.namaproduct[0] == Convert.ToChar(k))
                    {
                        counter++;
                        product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
                    }
                }
                counter = 0;
            }
            dtProdukSimmpan.Clear();
            foreach (Product produk in products)
            {
                dtProdukSimmpan.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.namacatagory);
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            dtcatagory.Clear();
            foreach (Catagory katagori in listcatagory)
            {
                dtcatagory.Rows.Add(katagori.katagoryid, katagori.katagoryname);
                comboBox1.Items.Add(katagori.katagoryname);
                comboBox2.Items.Add(katagori.katagoryname);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            catagory="C"+(comboBox1.SelectedIndex+1);
        }

        private void dataproduct1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                DataGridViewRow edit = dataproduct1.Rows[dataproduct1.CurrentCell.RowIndex];
                textBox1.Text = edit.Cells["Nama Product"].Value.ToString();
                textBox2.Text = edit.Cells["Harga"].Value.ToString();
                textBox3.Text = edit.Cells["Stock"].Value.ToString();
                string penampung= edit.Cells["ID Catagory"].Value.ToString();
                string nama = dic[penampung];
                comboBox1.Text = nama;
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtProdukTampil.Clear();
            foreach (Product produks in products)
            {
                if(comboBox2.SelectedValue.ToString()==produks.namacatagory)
                {
                    
                    for (int k = 65; k <= 90; k++)
                    {
                        foreach (Product product in products)
                        {
                            if (product.namaproduct[0] == Convert.ToChar(k))
                            {
                                counter++;
                                product.Idproduct = Convert.ToChar(k) + counter.ToString("000");
                            }
                        }
                        counter = 0;
                    }
                    dtProdukTampil.Clear();
                    foreach (Product produk in products)
                    {
                        if(produk.namacatagory == comboBox2.SelectedValue.ToString())
                        {
                            dtProdukTampil.Rows.Add(produk.Idproduct, produk.namaproduct, produk.hargaproduct, produk.stockproduct, produk.namacatagory);
                        }
                        
                    }
                }
            }
            dataproduct1.DataSource = dtProdukTampil;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            comboBox2.Text = "";
            dataproduct1.DataSource = dtProdukSimmpan;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }
    }
}
