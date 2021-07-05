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
        List<string> identificators= new List<string>();
        VkApi api;
        public Form4(VkApi api)
        {
            this.api = api;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            var getFollowers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = "205575031",
                Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            foreach (User user in getFollowers)
            {
                identificators.Add(user.Id.ToString());
            }

            var get = api.Wall.Get(new WallGetParams
            { OwnerId = Convert.ToInt64(identificators[0]),
            Count = 1
            });

            get.WallPosts.
            var post = _api.Wall.Post(new WallPostParams
            {

            });
        }
    }
}
