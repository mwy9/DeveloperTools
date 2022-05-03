using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace WpfGeneration
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ComboBox> _Datas;

        public ObservableCollection<ComboBox> Datas
        {
            get { return _Datas; }
            set { _Datas = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetColor();
            this.Datas = new ObservableCollection<ComboBox>();
            for (int i = 0; i < 10; i++)
            {
                this.Datas.Add(new ComboBox()
                {
                    ID = "1",
                    Name = "zhi" + i,
                });
            }
            this.MultiComboBox1.ItemsSource = this.Datas;
            this.MultiComboBox2.ItemsSource = this.Datas;
            this.MultiComboBox3.ItemsSource = this.Datas;
            this.MultiComboBox4.ItemsSource = this.Datas;
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, true);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, false);
        }

        private void GetColor()
        {
            List<Color> list = new List<Color>
            {
                Color.FromRgb(0, 0, 0),
                Color.FromRgb(153, 51, 0),
                Color.FromRgb(51, 51, 0),
                Color.FromRgb(0, 51, 0),
                Color.FromRgb(0, 51, 102),
                Color.FromRgb(0, 0, 128),
                Color.FromRgb(51, 51, 153),
                Color.FromRgb(51, 51, 51),
                Color.FromRgb(128, 0, 0),
                Color.FromRgb(255, 102, 0),
                Color.FromRgb(128, 128, 0),
                Color.FromRgb(0, 128, 0),
                Color.FromRgb(0, 128, 128),
                Color.FromRgb(0, 0, 255),
                Color.FromRgb(102, 102, 153),
                Color.FromRgb(128, 128, 128),
                Color.FromRgb(255, 0, 0),
                Color.FromRgb(255, 153, 0),
                Color.FromRgb(153, 204, 0),
                Color.FromRgb(51, 153, 102),
                Color.FromRgb(51, 204, 204),
                Color.FromRgb(51, 102, 255),
                Color.FromRgb(128, 0, 128),
                Color.FromRgb(153, 153, 153),
                Color.FromRgb(255, 0, 255),
                Color.FromRgb(255, 204, 0),
                Color.FromRgb(255, 255, 0),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(0, 255, 255),
                Color.FromRgb(0, 204, 255),
                Color.FromRgb(153, 51, 102),
                Color.FromRgb(192, 192, 192),
                Color.FromRgb(255, 153, 204),
                Color.FromRgb(255, 204, 153),
                Color.FromRgb(255, 255, 153),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(204, 255, 204),
                Color.FromRgb(153, 204, 255),
                Color.FromRgb(204, 153, 255),
            };
            this.ColorSelector.ItemsSource = new ReadOnlyCollection<Color>(list);
        }

        private void FlatButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Color> list = new List<Color>();

                list.Add((Color)ColorSelector.SelectedValue);
                this.ColorSelector2.ItemsSource = new ReadOnlyCollection<Color>(list);// ColorSelector.SelectedItem;
                this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, false);
            }
            catch (Exception ex)
            {

            }
        }

        private void FlatButton_Click_2(object sender, RoutedEventArgs e)
        {
            /*
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Title = "请选择Excel文件";
            openFileDialog1.Filter = "图像文件|*.jpg;*.png;*.jpeg;*.bmp;*.gif|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {             
                this.image.Source =new BitmapImage(new Uri(openFileDialog1.FileName));
            }
            */
        }
    }


    public class ComboBox
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
