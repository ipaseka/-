using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace _2.All_URI_on_WebPage__RegEx_
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
        private void OnBtnFind_Click(object sender, RoutedEventArgs e)
        {
            lUriList.ItemsSource = null;
            var uri = tbUri.Text;
            try
            {
                if (uri.Length == 0)
                    throw new Exception("Should input URI First!");
                else if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                    throw new Exception("URI is not Well Formed");

                using (HttpWebResponse response = (HttpWebResponse)WebRequest.Create(uri).GetResponse())
                {

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception($"Can not connect to URI {uri}{Environment.NewLine} Responce Status: {response.StatusCode}: {response.StatusDescription}");

                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    
                    readStream.Close();

                    string pattern = @"http[s]*://[^""'\s]+";
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                    lUriList.ItemsSource = regex.Matches(data);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
