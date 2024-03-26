[Nuget.Material.Avalonia]: https://www.nuget.org/packages/Material.Avalonia/

[Nuget.Material.Avalonia.DataGrid]: https://www.nuget.org/packages/Material.Avalonia.DataGrid/

[Nuget.Material.Avalonia.Dialogs]: https://www.nuget.org/packages/Material.Avalonia.Dialogs/

[icon]: https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/master/wiki/FavIcon.svg

# ![][icon] Material.Avalonia

Customizable Material Design implementation for [AvaloniaUI](http://avaloniaui.net/) framework.

![](https://raw.githubusercontent.com/AvaloniaCommunity/Material.Avalonia/master/wiki/images/demo-screenshots/1.png)

- [More screenshots](https://github.com/AvaloniaUtils/material.avalonia/wiki/Screenshots-of-Demo)
- [Live demo in your browser](https://avaloniacommunity.github.io/Material.Avalonia/)  
  Currently, in the alpha stage, many things can be broken.

# ![][icon] Overview

This library is a collection of styles and controls to help you customize your Avalonia application with Material Design
theme.

- Material Design styles for almost all Avalonia controls
- Additional controls to support the Snackbars, side sheets, floating buttons, cards, dialogs and more
- Easy configuration of palette (at design and runtime), according to Material Guidelines guidelines
- Full [Material Design Icons](https://materialdesignicons.com/) icon pack support (must
  be [installed separately](https://github.com/SKProCH/Material.Icons), in favor of small library size)
- Demo applications included in the source project

[![Nuget.Material.Avalonia](https://img.shields.io/nuget/v/Material.Avalonia?label=Nuget&style=flat-square)][Nuget.Material.Avalonia]
[![Nuget.Material.Avalonia](https://img.shields.io/nuget/vpre/Material.Avalonia?label=Nuget&style=flat-square)][Nuget.Material.Avalonia]
[![Nuget.Material.Avalonia](https://img.shields.io/nuget/dt/Material.Avalonia?color=blue&label=Downloads&style=flat-square)][Nuget.Material.Avalonia]

# ![][icon] Getting started

Check out the [getting started](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Getting-started) wiki page.
Or follow these instructions:

1. Add [Material.Avalonia][Nuget.Material.Avalonia] nuget package to your project:
    ```shell
    dotnet add package Material.Avalonia
    ```
   [![Nuget.Material.Avalonia](https://img.shields.io/nuget/vpre/Material.Avalonia?label=Material.Avalonia&style=flat-square)][Nuget.Material.Avalonia]
   [![Nuget.Material.Avalonia](https://img.shields.io/nuget/dt/Material.Avalonia?color=blue&label=Downloads&style=flat-square)][Nuget.Material.Avalonia]
2. Edit `App.xaml` file:
   ```xaml
   <Application ...
     xmlns:themes="clr-namespace:Material.Styles.Themes;assembly=Material.Styles"
     ...>
     <Application.Styles>
       <themes:MaterialTheme BaseTheme="Dark" PrimaryColor="Purple" SecondaryColor="Lime" />
     </Application.Styles>
   </Application>
   ```
3. Installing additional packages:
   > All styles will be included automatically if you using `MaterialTheme`

    - If you want to use `DataGrid` control, add [Material.Avalonia.DataGrid][Nuget.Material.Avalonia.DataGrid] package:
       ```shell
       dotnet add package Material.Avalonia.DataGrid
       ```
      [![Nuget.Material.Avalonia.DataGrid](https://img.shields.io/nuget/vpre/Material.Avalonia.DataGrid?label=Material.Avalonia.DataGrid&style=flat-square)][Nuget.Material.Avalonia.DataGrid]
      [![Nuget.Material.Avalonia.DataGrid](https://img.shields.io/nuget/dt/Material.Avalonia.DataGrid?color=blue&label=Downloads&style=flat-square)][Nuget.Material.Avalonia.DataGrid]

    - If you want to use dialogs provided from Material.Avalonia,
      add [Material.Avalonia.Dialogs][Nuget.Material.Avalonia.Dialogs] package:
       ```shell
       dotnet add package Material.Avalonia.Dialogs
       ```
      [![Nuget.Material.Avalonia.Dialogs](https://img.shields.io/nuget/vpre/Material.Avalonia.Dialogs?label=Material.Avalonia.Dialogs&style=flat-square)][Nuget.Material.Avalonia.Dialogs]
      [![Nuget.Material.Avalonia.Dialogs](https://img.shields.io/nuget/dt/Material.Avalonia.Dialogs?color=blue&label=Downloads&style=flat-square)][Nuget.Material.Avalonia.Dialogs]

4. Done!
   Now your app is styled!
   You can use the Demo app to view how different styles look.  
   Every control has the ShowMeTheXaml button in the right bottom corner
   which will show you a markup needed to recreate it.

# ![][icon] Useful links

- [Advanced theming](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming) wiki page
- [Nightly packages](https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Using-nightly-build-feed) wiki page
- [Material Design Icons](https://github.com/SKProCH/Material.Icons) icon pack support
- [DialogHost.Avalonia](https://github.com/AvaloniaUtils/DialogHost.Avalonia) that provides a simple way to display a dialog
