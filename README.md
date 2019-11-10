# material.avalonia



[![nuget](https://img.shields.io/badge/material-nuget-%2303A9F4)](https://www.nuget.org/packages/Material.Avalonia/)

![](Images/DeepPurple.gif) ![](Images/Dark.gif)

Now we have 2 palets: DeepePurple Light and Dark
To inslall them u should dounload nuget package and include styles in needed scope
Here u cans see an exapmle with Application style scope
```xml
  <Application.Styles>
        <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.DeepPurple.xaml" />
        <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.Templates.xaml" />
    </Application.Styles
```
Or
```xml
  <Application.Styles>
        <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.Dark.xaml" />
        <StyleInclude Source="avares://Material.Avalonia/Material.Avalonia.Templates.xaml" />
    </Application.Styles
```
