using Custom_Log_Parser.core;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Custom_Log_Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogItem> ItemList { get; }
        private readonly ProgressControl progressControl;
        private FileControl fileControl;
        public MainWindow()
        {
            InitializeComponent();

            progressControl = new ProgressControl(PbProgress, TbProgress);
            ItemList = new ObservableCollection<LogItem>();
            DataContext = this;            
        }
        private void BtnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            CreateFileAndParseAsync();
        }

        private void BtnParse_Click(object sender, RoutedEventArgs e)
        {
            CreateFileAndParseAsync(TbPath.Text);
        }

        private async void CreateFileAndParseAsync(string filePath = null)
        {
            try
            {
                if (filePath == null)
                    fileControl = FileControl.FromDialog();
                else
                    fileControl = new FileControl(filePath);
                if (fileControl == null)
                    return;
                TbPath.Text = fileControl.FilePath;
                await fileControl.ParseIntoAsync(ItemList, progressControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
