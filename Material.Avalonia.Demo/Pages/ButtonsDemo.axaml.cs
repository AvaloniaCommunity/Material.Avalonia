using System;
using Avalonia.Controls;

namespace Material.Avalonia.Demo.Pages;

public partial class ButtonsDemo : UserControl {
    public ButtonsDemo() {
        InitializeComponent();
    }

    public void OnHyperlink1Clicked() {
        Console.WriteLine("Hyperlink #1 CLICKED!");
    }
    
    public void OnHyperlink2Clicked() {
        Console.WriteLine("Another Hyperlink is CLICKED!");
    }
}