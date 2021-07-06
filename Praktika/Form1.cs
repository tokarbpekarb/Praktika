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
            button1.Text = today;
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
                            button1.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName+user.LastName));
                        }
                    }
                }

            }
            Post(api, "");


        }
        
        
        static void Post(VkApi api, string userName)
        {
            
            PhotoAlbum al = new PhotoAlbum();
            al.Id = 0;
            
            api.Wall.Post(new WallPostParams
            {
                OwnerId = -205575031,
                Message = "Test post from 6th group. Work, you",
                //Attachments = new List<MediaAttachment>(new Photo { "photo539310031_457256583" })


            }) ;
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
