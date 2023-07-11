using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace DriveExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declare Private variables
        private bool IsMaximazed = false;   // Variable to keep track of the window sizw
        private ObservableCollection<File> FilesList = new ObservableCollection<File>();
        private int TotalDetectedFiles;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximazed)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximazed = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximazed = true;
                }
            }
        }


        private void FileExplorerButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialogue = new FolderBrowserDialog();
            
            if (folderDialogue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderAdress.Text = folderDialogue.SelectedPath;

                // Check for all available files
                CheckAllFiles(folderDialogue.SelectedPath);
                FileDataGrid.ItemsSource = FilesList;
            }
        }

        private void CheckAllFiles(string FolderAddress)
        {
            var files = System.IO.Directory.GetFiles(FolderAddress, ".*", System.IO.SearchOption.AllDirectories);
            TotalDetectedFiles = files.Length;
            int count = 1;
            foreach (string filePath in files)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
                
                FilesList.Add(new File { Number = string.Format("{0}", count),  Name = fileInfo.Name,
                            Date = string.Format("{0}", fileInfo.LastWriteTime), Type = System.IO.Path.GetExtension(filePath), 
                            Size = string.Format("{0}", fileInfo.Length)});

                count++;

            }
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(textBoxSearch.Text))
            {
                // Do nothing
            }
            else
            {
                ObservableCollection<File> searchResults = new ObservableCollection<File>();

                foreach (File file in FilesList)
                {
                    if (file.Name.Contains(textBoxSearch.Text))
                    {
                        searchResults.Add(file);
                    }
                }
                FileDataGrid.ItemsSource = searchResults;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Check for all available files
            CheckAllFiles(FolderAdress.Text);
            FileDataGrid.ItemsSource = FilesList;
        }

        public class File
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Type { get; set; }
            public string Size { get; set; }

            internal static bool Exists(string v)
            {
                throw new NotImplementedException();
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TextBoxSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                SearchButton_Click(sender, e);
            }
        }
    }
}
