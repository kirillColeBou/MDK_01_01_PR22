using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassModule;

namespace PhoneBook_Тепляков.Pages.PagesUser
{
    /// <summary>
    /// Логика взаимодействия для Filter_win.xaml
    /// </summary>
    public partial class Filter_win : Page
    {
        Call call_itm;
        User user_itm;
        public static Filter_win filter_Win;

        public Filter_win(Call _call, User _user)
        {
            InitializeComponent();
            call_itm = _call;
            user_itm = _user;
            filter_Win = this;
            if (_call.time_start != null)
            {
                string[] dateTimeStart = _call.time_start.Split(' ');
                string[] dateStart = dateTimeStart[0].Split('.');
                date_start_range.SelectedDate = new DateTime(int.Parse(dateStart[1]), int.Parse(dateStart[2]), int.Parse(dateStart[0]));
                time_start.Text = dateTimeStart[1];
                string[] dateTimeFinish = _call.time_end.Split(' ');
                string[] dateFinish = dateTimeFinish[0].Split('.');
                date_end_range.SelectedDate = new DateTime(int.Parse(dateFinish[1]), int.Parse(dateFinish[2]), int.Parse(dateFinish[0]));
                time_finish.Text = dateTimeFinish[1];
            }
            else
            {
                time_start.Text = "00:00:00";
                time_finish.Text = "00:00:00";
            }
            foreach (User item in MainWindow.connect.users)
            {
                ComboBoxItem combUser_phone = new ComboBoxItem();
                combUser_phone.Tag = item.id;
                combUser_phone.Content = item.phone_num;
                if (_call.user_id == item.id) combUser_phone.IsSelected = true;
                number_phone.Items.Add(combUser_phone);
            }
            ComboBoxItem combItm_is = new ComboBoxItem();
            combItm_is.Tag = 1;
            combItm_is.Content = "Исходящий";
            if (_call.category_call == 1) combItm_is.IsSelected = true;
            call_category.Items.Add(combItm_is);
            ComboBoxItem combItm_vh = new ComboBoxItem();
            combItm_vh.Tag = 2;
            combItm_vh.Content = "Входящий";
            if (_call.category_call == 2) combItm_vh.IsSelected = true;
            call_category.Items.Add(combItm_vh);
        }

        private void Click_Filter_Redact(object sender, RoutedEventArgs e)
        {
            if (!CheckTime(time_start.Text))
            {
                MessageBox.Show("Не корректный формат времени");
                return;
            }
            if (!CheckTime(time_finish.Text))
            {
                MessageBox.Show("Не корректный формат времени");
                return;
            }
            if (date_start_range.SelectedDate != null && date_end_range.SelectedDate != null)
            {
                DateTime dateStart = (System.DateTime)date_start_range.SelectedDate;
                DateTime dateFinish = (System.DateTime)date_end_range.SelectedDate;
                TimeSpan dateEnd = dateFinish.Subtract(dateStart);
                if (!dateEnd.ToString().Contains("-"))
                {
                    if(number_phone.Text == "")
                    {
                        MessageBox.Show("Запрос не был обработан. Вы не указали номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if(call_category.Text == "")
                    {
                        MessageBox.Show("Запрос не был обработан. Вы не указали категорию звонка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (call_itm.time_end == null)
                    {
                        var dateTimeFormatInfo = new System.Globalization.DateTimeFormatInfo();
                        dateTimeFormatInfo.ShortDatePattern = "MM/dd/yyyy";
                        string query = $"INSERT INTO [search_filter]([time_start], [time_end], [phone_number], [category_call]) SELECT [calls.time_start], [calls.time_end], [users.phone_num], [calls.category_call] FROM [users], [calls] WHERE [calls.category_call] = '{call_category.SelectedIndex + 1}' AND [users.phone_num] = '{number_phone.Text}' AND [calls.date] BETWEEN #{date_start_range.SelectedDate.Value.ToString("d", dateTimeFormatInfo)} {time_start.Text}# AND #{date_end_range.SelectedDate.Value.ToString("d", dateTimeFormatInfo)} {time_finish.Text}#";
                        var pc = MainWindow.connect.QueryAccess(query);
                        if (pc != null)
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tables.search_filter);
                            MessageBox.Show("Фильтр применен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.filters);
                        }
                        else MessageBox.Show("Фильтра не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else MessageBox.Show("Дата диапозона больше даты конца дипозона");
            }
            else MessageBox.Show("Вы не указали дату");
        }

        private void Click_Cancel_Filter_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        public bool CheckTime(string str)
        {
            string[] str1 = str.Split(':');
            if (str1.Length == 3)
                if (str1[0].Trim() != "" && str1[1].Trim() != "" && str1[2].Trim() != "")
                    if (int.Parse(str1[0]) >= 0 && int.Parse(str1[0]) <= 23)
                        if (int.Parse(str1[1]) >= 0 && int.Parse(str1[1]) <= 59)
                            if (int.Parse(str1[2]) >= 0 && int.Parse(str1[2]) <= 59)
                                return true;
                            else return false;
                        else return false;
                    else return false;
                else return false;
            else return false;
        }
    }
}
