using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCP.Client;

namespace TCP.Client.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var _client = new Client();
            _client.Connect();

            TextBox textBoxAnswer = this.FindName("Client_Input") as TextBox;


            MessageBox.Show("Client Message: " + textBoxAnswer.Text);
            MessageBox.Show("Server Response: " + _client.SendStringWithEcho(textBoxAnswer.Text));
        }



    }
}
