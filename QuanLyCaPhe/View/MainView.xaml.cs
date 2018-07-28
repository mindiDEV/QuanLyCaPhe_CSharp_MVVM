using QuanLyCaPhe.Model;
using QuanLyCaPhe.UserController;
using QuanLyCaPhe.View;
using QuanLyCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyCaPhe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Grid _gridMain;

        public static Grid _GridMain { get => _gridMain; set { _gridMain = value; } }

        
        public MainWindow()
        {
           
            InitializeComponent();

            _GridMain = GridMain;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }


    }
}