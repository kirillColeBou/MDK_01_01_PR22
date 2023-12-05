using ClassModule;
using PhoneBook_Тепляков.Pages;
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

namespace PhoneBook_Тепляков.Elements
{
    /// <summary>
    /// Логика взаимодействия для Delete_itm.xaml
    /// </summary>
    public partial class Delete_itm : UserControl
    {
        public static Delete_itm itm;
        public Delete_itm()
        {
            InitializeComponent();
            itm = this;
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            opgridAnimation.From = 0;
            opgridAnimation.To = 1;
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.4);
            border.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
        }

        private void Click_del(object sender, RoutedEventArgs e)
        {
            Main.main.parrent.Children.Clear();
            Main.main.ClearFilter();
            var add = new Pages.PagesUser.Filter_win(new Search_filter(), new Call());
            Main.main.parrent.Children.Add(new Elements.Add_itm(add));
        }
    }
}
