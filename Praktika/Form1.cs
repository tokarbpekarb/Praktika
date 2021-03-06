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
using VkNet.Model.Attachments;

namespace Praktika
{
  
    public partial class Form1 : Form
    {

        
        string today = DateTime.Today.Month+"."+ DateTime.Today.Day;
        
        public VkApi api = new VkApi();
        public string token;
        

        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(api);
            f3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTask T = new AddTask(api);
            T.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var getFollowers = api.Groups.GetMembers(new VkNet.Model.RequestParams.GroupsGetMembersParams
            {
                GroupId = "205575031",
                Fields = VkNet.Enums.Filters.UsersFields.BirthDate
                
            }) ;

            foreach (User user in getFollowers)
                {
                if (user.BirthDate != null)
                {
                    int dotNumber = user.BirthDate.Where(c => c == '.').Count();
                    if (dotNumber == 2)
                    {
                        int i = user.BirthDate.IndexOf('.');
                        i = user.BirthDate.IndexOf('.', i + 1);
                        string userData = user.BirthDate.Substring(0,i);
                        //button1.Text = userData;
                        if (today == userData)
                        {
                            string FL = Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName+" " + user.LastName)); 
                            
                            Post(api, FL);
                        }
                        
                    }
                }
               

            }

            
        }
        
       
        static void Post(VkApi api, string userName)
        {
            
            api.Wall.Post(new WallPostParams

            
            {
                Message = $"C днём рождения, {userName}! Это прекрасный день, который означает, что ты возможно становишься мудрее, сталкиваясь с теми или иными препятствиями, а также находишь новые пути решения тех или иных проблем. Желаем тебе удачи во всех твоих начинаниях и успехов в делах. ",
                OwnerId = -205575031
            });
        }
        
        static void Repost(VkApi api, string wall)
        {

            api.Wall.Repost("wall539310031_24", "Repost test with Id found.", 205575031, false);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(api);
            f4.Show();
        }

        private void Authorize()
        {
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = textBox1.Text
                }) ;
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Authorize();
            //var p = api.Users.Get(new long[] {api.UserId}).FirstOrDefault();
            //if (p == null)
            //    return;
            //button5.Text = p.FirstName + " " + p.LastName;
            button5.Text = "УСПЕШНО" ;
            button5.ForeColor = Color.White;
            button5.BackColor = Color.LightGreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button5.Text = "Войти";
            button5.BackColor = Color.Transparent;
            button5.ForeColor = Color.Black;
        }
    }
}
