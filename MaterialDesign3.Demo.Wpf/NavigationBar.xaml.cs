﻿using System;
using System.Collections.Generic;
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
using MahApps.Metro.IconPacks;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public List<SampleItem> SampleList { get; set; }
        public NavigationBar()
        {
            InitializeComponent();
            DataContext = this;

            SampleList = new()
            {
                new SampleItem
                {
                    Title = "Payment",
                    SelectedIcon = PackIconMaterialKind.CreditCard,
                    UnselectedIcon = PackIconMaterialKind.CreditCardOutline,
                },
                new SampleItem
                {
                    Title = "Home",
                    SelectedIcon = PackIconMaterialKind.Home,
                    UnselectedIcon = PackIconMaterialKind.HomeOutline,
                },
                new SampleItem
                {
                    Title = "Special",
                    SelectedIcon = PackIconMaterialKind.Star,
                    UnselectedIcon = PackIconMaterialKind.StarOutline,
                },
                new SampleItem
                {
                    Title = "Shared",
                    SelectedIcon = PackIconMaterialKind.AccountMultiple,
                    UnselectedIcon = PackIconMaterialKind.AccountMultipleOutline,
                },
                new SampleItem
                {
                    Title = "Files",
                    SelectedIcon = PackIconMaterialKind.Folder,
                    UnselectedIcon = PackIconMaterialKind.FolderOutline,
                },
                new SampleItem
                {
                    Title = "Library",
                    SelectedIcon = PackIconMaterialKind.Bookshelf,
                    UnselectedIcon = PackIconMaterialKind.Bookshelf,
                },
            };
        }
    }
}
