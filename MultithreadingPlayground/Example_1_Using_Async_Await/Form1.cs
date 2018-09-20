using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example_1_Using_Async_Await
{
    public partial class Form1 : Form
    {
        public Task<int> CalculateValue()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return 123;
            });
        }
        public Task<int> CalculateValueTpl()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return 123;
            });
        }
        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var task = CalculateValueTpl();
            task.ContinueWith(t =>
            {
                lblResult.Text = t.Result.ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void btnCalculateAsync_Click(object sender, EventArgs e)
        {
            var result = await CalculateValueAsync();
            lblResult.Text = result.ToString();
        }

        private void btnCalculateUiThread_Click(object sender, EventArgs e)
        {
            var result = CalculateValue().Result;
            lblResult.Text = result.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Result";
        }
    }
}
