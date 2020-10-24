using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        private int initMoney = 0;
        private int incrMoney = 0;
        private int len = 0;

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        public class GridControlClass {
            public int year { get; set; }
            public int money { get; set; }
            public string p10 { get; set; }
            public string p15 { get; set; }
            public string p20 { get; set; }
            public string p25 { get; set; }
            public string p30 { get; set; }
        }

        private void button1_Click(object sender, EventArgs e) {
            initMoney = string.IsNullOrEmpty(tbInitMoney.Text) ? 0 : Convert.ToInt32(tbInitMoney.Text);
            incrMoney = string.IsNullOrEmpty(tbIncrMoney.Text) ? 0 : Convert.ToInt32(tbIncrMoney.Text);
            len = string.IsNullOrEmpty(tbTotalYear.Text) ? 0 : Convert.ToInt32(tbTotalYear.Text) + 1;

            gcUploadData.DataSource = new List<GridControlClass>();
            var lstData = new List<GridControlClass>();

            string lastP10 = string.Empty;
            string lastP15 = string.Empty;
            string lastP20 = string.Empty;
            string lastP25 = string.Empty;
            string lastP30 = string.Empty;
            for (int i = 0; i < len; i++) {
                var item = new GridControlClass {
                    year = i,
                    money = initMoney + (incrMoney * i)
                };

                if (i == 0) {
                    lastP10 = item.p10 = item.money.ToString();
                    lastP15 = item.p15 = item.money.ToString();
                    lastP20 = item.p20 = item.money.ToString();
                    lastP25 = item.p25 = item.money.ToString();
                    lastP30 = item.p30 = item.money.ToString();
                } else {
                    lastP10 = (Convert.ToDecimal(lastP10) * Convert.ToDecimal((1 + 0.1)) + incrMoney).ToString();
                    item.p10 = Convert.ToDecimal(lastP10).ToString("F2");

                    lastP15 = (Convert.ToDecimal(lastP15) * Convert.ToDecimal((1 + 0.15)) + incrMoney).ToString();
                    item.p15 = Convert.ToDecimal(lastP15).ToString("F2");

                    lastP20 = (Convert.ToDecimal(lastP20) * Convert.ToDecimal((1 + 0.20)) + incrMoney).ToString();
                    item.p20 = Convert.ToDecimal(lastP20).ToString("F2");

                    lastP25 = (Convert.ToDecimal(lastP25) * Convert.ToDecimal((1 + 0.25)) + incrMoney).ToString();
                    item.p25 = Convert.ToDecimal(lastP25).ToString("F2");

                    lastP30 = (Convert.ToDecimal(lastP30) * Convert.ToDecimal((1 + 0.30)) + incrMoney).ToString();
                    item.p30 = Convert.ToDecimal(lastP30).ToString("F2");
                }

                lstData.Add(item);
            }



            Monitor.Enter(gcUploadData.DataSource);
            var lstLoading = gcUploadData.DataSource as List<GridControlClass>;
            Monitor.Exit(gcUploadData.DataSource);

            foreach (var item in lstData) {
                lstLoading.Add(item);

            }

            gcUploadData.Invoke(new Action(() => {
                gcUploadData.RefreshDataSource();
            }));
        }
    }
}
