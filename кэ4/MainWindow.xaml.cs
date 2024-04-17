using System;
using System.Collections.Generic;
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

namespace кэ4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bntEntry_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (txtPassword.Text == "" || txtLogin.Text == "")
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {


                var user = entities.Users.FirstOrDefault(p => p.UsersLogin == login && p.UsersPassword == password);
                if (user != null)
                {
                    if (user.UsersRole.Trim() == "сотрудник")
                    {
                        MessageBox.Show("Вы вошли как сотрудник, " + user.UsersLogin);
                        var n = new Sotrydnik();
                        n.Show();
                        this.Close();
                    }
                    else if (user.UsersRole.Trim() == "исполнитель")
                    {
                        MessageBox.Show("Вы вошли как исполнитель, " + user.UsersLogin);
                        var n = new Artist();
                        n.Show();
                        this.Close();
                    }
                   
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtLogin.Text = "";
                    txtPassword.Text = "";
                }
            }
        }
    }
}
