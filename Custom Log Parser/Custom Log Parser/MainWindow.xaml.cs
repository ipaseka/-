using Custom_Log_Parser.core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Custom_Log_Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<LogItem> ItemList { get; }
        public ObservableCollection<string> LogTypeList { get; }        
        private readonly ProgressControl progressControl;
        private FileControl fileControl;

        public MainWindow()
        {
            InitializeComponent();

            progressControl = new ProgressControl(PbProgress, TbProgress);
            ItemList = new List<LogItem>();
            LogTypeList = new ObservableCollection<string>();

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
            ResetView();
            try
            {
                IsEnabled = false;

                if (filePath == null)
                    fileControl = FileControl.FromDialog();
                else
                    fileControl = new FileControl(filePath);
                if (fileControl == null)
                    return;
                TbPath.Text = fileControl.FilePath;

                ItemList.AddRange(await fileControl.ParseAsync(progressControl));

                var sw = new Stopwatch();
                sw.Start();

                Parallel.ForEach(ItemList, s => s.Parse());

                sw.Stop();
                Debug.WriteLine($"Parse time: {sw.Elapsed}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsEnabled = true;
            }

            ItemListAddFilter();
            ItemListAddTypeList();
        }

        private void ItemListAddTypeList()
        {
            LogTypeList.Add("ALL");
            foreach (string item in ItemList.Select(x => x.Type).Distinct())
            {
                LogTypeList.Add(item);
            }
        }

        private void ItemListAddFilter()
        {
            if (TryFindResource("CvsList") is CollectionViewSource CvsList)
            {
                CvsList.View.Filter = o =>
                {
                    string selectedType = (string)LbLogType.SelectedItem ?? "ALL";
                    return selectedType == "ALL" || (o as LogItem).Type.Equals(selectedType);
                };
            }
        }

        private void ResetView()
        {
            ItemList.Clear();
            LogTypeList.Clear();
        }

        private void LbLogType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listLogs.ItemsSource).Refresh();
        }
    }
}
