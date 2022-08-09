using System;
using System.Diagnostics;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;

namespace MaterialDesign3Demo
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

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue!);

            Snackbar = MainSnackbar;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NavDrawer.OpenMode is not DrawerHostOpenMode.Standard)
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

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            DemoItemsSearchBox.Focus();
            if (ActualWidth > 1600)
            {
                NavRail.Visibility = Visibility.Collapsed;
                MenuToggleButton.Visibility = Visibility.Collapsed;
            }

        }


        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser(ConfigurationManager.AppSettings["GitHub"]);

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth <= 700)
            {
                NavRail.Visibility = Visibility.Collapsed;
                NavBar.Visibility = Visibility.Visible;
                NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
                NavDrawer.IsLeftDrawerOpen = false;
                MenuToggleButton.Visibility = Visibility.Visible;
                FAB.Visibility = Visibility.Visible;
                DrawerFAB.Visibility = Visibility.Collapsed;
            }
            else if (ActualWidth > 700 && ActualWidth <= 1600)
            {
                NavRail.Visibility = Visibility.Visible;
                NavBar.Visibility = Visibility.Collapsed;
                NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
                NavDrawer.IsLeftDrawerOpen = false;
                MenuToggleButton.Visibility = Visibility.Visible;
                FAB.Visibility = Visibility.Collapsed;
                DrawerFAB.Visibility = Visibility.Collapsed;
            }
            else if (ActualWidth > 1600)
            {
                NavRail.Visibility = Visibility.Collapsed;
                NavBar.Visibility = Visibility.Collapsed;
                NavDrawer.OpenMode = DrawerHostOpenMode.Standard;
                NavDrawer.IsLeftDrawerOpen = true;
                MenuToggleButton.Visibility = Visibility.Collapsed;
                FAB.Visibility = Visibility.Collapsed;
                DrawerFAB.Visibility = Visibility.Visible;
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            NavDrawer.IsLeftDrawerOpen = false;
            if (ActualWidth > 1600)
            {
                NavRail.Visibility = Visibility.Visible;
                MenuToggleButton.Visibility = Visibility.Visible;
            }

        }

        private void CloseNotificationPanel_Click(object sender, RoutedEventArgs e) => NotificationPanel.Visibility = Visibility.Collapsed;
    }
}
