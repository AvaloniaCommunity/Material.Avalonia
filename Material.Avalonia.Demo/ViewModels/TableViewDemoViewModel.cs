using System.Collections.ObjectModel;

namespace Material.Avalonia.Demo.ViewModels;

public sealed class TableViewDemoViewModel : ViewModelBase {
    public ObservableCollection<Country> Countries { get; } = [
        new("Afghanistan", "ASIA (EX. NEAR EAST)", 31056997, 647500, 700),
        new("Albania", "EASTERN EUROPE", 3581655, 28748, 4500),
        new("Algeria", "NORTHERN AFRICA", 32930091, 2381740, 6000),
        new("Australia", "OCEANIA", 20264082, 7686850, 29000),
        new("Austria", "WESTERN EUROPE", 8192880, 83870, 30000),
        new("Brazil", "LATIN AMER. & CARIB", 188078227, 8511965, 7600),
        new("Canada", "NORTHERN AMERICA", 33098932, 9984670, 29800),
        new("China", "ASIA (EX. NEAR EAST)", 1313973713, 9596960, 5000),
        new("France", "WESTERN EUROPE", 60876136, 547030, 27600),
        new("Germany", "WESTERN EUROPE", 82422299, 357021, 27600),
        new("India", "ASIA (EX. NEAR EAST)", 1095351995, 3287590, 2900),
        new("Japan", "ASIA (EX. NEAR EAST)", 127463611, 377835, 28200),
        new("New Zealand", "OCEANIA", 4076140, 268680, 21600),
        new("Norway", "WESTERN EUROPE", 4610820, 323802, 37800),
        new("Poland", "EASTERN EUROPE", 38536869, 312685, 11100),
        new("South Africa", "SUB-SAHARAN AFRICA", 44187637, 1219912, 10700),
        new("United Kingdom", "WESTERN EUROPE", 60609153, 244820, 27700),
        new("United States", "NORTHERN AMERICA", 298444215, 9631420, 37800)
    ];

    public sealed record Country(string Name, string Region, int Population, int Area, int Gdp);
}