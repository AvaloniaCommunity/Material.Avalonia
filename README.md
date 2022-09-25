[nuget]: https://www.nuget.org/packages/Material.Avalonia/

# ![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/dev/FavIcon.svg) Material.Avalonia

Customizable Material Design implementation for [AvaloniaUI](http://avaloniaui.net/) framework.

![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/dev/wiki/images/demo-screenshots/1.png)
###### [More screenshots](https://github.com/AvaloniaUtils/material.avalonia/wiki/Screenshots-of-Demo)

# ![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/dev/FavIcon.svg) Overview
This library is a collection of styles and controls to help you customize your Avalonia application with Material Design theme.
- Material Design styles for almost all Avalonia controls
- Additional controls to support the Snackbars, side sheets, floating buttons, cards, dialogs and more
- Easy configuration of palette (at design and runtime), according to Material Guidelines guidelines
- Full [Material Design Icons](https://materialdesignicons.com/) icon pack support (must be [installed separately](https://github.com/AvaloniaUtils/Material.Icons.Avalonia), in favor of small library size)
- Demo applications included in the source project

[![nuget](https://img.shields.io/nuget/v/Material.Avalonia?label=Nuget&style=flat-square)][nuget]
[![nuget](https://img.shields.io/nuget/vpre/Material.Avalonia?label=Nuget&style=flat-square)][nuget]
[![nuget](https://img.shields.io/nuget/dt/Material.Avalonia?color=blue&label=Downloads&style=flat-square)][nuget]

# ![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/dev/FavIcon.svg) Getting started
Check out the [getting started](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Getting-started) wiki page. Or follow these very quick instructions:
1. Add [Material.Avalonia][nuget] nuget package to your project:

       dotnet add package Material.Avalonia

2. Edit `App.xaml` file:
   > If you install 3.0.0-* version or higher, use this:
   ```xaml
   <Application ...
     xmlns:themes="clr-namespace:Material.Styles.Themes;assembly=Material.Styles"
     ...>
     <Application.Styles>
       <themes:MaterialTheme BaseTheme="Dark" PrimaryColor="Purple" SecondaryColor="Lime" />
     </Application.Styles>
   </Application>
   ```
   > If you install 2.5.1 or lower use this:
   ```xaml
   <Application ...
     xmlns:themes="clr-namespace:Material.Styles.Themes;assembly=Material.Styles"
     ...>
     <Application.Resources>
       <themes:BundledTheme BaseTheme="Light" PrimaryColor="Teal" SecondaryColor="Amber"/>
     </Application.Resources>
     <Application.Styles>
       <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.Templates.xaml" />
     </Application.Styles>
   </Application>
   ```

# ![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/dev/FavIcon.svg) Useful links
- [Advanced theming](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming) wiki page
- [Nightly packages](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Using-nightly-build-feed) wiki page
- [Material Design Icons](https://github.com/AvaloniaUtils/Material.Icons.Avalonia) icon pack support
- [DialogHost.Avalonia](https://github.com/AvaloniaUtils/DialogHost.Avalonia) that provides a simple way to display a dialog
