# material.avalonia

[![nuget](https://img.shields.io/badge/material-nuget-%2303A9F4)](https://www.nuget.org/packages/Material.Avalonia/)

This repository is a collection of styles to help you customize your [Avalonia](https://github.com/AvaloniaUI/Avalonia) application theme with Material Design.

![](Images/DeepPurple.gif)

![](Images/Dark.gif)

A collection of styles can be installed via [nuget-package](https://www.nuget.org/packages/Material.Avalonia/) and then include them to the required scope. See the example of `Application.Styles` collections:

```xaml
<Application.Styles>        
    <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.Templates.xaml" />
    <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.{Palette}.xaml" />
</Application.Styles>
```

Now we have next pallets:
- LightGreen
- DeepPurple
- Dark


**Powered by**

<img width="400" alt="portfolio_view" src="https://github.com/CreateLab/MessageBox.Avalonia/blob/master/Images/jetbrains-variant-4.png">
