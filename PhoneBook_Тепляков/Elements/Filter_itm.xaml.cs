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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassModule;

namespace PhoneBook_Тепляков.Elements
{
    /// <summary>
    /// Логика взаимодействия для Filter_itm.xaml
    /// </summary>
    public partial class Filter_itm : UserControl
    {
        Call call_loc;
        User user_loc;
        public Filter_itm(Call _call_loc, User _user_loc)
        {
            InitializeComponent();
            call_loc = _call_loc;
            user_loc = _user_loc;
            if (_call_loc.time_end != null)
            {
                user_loc = MainWindow.connect.users.Find(x => x.id == _call_loc.user_id);
                category_call_text.Content = user_loc.fio_user.ToString();
                string[] dateLoc1 = _call_loc.time_start.ToString().Split(' ');
                string[] dateLoc2 = _call_loc.time_end.ToString().Split(' ');
                string[] date1 = dateLoc1[0].Split('.');
                string[] date2 = dateLoc2[0].Split('.');
                DateTime dateStart = new DateTime(int.Parse(date1[2]), int.Parse(date1[1]), int.Parse(date1[0]), int.Parse(dateLoc1[1].Split(':')[0]), int.Parse(dateLoc1[1].Split(':')[1]), 0);
                DateTime dateFinish = new DateTime(int.Parse(date2[2]), int.Parse(date2[1]), int.Parse(date2[0]), int.Parse(dateLoc2[1].Split(':')[0]), int.Parse(dateLoc2[1].Split(':')[1]), 0);
                TimeSpan dateEnd = dateFinish.Subtract(dateStart);
                time_call_text.Content = "Продолжительность звонка: " + dateEnd.ToString();
                number_call_text.Content = "Номер телефона: " + user_loc.phone_num.ToString();
            }
            img_category_call.Source = (_call_loc.category_call == 1) ? new BitmapImage(new Uri(@"/Images/out.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"/Images/in.png", UriKind.RelativeOrAbsolute));
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            opgridAnimation.From = 0;
            opgridAnimation.To = 1;
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.4);
            border.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
        }
    }
}
