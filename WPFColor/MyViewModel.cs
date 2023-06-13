using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    [AddINotifyPropertyChangedInterface]
    internal class MyViewModel
    {
        private int index;
        private int oldIndex;
        private Color tmpCColor;

        private readonly RelayCommand addColor;
        private readonly RelayCommand delColor;
        private readonly RelayCommand clearSelection;
        private readonly ObservableCollection<Color> ColorList;
          
        public IEnumerable<Color> CList => ColorList;
        public double Alpha { get; set; }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        [DependsOn("SelectedIndex")]
        public Visibility HelpVisible => SelectedIndex >= 0? Visibility.Visible: Visibility.Hidden;
  
        public int SelectedIndex 
        {
            get
            {
                if (index >= 0 && index != oldIndex)
                {
                    Alpha = ColorList[index].A;
                    Red = ColorList[index].R;
                    Green = ColorList[index].G;
                    Blue = ColorList[index].B;
                }
                oldIndex = index;
                return index;
            }
            set => index = value;
        }

        public ICommand AddColor => addColor;
        public ICommand DelColor => delColor;
        public ICommand ClearSelection => clearSelection;

        public MyViewModel()
        {
            index = -1;
            ColorList = new();
            addColor = new((o) => { ColorList.Add(tmpCColor); }, (o) => !ColorList.Contains(tmpCColor));
            delColor = new((o) => { ColorList.RemoveAt(SelectedIndex); }, (o) => SelectedIndex >= 0);
            clearSelection = new((o) => { SelectedIndex = -1; }, (o) => SelectedIndex >= 0);
        }

        [DependsOn("alpha", "red", "green", "blue")]
        public Color CurrentColor
        {
            get
            {
                tmpCColor = Color.FromArgb((byte)Alpha, (byte)Red, (byte)Green, (byte)Blue);

                if (SelectedIndex >= 0)
                {
                    int tmp = SelectedIndex;
                    ColorList[SelectedIndex] = tmpCColor;
                    SelectedIndex = tmp;
                }
                
                return tmpCColor;
            }
         }
    }
}
