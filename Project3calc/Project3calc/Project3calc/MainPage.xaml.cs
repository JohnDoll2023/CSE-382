using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project3calc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // enum for math operation
        enum Operation
        {
            divide,
            multiply,
            subtract,
            add
        }

        // variables for running operations and memory
        int memory = 0;
        int initialValue;
        Operation op;

        // operations for clear button and numbers
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "C")
            {
                nLabel.Text = "0";
            }
            else if (b.Text == "+/-")
            {
                if (nLabel.Text != "0")
                {
                    int num = int.Parse(nLabel.Text);
                    if (num > 0)
                    {
                        num -= 2 * num;
                    } else
                    {
                        num += -2 * num;
                    }
                    nLabel.Text = num.ToString();
                }
            }
            else if (nLabel.Text == "0")
            {
                nLabel.Text = b.Text;
            }
            else
            {
                nLabel.Text += b.Text;
            }
        }

        // does operations for finance buttons
        void Finance_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "START")
            {
                saLabel.Text = "$" + nLabel.Text;
                nLabel.Text = "0";
            } else if (b.Text == "YEARS")
            {
                yLabel.Text = nLabel.Text;
                nLabel.Text = "0";
            } else if (b.Text == "RATE")
            {
                rorLabel.Text = nLabel.Text + "%";
                nLabel.Text = "0";
            }
            else if (b.Text == "INVEST")
            {
                riLabel.Text = "$" + nLabel.Text;
                nLabel.Text = "0";
            }
        }

        // pop up for frequency button
        async void Freq_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string action = await DisplayActionSheet("Frequency", "Cancel", null, "Monthly", "Quarterly", "Annually");
            if (action != "Cancel")
            {
                fLabel.Text = action;
            }
        }

        // does arithmetic for arithmetic buttons
        void Arithmetic_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "FINAL")
            {
                int sa = int.Parse(saLabel.Text.Substring(1));
                int y = int.Parse(yLabel.Text);
                double ror = double.Parse(rorLabel.Text.Substring(0, rorLabel.Text.Length - 1)) / 100;
                int ri = int.Parse(riLabel.Text.Substring(1));
                string freq = fLabel.Text;
                int freqPerYear = 1;
                if (freq == "Monthly")
                {
                    freqPerYear = 12;
                } else if (freq == "Quarterly")
                {
                    freqPerYear = 4;
                }
                fbLabel.Text = "$" + Compute(sa, y, ror, ri, freqPerYear).ToString();
            } else if (b.Text == "M+")
            {
                memory += int.Parse(nLabel.Text);
            } else if (b.Text == "M-")
            {
                memory -= int.Parse(nLabel.Text);
            } else if (b.Text == "MR")
            {
                nLabel.Text = memory.ToString();
            } else if (b.Text == "MC")
            {
                memory = 0;
            } else if (b.Text == "/")
            {
                op = Operation.divide;
                initialValue = int.Parse(nLabel.Text);
                nLabel.Text = "0";
            } else if(b.Text == "X")
            {
                op = Operation.multiply;
                initialValue = int.Parse(nLabel.Text);
                nLabel.Text = "0";
            } else if (b.Text == "-")
            {
                op = Operation.subtract;
                initialValue = int.Parse(nLabel.Text);
                nLabel.Text = "0";
            } else if (b.Text == "+")
            {
                op = Operation.add;
                initialValue = int.Parse(nLabel.Text);
                nLabel.Text = "0";
            } else if (b.Text == "=")
            {
                int secondValue = int.Parse(nLabel.Text);
                if (op == Operation.add)
                {
                    nLabel.Text = (initialValue + secondValue).ToString();
                } else if (op == Operation.subtract)
                {
                    nLabel.Text = (initialValue - secondValue).ToString();
                }
                else if (op == Operation.multiply)
                {
                    nLabel.Text = (initialValue * secondValue).ToString();
                }
                else if (op == Operation.divide)
                {
                    if (secondValue == 0)
                    {
                        nLabel.Text = "0";
                    } else
                    {
                        nLabel.Text = (initialValue / secondValue).ToString();
                    }
                }
            }
        }

        // does final value of investment computation
        private int Compute(int start, int years, double perc, int investment, int depositsPerYear)
        {
            double bal = start;
            double monthlyRate = perc / 12.0;
            int monthsToDeposit = 12 / depositsPerYear;
            for (int y = 0; y < years; y++)
            {
                for (int m = 1; m <= 12; m++)
                {
                    bal += bal * monthlyRate;
                    if (m % monthsToDeposit == 0)
                    {
                        bal += investment;      // make deposits at the end of the month
                    }
                }
            }
            return (int)Math.Round(bal);
        }

        // turns off finance buttons if current number less than 0
        void nLabel_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int num = 0;
            if (nLabel.Text != null)
            {
                num = int.Parse(nLabel.Text);
            } 
            if (num < 0)
            {
                start.IsEnabled = false;
                years.IsEnabled = false;
                rate.IsEnabled = false;
                invest.IsEnabled = false;
            } else
            {
                start.IsEnabled = true;
                years.IsEnabled = true;
                rate.IsEnabled = true;
                invest.IsEnabled = true;
            }
        }
    }
}