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

namespace Praktika
{
    public partial class Form4 : Form
    {
        long GroupId = 204431804;
        List<long> identificators= new List<long>();
        VkApi api;
        public Form4(VkApi api)
        {
            this.api = api;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var followers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = GroupId.ToString(),
                Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            foreach (User user in followers)
            {
                identificators.Add(user.Id));
            }

            string post_id = null;
            foreach (long id in identificators)
            {
                post_id = "wall" + id.ToString() + "_";
                var userPosts = api.Wall.Get(new WallGetParams
                {
                    OwnerId = id,
                    Count = 1
                });

                try
                {
                    post_id += userPosts.WallPosts[0].Id.ToString();
                }
                catch(Exception k)
                {
                    MessageBox.Show(k.Message, "Ошибка");
                }

                var repost = api.Wall.Repost(post_id, "test", GroupId ,false);
            }

            MessageBox.Show("Успешно!", "Задание 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        
    }
}
