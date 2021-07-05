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
    public partial class Form3 : Form
    {
        bool choose = true;
        VkApi api;
        public Form3(VkApi api)
        {
            this.api = api;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = null;
            if(choose)
            {
                var getFriends = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    Fields = VkNet.Enums.Filters.ProfileFields.All
                });
                foreach (User user in getFriends)
                {
                    if(checkBox1.Checked)
                    {
                        result += user.FirstName;
                    }
                    if (checkBox2.Checked)
                        result += " " + user.LastName;
                    if(checkBox3.Checked)
                    {
                        result += " " + user.Id;
                    }
                    result += "\n";
                }
                MessageBox.Show(result, "Список ваших друзей", MessageBoxButtons.OK) ;
                    //Console.WriteLine(user.FirstName);
            }
            else
            {
                var getFollowers = api.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = "205575031",
                    Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
                });
                foreach (User user in getFollowers)
                {
                    if (checkBox1.Checked)
                    {
                        result += user.FirstName;
                    }
                    if (checkBox2.Checked)
                        result += " " + user.LastName;
                    if (checkBox3.Checked)
                    {
                        result += " " + user.Id;
                    }
                    result += "\n";
                }
                MessageBox.Show(result, "Список подписчиков сообщества", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var getFollowers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = "205575031",
                Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            string result = null;
            foreach (User user in getFollowers)
            {
                result += user.FirstName + " " + user.LastName + " " + user.Id;
                result += "\n";
            }
            MessageBox.Show(result, "Список подписчиков сообщества", MessageBoxButtons.OK);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            choose = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            choose = true;
        }
    }
}
