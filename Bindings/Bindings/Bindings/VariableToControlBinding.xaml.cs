using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bindings {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyData : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private int data;
        public int Value {
            get {
                return data;
            }
            set {
                if (data != value) {
                    data = value;
                    RaisePropertyChanged();
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class VariableToControlBinding : ContentPage {
        MyData myData = new MyData { Value = 0 };
        MyPoint myPoint = new MyPoint { ValueX = 0, ValueY = 0 };
        public VariableToControlBinding() {
            InitializeComponent();
            varValue.SetBinding(Label.TextProperty, new Binding { Source = myData, Path = "Value" });
            xCoord.SetBinding(Label.TextProperty, new Binding { Source = myPoint, Path = "ValueX" });
            yCoord.SetBinding(Label.TextProperty, new Binding { Source = myPoint, Path = "ValueY" });
            Random random = new Random();
            Device.StartTimer(new TimeSpan(0, 0, 1), () => { myData.Value += 1; myPoint.ValueX = random.NextDouble(); myPoint.ValueY = random.NextDouble();  return true; });
        }
    }

    public class MyPoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double x;
        private double y;
        public double ValueX
        {
            get
            {
                return x;
            }
            set
            {
                if (x != value)
                {
                    x = value;
                    RaisePropertyChanged();
                }
            }
        }
        public double ValueY
        {
            get
            {
                return y;
            }
            set
            {
                if (y != value)
                {
                    y = value;
                    RaisePropertyChanged();
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
