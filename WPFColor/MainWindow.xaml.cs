using System.Numerics;
using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Controls;
using PropertyChanged;


using System.Windows.Media;
using System.Windows.Data;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {

        MyView view ;
        public MainWindow()
        {
           
            InitializeComponent();
            view = new(colorsList);
            this.DataContext = view;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Content.ToString() == "Add")
                colorsList.Items.Add(view.CurrentColorHex);
            else
                colorsList.Items.RemoveAt(colorsList.SelectedIndex);
            view.ItemCount = colorsList.Items.Count;
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class MyView
    {
        private ListBox colorList;
        public double Alpha { get; set; }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public int SelectedIndex { get; set; }
        public int ItemCount { get; set; }


        public MyView(ListBox list) 
        {
            Alpha = Red = Green = Blue = 0;
            colorList = list;
        }

        [DependsOn("alpha", "red", "green", "blue")]
        public SolidColorBrush CurrentColor => new(Color.FromArgb((byte)Alpha, (byte)Red, (byte)Green, (byte)Blue));

        [DependsOn("SelectedIndex")]
        public bool IsSelected => ItemCount != 0 && SelectedIndex >= 0;

        [DependsOn("CurrentColor", "ItemCount")]
        public bool IsNotColorExist => !colorList.Items.Contains(CurrentColorHex);

        public string CurrentColorHex => $"#{(byte)Alpha:X2}{(byte)Red:X2}{(byte)Green:X2}{(byte)Blue:X2}";
    }

}
