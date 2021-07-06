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
    public partial class AddTask : Form
    {
        int today = DateTime.Today.Year;
        VkApi api;
        
        public AddTask(VkApi api)
        {
            this.api = api;
            InitializeComponent();
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            var getFriends = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.BirthDate
                
        });
            int sum = 0;
            foreach (User user in getFriends)
            {
                if (user.BirthDate != null)
                {
                    int dotNumber = user.BirthDate.Where(c => c == '.').Count();
                    if (dotNumber == 2)
                    {
                        int i = user.BirthDate.IndexOf('.');
                        i = user.BirthDate.IndexOf('.', i + 1);
                        int userData = Convert.ToInt32(user.BirthDate.Substring(i+1));
                        sum += today - userData;
                    }
                }
                
            }
            sum /= getFriends.Count;
            textBox1.Text = sum.ToString();
        }
    }
}
