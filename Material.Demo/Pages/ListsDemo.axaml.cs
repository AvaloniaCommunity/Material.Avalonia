﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages
{
    public class ListsDemo : UserControl
    {
        public ListsDemo()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}