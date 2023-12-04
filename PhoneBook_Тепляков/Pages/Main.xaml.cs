﻿using ClassModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

namespace PhoneBook_Тепляков.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public enum page_main
        {
            users, calls, filters, none
        }

        public static page_main page_select;

        public Main()
        {
            InitializeComponent();
            page_select = page_main.none;
        }

        private void Click_Phone(object sender, RoutedEventArgs e)
        {
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if(page_select != page_main.users)
            {
                page_select = page_main.users;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        Dispatcher.InvokeAsync(async () =>
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tables.users);
                            foreach (User user_itm in MainWindow.connect.users)
                            {
                                if (page_select == page_main.users)
                                {
                                    parrent.Children.Add(new Elements.User_itm(user_itm));
                                    await Task.Delay(90);
                                }
                            }
                            if(page_select == page_main.users)
                            {
                                var ff = new Pages.PagesUser.User_win(new User());
                                parrent.Children.Add(new Elements.Add_itm(ff));
                            }
                        });
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void Click_History(object sender, RoutedEventArgs e)
        {
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.calls)
            {
                page_select = page_main.calls;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        Dispatcher.InvokeAsync(async () =>
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tables.calls);
                            foreach (Call call_itm in MainWindow.connect.calls)
                            {
                                if (page_select == page_main.calls)
                                {
                                    parrent.Children.Add(new Elements.Call_itm(call_itm));
                                    await Task.Delay(90);
                                }
                            }
                            if (page_select == page_main.calls)
                            {
                                var ff = new Pages.PagesUser.Call_win(new Call());
                                parrent.Children.Add(new Elements.Add_itm(ff));
                            }
                        });
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void Click_Range_Date(object sender, RoutedEventArgs e)
        {
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.filters)
            {
                page_select = page_main.filters;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        Dispatcher.InvokeAsync(async () =>
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tables.calls);
                            MainWindow.connect.LoadData(ClassConnection.Connection.tables.users);
                            await Task.Delay(90);
                            if (page_select == page_main.filters)
                            {
                                var ff = new Pages.PagesUser.Filter_win(new Call(), new User());
                                parrent.Children.Add(new Elements.Add_itm(ff));
                            }
                        });
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        public void Anim_move(Control control1, Control control2, Frame frame_main = null, Page pages = null, page_main page_restart = page_main.none)
        {
            if(page_restart != page_main.none)
            {
                if(page_restart == page_main.users)
                {
                    page_select = page_main.none;
                    Click_Phone(new object(), new RoutedEventArgs());
                }
                else if(page_restart == page_main.calls)
                {
                    page_select = page_main.none;
                    Click_History(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.filters)
                {
                    page_select = page_main.none;
                    Click_Range_Date(new object(), new RoutedEventArgs());
                }
            }
            else
            {
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.3);
                opgridAnimation.Completed += delegate
                {
                    if(pages != null)
                    {
                        frame_main.Navigate(pages);
                    }
                    control1.Visibility = Visibility.Hidden; 
                    control2.Visibility = Visibility.Visible;
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.4);
                    control2.BeginAnimation(ScrollViewer.OpacityProperty, opgriAnimation);
                };
                control1.BeginAnimation(ScrollViewer.OpacityProperty, opgridAnimation);
            }
        }
    }
}
