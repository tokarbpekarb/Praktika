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

        private void button4_Click(object sender, EventArgs e)
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
                return;
            }
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
