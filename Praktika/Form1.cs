using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.IO;

namespace Praktika
{
    public partial class Form1 : Form
    {
        public VkApi api = new VkApi();
        public string token;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            token = textBox1.Text;

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = token
                }) ;
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message);
            }

            Form3 f3 = new Form3(api);
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            token = textBox1.Text;

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = token
                });
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);
            }

            AddTask T = new AddTask(api);
            T.ShowDialog();
        }

        //static void Auth(string token)
        //{
        //    var api = new VkApi();

        //    api.Authorize(new ApiAuthParams
        //    {
        //        AccessToken = token
        //    }) ;
        //}
    }
}
