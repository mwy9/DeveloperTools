using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFBatch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        enum CheckedRadion
        {
            Continuity,

            Random
        }
        enum TypeSelect
        {
            Number,
            Capital,
            letter
        }
        public MainWindow()
        {
            InitializeComponent();

            this.Radion3.IsEnabled = false;
            this.Radion4.IsEnabled = false;
            this.Radion5.IsEnabled = false;
        }
        private CheckedRadion Change;
        private TypeSelect Select;
        private static char[] conletter =   
      {   
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'  
      };
        private static char[] conCapital =   
      {    
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
      };
        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Text.Clear();
            List<String> list = new List<string>();
            string text = "";
            if (Change == CheckedRadion.Continuity)
            {
                int number = Math.Abs(Convert.ToInt32(End.Text) - Convert.ToInt32(Start.Text));
                for (int i = 0; i < number; i++)
                {
                    text = Prefix.Text + i.ToString().PadLeft(3, '0') + Suffix.Text;
                    list.Add(text);
                }
                foreach (string item in list)
                {
                    this.Text.Text += item + "\n";
                }

            }
            if (Change == CheckedRadion.Random)
            {
                Random random = new Random();
                if (Select == TypeSelect.Number)
                {
                    int end = Convert.ToInt32(1.ToString().PadRight(Convert.ToInt32(Long.Text), '0'));

                    for (int i = 0; i < Convert.ToInt32(Count.Text); i++)
                    {
                        int n = random.Next(0, end);
                        text = PrefixRandom.Text + n + SuffixRandom.Text;
                        list.Add(text);
                    }

                    foreach (string item in list)
                    {
                        this.Text.Text += item + "\n";
                    }
                }
                else if (Select == TypeSelect.Capital)
                {
                    //int end = Convert.ToInt32(1.ToString().PadRight(Convert.ToInt32(Long.Text), '0'));

                    for (int i = 0; i < Convert.ToInt32(Count.Text); i++)
                    {
                        string nn = GenerateRandom(conCapital, Convert.ToInt32(Long.Text)) + SuffixRandom.Text;
                        //int n = conCapital[random.Next(0, Convert.ToInt32(Long.Text))];
                        text = PrefixRandom.Text + nn ;
                        list.Add(text);
                    }

                    foreach (string item in list)
                    {
                        this.Text.Text += item + "\n";
                    }
                    //constant[rd.Next(52)]
                }else if (Select == TypeSelect.letter)
                {
                    for (int i = 0; i < Convert.ToInt32(Count.Text); i++)
                    {
                        //int n = conletter[random.Next(0, Convert.ToInt32(Long.Text))];
                        text = PrefixRandom.Text + GenerateRandom(conletter, Convert.ToInt32(Long.Text)) + SuffixRandom.Text;
                        list.Add(text);
                    }

                    foreach (string item in list)
                    {
                        this.Text.Text += item + ",";
                    }
                }

            }
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, true);
        }

        public static string GenerateRandom(char[] constant, int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(26);


            //TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //return Convert.ToInt64(ts.TotalSeconds).ToString();
            //int mmm = Convert.ToInt32(ts.TotalSeconds);

            Thread.Sleep(50);

            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(26)]);
            }
            return newRandom.ToString();
        }   

        private void FlatRadionButton_Checked(object sender, RoutedEventArgs e)
        {
            Change = CheckedRadion.Continuity;
        }

        private void FlatRadionButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Change = CheckedRadion.Random;
            this.Radion3.IsEnabled = true;
            this.Radion4.IsEnabled = true;
            this.Radion5.IsEnabled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, false);
        }

        private void FlatRadionButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Select = TypeSelect.Number;
        }

        private void FlatRadionButton_Checked_3(object sender, RoutedEventArgs e)
        {
            Select = TypeSelect.letter;
        }

        private void FlatRadionButton_Checked_4(object sender, RoutedEventArgs e)
        {
            Select = TypeSelect.Capital;        
        }

    }
}
