using System.Numerics;
using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Controls;
using PropertyChanged;


using System.Windows.Media;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        MyViewModel view ;
        public MainWindow()
        {
            InitializeComponent();
            view = new();
            DataContext = view;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Content.ToString() == "Add") view.AddHexString();
            else  view.RemoveHexString(colorsList.SelectedIndex);
        }
    }

   

}
