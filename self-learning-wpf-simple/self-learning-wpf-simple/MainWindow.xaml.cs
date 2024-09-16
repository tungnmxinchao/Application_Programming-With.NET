using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace self_learning_wpf_simple
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateInlineTextBlock();
        }

        private void CreateInlineTextBlock()
        {

            Run run1 = new Run("Hello ")
            {
                Foreground = Brushes.Blue
            };


            Hyperlink hyperlink = new Hyperlink(new Run("click here"))
            {
                NavigateUri = new System.Uri("https://facebook.com/"),
                Foreground = Brushes.Red
            };
            hyperlink.RequestNavigate += TestClickHere;


            Run run2 = new Run(" to search on Google.")
            {
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Green
            };


            txtBlock.Inlines.Add(run1);
            txtBlock.Inlines.Add(hyperlink);
            txtBlock.Inlines.Add(run2);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            txtBlock.Text = "tung dep trai";
        }

        public void profile(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.ToString()) { UseShellExecute = true });
        }

        private void TestClickHere(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello dai ca tung!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String input = tbInput.Text;
            int number;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Input is empty or contains only whitespace.");
                return;
            }
            if (int.TryParse(input, out number))
            {             
                if (number <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lớn hơn 0.");
                    return;
                }              
            }

            MessageBox.Show(input);
        }
    }
}