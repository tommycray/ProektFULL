using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace кэ4
{
    /// <summary>
    /// Логика взаимодействия для Artist.xaml
    /// </summary>
    public partial class Artist : Window
    {
        Entities entities = new Entities();
        public Artist()
        {
            InitializeComponent();
            foreach (var entity in entities.Zayavka)
                listbox1.Items.Add(entity);
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = listbox1.SelectedItem as Zayavka;
            if (selected != null)
            {
                txtComment.Text = selected.ZayavkaComment;
            }
            else
            {
                txtComment.Text = "";
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var seletedItem = listbox1.SelectedItem as Zayavka;
            if (txtComment.Text == "")
            {
                MessageBox.Show("Не выбрана запись!");
                return;
            }
            else
            {
                if (seletedItem == null) 
                {
                    seletedItem = new Zayavka();
                    entities.Zayavka.Add(seletedItem);
                    listbox1.Items.Add(seletedItem);
                }
                seletedItem.ZayavkaComment = txtComment.Text.Trim();
                entities.SaveChanges();
                listbox1.Items.Refresh();
                listbox1.SelectedIndex = -1;
                txtComment.Text = "";
                MessageBox.Show("Данные сохранены!");
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            listbox1.SelectedIndex = -1;
            txtComment.Text = "";
        }
    }
}
