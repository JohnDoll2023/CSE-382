using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace MVVM
{
	class SimpleAdderViewModel : INotifyPropertyChanged
	{
		int adder, addee, sum;

		public event PropertyChangedEventHandler PropertyChanged;

		public int Adder
		{
			set
			{
				if (adder != value)
				{
					adder = value;
					OnPropertyChanged("Adder");
					UpdateSum();
				}
			}
			get
			{
				return adder;
			}
		}

		public int Addee
		{
			set
			{
				if (addee != value)
				{
					addee = value;
					OnPropertyChanged("Addee");
					UpdateSum();
				}
			}
			get
			{
				return addee;
			}
		}

		public int Sum
		{
			protected set
			{
				if (sum != value)
				{
					sum = value;
					OnPropertyChanged("Sum");
				}
			}
			get
			{
				return sum;
			}
		}


		void UpdateSum()
		{
			Sum = Adder + Addee;
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

