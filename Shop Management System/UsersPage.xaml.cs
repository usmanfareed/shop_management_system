using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DLL.DAL;
using DLL.DataModel;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public static int Id;
        public static int UserId;


        public UsersPage()
        {
            InitializeComponent();
            DropDownLoad();
            LoadGroupsGrid();
            LoadUsers();
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            List<int> list = new List<int>();
            string Name = groupName.Text;


            list.Add(((bool) Billing.IsChecked) ? 1 : 0);
            list.Add(((bool) Inventory.IsChecked) ? 2 : 0);
            list.Add(((bool) Reports.IsChecked) ? 3 : 0);
            list.Add(((bool) Users.IsChecked) ? 4 : 0);

            string message = GroupAndPermissionModel.InsertProduct(Id, Name, list);
            Reset();

            MessageBox.Show(message);
            LoadGroupsGrid();
        }

        private void DropDownLoad()
        {
            var data = GroupAndPermissionModel.GetGroups();
            GroupsDrop.ItemsSource = data;
            GroupsDrop.SelectedValue = data.Select(x => x.GroupId).First();
            GroupsDrop.SelectedValue = null;

        }

        private void LoadGroupsGrid()
        {
            var groups = GroupAndPermissionModel.GetGroupsPermission();

            List<Group> list = new List<Group>();
            string GroupName = "";

            foreach (var group in groups)
            {
                if (GroupName != group.GroupName)
                {
                    Group groupData = new Group();

                    GroupName = group.GroupName;
                    int GroupId = group.GroupId;


                    string Permissions = string.Join("  ,  ", groups.Where(x => x.GroupName.Equals(GroupName))
                        .Select(x => x.PermissionName).ToList());
                    groupData.GroupName = GroupName;
                    groupData.PermissionName = Permissions;
                    groupData.GroupId = GroupId;
                    list.Add(groupData);
                }
            }

            GroupsGrid.ItemsSource = list;
        }

        private void GroupsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Reset();
            CreateGroup.Content = "Update";
            Group group = GroupsGrid.SelectedItem as Group;

            Id = group.GroupId;

            groupName.Text = group.GroupName;

            string[] permissions = group.PermissionName.Split(',');

            foreach (var permission in permissions)
            {
                switch (permission.Trim())
                {
                    case "BillingSection":
                    {
                        Billing.IsChecked = true;
                    }
                        break;

                    case "InventorySection":
                    {
                        Inventory.IsChecked = true;
                    }
                        break;

                    case "ReportsSection":
                    {
                        Reports.IsChecked = true;
                    }
                        break;

                    case "Users":
                    {
                        Users.IsChecked = true;
                    }
                        break;
                }
            }
        }

        private void resetGroupGrid_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            Id = 0;
            groupName.Text = null;
            Billing.IsChecked = false;
            Inventory.IsChecked = false;
            Reports.IsChecked = false;
            Users.IsChecked = false;
            CreateGroup.Content = "Create";
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {

            User user = new User();
            user.UserId = UserId;
            user.UserName = tb_Name.Text;
            user.UserPassword = tb_Password.Password;
            user.GroupId = Convert.ToInt32(GroupsDrop.SelectedValue);

            bool check = UserModel.CreateUser(user);

            MessageBox.Show(check ? "Success" : "Failed");
            LoadUsers();
            UserSectionReset();

        }


        private void LoadUsers()
        {
            DataTable table = UserModel.GetUsers();

            UserGrid.DataContext = table.DefaultView;
        }

        private void UserGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView data = (DataRowView)UserGrid.CurrentItem;
            var row = data.Row;
            UserId = Convert.ToInt32(row.ItemArray[0]);
            tb_Name.Text = row.ItemArray[1].ToString();
            GroupsDrop.SelectedValue = Convert.ToInt32(row.ItemArray[2]);

            CreateUser.Content = "Update";
        }

        private void UserSectionReset()
        {
            UserId = 0;
            tb_Name.Text = null;
            tb_Password.Password = null;
            GroupsDrop.SelectedValue = null;
            CreateUser.Content = "Create";


        }

        private void ResetUser_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
            UserSectionReset();

        }
    }
}
