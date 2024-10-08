﻿using MedalDownloaderBypass.Downloader;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedalDownloaderBypass
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnDownloadButtonClick(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;
            if (!string.IsNullOrWhiteSpace(url) || url.Contains("https://medal.tv/"))
            {
                MessageBoxResult result = MessageBox.Show
                (
                    $"Are you sure you want to download:\n({url})?",
                    "Download",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    _ = SaveFile.SaveClipAsync(url); //Run download and save process
                }
                else if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid / empty URL", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void OnQuitAppButtonClick(object sender, RoutedEventArgs e)
        {
            try { Process.GetCurrentProcess().Kill(); } catch { Environment.Exit(0); }
        }
    }
}