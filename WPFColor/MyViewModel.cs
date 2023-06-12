using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    [AddINotifyPropertyChangedInterface]
    internal class MyViewModel
    {
        private readonly RelayCommand addColor;
        private readonly RelayCommand delColor;
        private readonly ObservableCollection<string> ColorList;
        public IEnumerable<string> CList => ColorList;
        public double Alpha { get; set; }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public int SelectedIndex { get; set; }
       

        public ICommand AddColor => addColor;
        public ICommand DelColor => delColor;


        public MyViewModel()
        {
            ColorList = new();
            Alpha = Red = Green = Blue = 0;
            addColor = new((o) => { ColorList.Add(CurrentColor.ToString()); ; }, (o) => !ColorList.Contains(CurrentColor.ToString()) );
            delColor = new((o) => { ColorList.RemoveAt(SelectedIndex);}, (o) =>  SelectedIndex >= 0);
        }

        [DependsOn("alpha", "red", "green", "blue")]
        public SolidColorBrush CurrentColor => new(Color.FromArgb((byte)Alpha, (byte)Red, (byte)Green, (byte)Blue));
    }
}
