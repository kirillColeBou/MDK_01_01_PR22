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
using PhoneBook_Тепляков.Pages.PagesUser;

namespace PhoneBook_Тепляков.Elements
{
    /// <summary>
    /// Логика взаимодействия для Filter_itm.xaml
    /// </summary>
    public partial class Filter_itm : UserControl
    {
        Search_filter filter;
        public Filter_itm(Search_filter _filter)
        {
            InitializeComponent();
            filter = _filter;
            string category;
            if (_filter.time_end != null)
            {
                if (Convert.ToInt32(_filter.category_call) == 1) category = "Исходящий";
                else category = "Входящий";
                category_call_text.Content = category;
                string[] dateLoc1 = _filter.time_start.ToString().Split(' ');
                string[] dateLoc2 = _filter.time_end.ToString().Split(' ');
                string[] date1 = dateLoc1[0].Split('.');
                string[] date2 = dateLoc2[0].Split('.');
                DateTime dateStart = new DateTime(int.Parse(date1[2]), int.Parse(date1[1]), int.Parse(date1[0]), int.Parse(dateLoc1[1].Split(':')[0]), int.Parse(dateLoc1[1].Split(':')[1]), 0);
                DateTime dateFinish = new DateTime(int.Parse(date2[2]), int.Parse(date2[1]), int.Parse(date2[0]), int.Parse(dateLoc2[1].Split(':')[0]), int.Parse(dateLoc2[1].Split(':')[1]), 0);
                TimeSpan dateEnd = dateFinish.Subtract(dateStart);
                time_call_text.Content = "Продолжительность звонка: " + dateEnd.ToString();
                number_call_text.Content = "Номер телефона: " + _filter.phone_number.ToString();
            }
            img_category_call.Source = (Convert.ToInt32(_filter.category_call) == 1) ? new BitmapImage(new Uri(@"/Images/out.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"/Images/in.png", UriKind.RelativeOrAbsolute));
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            opgridAnimation.From = 0;
            opgridAnimation.To = 1;
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.4);
            border.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
        }
    }
}
