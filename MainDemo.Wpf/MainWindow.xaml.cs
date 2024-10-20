﻿using System;
using System.Diagnostics;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignDemo
{
    public partial class MainWindow
    {
        public static Snackbar Snackbar = new();
        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("Welcome to Material Design In XAML Toolkit");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel();

            Snackbar = MainSnackbar;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen flag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
            => DemoItemsSearchBox.Focus();
    }
}
