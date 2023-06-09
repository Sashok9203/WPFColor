using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    [AddINotifyPropertyChangedInterface]
    internal class MyViewModel
    {

        private ObservableCollection<string> ColorList;
        public IEnumerable<string> CList => ColorList;
        public double Alpha { get; set; }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public int SelectedIndex { get; set; }
        public int ItemsCount { get; set; }


        public MyViewModel()
        {
            ColorList = new();
            Alpha = Red = Green = Blue = 0;
        }

        [DependsOn("alpha", "red", "green", "blue")]
        public SolidColorBrush CurrentColor => new(Color.FromArgb((byte)Alpha, (byte)Red, (byte)Green, (byte)Blue));

        [DependsOn("SelectedIndex")]
        public bool IsSelected =>  SelectedIndex >= 0;

        [DependsOn("CurrentColor", "ItemsCount")]
        public bool IsNotColorExist => !ColorList.Contains(CurrentColorHex);

        public string CurrentColorHex => $"#{(byte)Alpha:X2}{(byte)Red:X2}{(byte)Green:X2}{(byte)Blue:X2}";

        public void AddHexString() 
        {
            ColorList.Add(CurrentColorHex);
            ItemsCount = ColorList.Count;
        }

        public void RemoveHexString(int index)
        {
            ColorList.RemoveAt(index);
            ItemsCount = ColorList.Count;
        } 
    }
}
