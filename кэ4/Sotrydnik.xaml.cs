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
using System.Windows.Shapes;

namespace кэ4
{
    /// <summary>
    /// Логика взаимодействия для Sotrydnik.xaml
    /// </summary>
    public partial class Sotrydnik : Window
    {
        Entities entities = new Entities();
        public Sotrydnik()
        {
            InitializeComponent();
            //foreach (var entity in entities.Zayavka)
            //    listbox1.Items.Add(entity);

            listbox1.ItemsSource = entities.Zayavka.ToList();

            foreach (var a in entities.Artists)
                cmbArtist.Items.Add(a);

            //foreach (var c in entities.Clients)
            //    cmbClient.Items.Add(c);

            cmbClient.ItemsSource = entities.Clients.ToList();
            

            foreach (var g in entities.Oboryd)
                cmbOboryd.Items.Add(g);

            foreach (var j in entities.Status)
                cmbStatus.Items.Add(j);

            foreach (var w in entities.Type)
                cmbType.Items.Add(w);

            foreach (var l in entities.Status) 
                cmbFilter.Items.Add(l);
            Ddate.DisplayDateStart = DateTime.Today;
       
        }

       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listbox1.SelectedItem as Zayavka;
            if (cmbArtist.SelectedIndex == -1 || cmbClient.SelectedIndex == -1 || cmbOboryd.SelectedIndex == -1 ||
               cmbStatus.SelectedIndex == -1 || cmbType.SelectedIndex == -1 || txtDescription.Text == "" || txtComment.Text == "" ||
               txtNubmer.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                int num;
                try
                {
                    if(!int.TryParse(txtNubmer.Text, out num))
                    {
                        MessageBox.Show("Введете корректный числовой формат!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        if( selectedItem == null)
                        {
                            selectedItem = new Zayavka();
                            entities.Zayavka.Add(selectedItem);
                            listbox1.Items.Add(selectedItem);
                        }
                        selectedItem.Clients = cmbClient.SelectedItem as Clients;
                        selectedItem.Artists = cmbArtist.SelectedItem as Artists;
                        selectedItem.Oboryd = cmbOboryd.SelectedItem as Oboryd;
                        selectedItem.Status = cmbStatus.SelectedItem as Status;
                        selectedItem.Type = cmbType.SelectedItem as Type;
                        selectedItem.ZayavkaDescription = txtDescription.Text.Trim();
                        selectedItem.ZayavkaComment = txtComment.Text.Trim();
                        selectedItem.ZayavkaDate = Ddate.SelectedDate;
                        selectedItem.ZayavkaNumber = num;
                        entities.SaveChanges();
                        ApplyFilterAndSearch();
                        listbox1.Items.Refresh();

                        MessageBox.Show("Данные сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = listbox1.SelectedItem as Zayavka;
            if (selected != null)
            {
                cmbArtist.SelectedItem = selected.Artists;
                cmbClient.SelectedItem = selected.Clients;
                cmbOboryd.SelectedItem = selected.Oboryd;
                cmbStatus.SelectedItem = selected.Status;
                cmbType.SelectedItem = selected.Type;
                txtDescription.Text = selected.ZayavkaDescription;
                txtComment.Text = selected.ZayavkaComment;
                Ddate.SelectedDate = selected.ZayavkaDate;
                txtNubmer.Text = selected.ZayavkaNumber.ToString();

            }
            else
            {
                cmbArtist.SelectedIndex = -1;
                cmbClient.SelectedIndex = -1;
                cmbOboryd.SelectedIndex = -1;
                cmbStatus.SelectedIndex = -1;
                cmbType.SelectedIndex = -1;
                txtDescription.Text = "";
                txtComment.Text = "";
                Ddate.SelectedDate = DateTime.Now;
                txtNubmer.Text = "";
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            cmbArtist.SelectedIndex = -1;
            cmbClient.SelectedIndex = -1;
            cmbOboryd.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            txtDescription.Text = "";
            txtComment.Text = "";
            Ddate.SelectedDate = DateTime.Now;
            txtNubmer.Text = "";
            listbox1.SelectedIndex = -1;
            txtNubmer.Focus();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
   
                ApplyFilterAndSearch();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                ApplyFilterAndSearch();
        }
        private void ApplyFilterAndSearch()
        {
            try
            {
                string searchText = txtSearch.Text.Trim().ToLower(); // Приводим текст поиска к нижнему регистру

                var filteredZayavki = entities.Zayavka.ToList();

                // Применяем фильтр, если выбран какой-либо из фильтров
                if (cmbFilter.SelectedItem != null)
                {
                    string selectedFilter = cmbFilter.SelectedItem.ToString().ToLower();

                    switch (selectedFilter)
                    {
                        case "в работе":
                            filteredZayavki = filteredZayavki.Where(z => z.ZayavkaStatusId == 1).ToList();
                            break;
                        case "выполнен":
                            filteredZayavki = filteredZayavki.Where(z => z.ZayavkaStatusId == 3).ToList();
                            break;
                        case "не выполнено":
                            filteredZayavki = filteredZayavki.Where(z => z.ZayavkaStatusId == 2).ToList();
                            break;
                        default:
                            break;
                    }
                }

                // Применяем поиск по имени клиента и описанию
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filteredZayavki = filteredZayavki.Where(z =>
                        z.Clients.ClientName.ToLower().Contains(searchText) || // Поиск по имени клиента
                        z.ZayavkaDescription.ToLower().Contains(searchText)   // Поиск по описанию
                    ).ToList();
                }

                // Обновляем источник данных для ListBox
                listbox1.ItemsSource = filteredZayavki;
                listbox1.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
