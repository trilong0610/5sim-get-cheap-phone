using GetPhoneCode._5sim;
using GetPhoneCode.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetPhoneCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // load env local
            txt_api_key.Text = Properties.Settings.Default.api;
            switch_update_history.Checked = Properties.Settings.Default.autoHistory;
            txt_update_time.Text = Properties.Settings.Default.autoHistoryTime.ToString();

            cb_product.ValueMember = "name";
            cb_product.DisplayMember = "name";
            cb_product.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_product.AutoCompleteSource = AutoCompleteSource.ListItems;
            grid_phone.Columns[1].Visible = false;
            // Lấy danh sách product từ file xml
            Task.Run(() =>
            {
                var data = ProductPriceCommon.loadProduct();
                cb_product.Invoke((Action)(() => cb_product.DataSource = data
                ));
                
                
            });

            // Lấy thong tin user
            Task.Run(() =>
            {
                if (txt_api_key.Text != null)
                {
                    var user = UserCommon.loadUserBalance(txt_api_key.Text);
                    lb_email.Invoke((Action)(() => lb_email.Text = user.email));
                    lb_balance.Invoke((Action)(() => lb_balance.Text = user.balance.ToString()));
                }
                

            });
        }

        private void btn_get_phone_Click(object sender, EventArgs e)
        {
            var product = cb_product.SelectedValue.ToString();
            grid_phone.Refresh();
            Task.Run(() =>
            {
                // Lay danh sach so dien thoai
                var prices = ProductPriceCommon.loadProductPrices(product, Int32.Parse(txt_count_count.Text));
                int count = 0;
                var pricesSort = prices.OrderBy(o => o.Cost).ToList();
                if (Int32.Parse(txt_count_operators.Text) > 0)
                {
                    pricesSort = pricesSort.GetRange(0, Int32.Parse(txt_count_operators.Text));
                }
                
                grid_phone.Invoke((Action)(() =>
                    grid_phone.DataSource = pricesSort
                ));
                // An process bar
            });
            
            
        }

        private void txt_api_key_TextChange(object sender, EventArgs e)
        {
            if (txt_api_key.Text != null)
            {
                Properties.Settings.Default.api = txt_api_key.Text;
                Properties.Settings.Default.Save();
                var user = UserCommon.loadUserBalance(txt_api_key.Text);
                lb_email.Invoke((Action)(() => lb_email.Text = user.email));
                lb_balance.Invoke((Action)(() => lb_balance.Text = user.balance.ToString()));
            }
        }

        private void btn_get_code_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                
                // Lay danh sach may chu can thue
                foreach (DataGridViewRow item in grid_phone.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value))
                    {
                        string product = item.Cells[1].Value.ToString();
                        string country = item.Cells[2].Value.ToString();
                        string _operator = item.Cells[3].Value.ToString();
                        PurchaseCommon.buyNumber(txt_api_key.Text, country, _operator, product);
                    }
                }

            });
        }

        private void bunifuToggleSwitch1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            // Neu switch dang true thi khong cho chinh time
            if (e.Checked)
            {
                txt_update_time.Enabled = false;
            }
            else
            {
                txt_update_time.Enabled = true;
            }

            if (e.Checked)
            {
                autoGetHistory(Convert.ToInt32(txt_update_time.Text));
            }
            Properties.Settings.Default.autoHistoryTime = Convert.ToInt32(txt_update_time.Text);
            Properties.Settings.Default.Save();
        }

        private void autoGetHistory(int second) {
            
            Task.Run(() =>
            {
                while (switch_update_history.Checked && Convert.ToInt32(txt_update_time.Text) > 0)
                {

                    //OrderCommon.checkOrder(txt_api_key.Text, "335186974");
                    var history = UserCommon.loadOrderHistory(txt_api_key.Text, getBlackListStatus(), txt_history_limit.Text);

                    grid_code.Invoke((Action)(() => { 
                        grid_code.DataSource = history;
                        foreach (DataGridViewColumn item in grid_code.Columns)
                        {
                            if (item.HeaderText == "Id" || item.HeaderText == "Expires")
                            {
                                item.Visible = false;
                            }
                        }
                    }));
                    

                    Task.Delay(second * 1000).Wait();
                }
            });
            
            
            
        }

        private void txt_update_time_TextChanged(object sender, EventArgs e)
        {
            if (txt_update_time.Text != String.Empty)
            {
                Properties.Settings.Default.autoHistoryTime = Convert.ToInt32(txt_update_time.Text);
                Properties.Settings.Default.Save();
            }
            
        }

        private List<string> getBlackListStatus() {
            List<string> listStatus = new List<string>();
            if (!switch_status_banned.Checked)
                listStatus.Add("BANNED");
            if (!switch_status_canceled.Checked)
                listStatus.Add("CANCELED");
            if (!switch_status_finished.Checked)
                listStatus.Add("FINISHED");
            if (!switch_status_pending.Checked)
                listStatus.Add("PENDING");
            if (!switch_status_received.Checked)
                listStatus.Add("RECEIVED");
            if (!switch_status_timeout.Checked)
                listStatus.Add("TIMEOUT");

            return listStatus;
        }
    }
}
