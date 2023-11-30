using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using NTLibrary.Contracts.Services;
using NTLibrary.Helpers;

using Windows.ApplicationModel;
using NTLibrary.Services;
using static NTLibrary.Services.AlwaysOnTopService;

namespace NTLibrary.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ILocalSettingsService _localSettingsService;
    private readonly IBackdropSelectorService _backdropSelectorService;
    private readonly IAlwaysOnTopService _alwaysOnTopService;

    public BackdropSelectorService.BackdropType Mica => BackdropSelectorService.BackdropType.Mica;
    public BackdropSelectorService.BackdropType MicaAlt => BackdropSelectorService.BackdropType.MicaAlt;
    public BackdropSelectorService.BackdropType DesktopAcrylic => BackdropSelectorService.BackdropType.DesktopAcrylic;

    public AlwaysOnTop AlwaysOnTopEnabled => AlwaysOnTop.Enabled;
    public AlwaysOnTop AlwaysOnTopDisabled => AlwaysOnTop.Disabled;

    [ObservableProperty] private ElementTheme _elementTheme;
    [ObservableProperty] private BackdropSelectorService.BackdropType _backDrop;
    [ObservableProperty] private AlwaysOnTop _alwaysOnTop;
    [ObservableProperty] private string _versionDescription;


    public ICommand SwitchThemeCommand
    {
        get;
    }
    public ICommand SwitchBackdropCommand
    {
        get;
    }
    public ICommand SwitchAlwaysOnTopCommand
    {
        get;
    }

    public string CurrentTheme => _themeSelectorService.Theme.ToString();
    public string CurrentBackdrop => _backdropSelectorService.Backdrop.ToString();
    public string CurrentAlwaysOnTop => _alwaysOnTopService.IsAlwaysOnTop.ToString();

    public SettingsViewModel(IThemeSelectorService themeSelectorService, ILocalSettingsService localSettingsService,
        IBackdropSelectorService backdropSelectorService, IAlwaysOnTopService alwaysOnTopService)
    {
        _themeSelectorService = themeSelectorService;
        _backdropSelectorService = backdropSelectorService;
        _alwaysOnTopService = alwaysOnTopService;
        _localSettingsService = localSettingsService;

        _backDrop = _backdropSelectorService.Backdrop;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();

        _themeSelectorService.ThemeChanged += () => OnPropertyChanged(nameof(CurrentTheme));
        _themeSelectorService.ThemeChanged += () => OnPropertyChanged(nameof(ThemeGlyph));
        _backdropSelectorService.BackdropChanged += () => OnPropertyChanged(nameof(CurrentBackdrop));
        _alwaysOnTopService.AlwaysOnTopChanged += () => OnPropertyChanged(nameof(CurrentAlwaysOnTop));

        SwitchThemeCommand = new RelayCommand<ElementTheme>(ChangeTheme);
        SwitchBackdropCommand = new RelayCommand<BackdropSelectorService.BackdropType>(ChangeBackdrop);
        SwitchAlwaysOnTopCommand = new RelayCommand<AlwaysOnTop>(ChangeAlwaysOnTop);
    }

    private async void ChangeTheme(ElementTheme theme)
    {
        if (ElementTheme == theme)
        {
            return;
        }

        ElementTheme = theme;
        await _themeSelectorService.SetThemeAsync(theme);
    }
    public string ThemeGlyph =>
        ElementTheme switch
        {
            ElementTheme.Light => "\uE793",
            ElementTheme.Dark => "\uE708",
            ElementTheme.Default => "\uE713",
            _ => "\uE713"
        };
    private async void ChangeBackdrop(BackdropSelectorService.BackdropType backdrop)
    {
        if (BackDrop == backdrop)
        {
            return;
        }

        await _backdropSelectorService.SetBackdropAsync(backdrop);
        BackDrop = backdrop;
    }

    private async void ChangeAlwaysOnTop(AlwaysOnTop param)
    {
        if (AlwaysOnTop == param)
        {
            return;
        }
        await _alwaysOnTopService.SetAlwaysOnTopAsync(param);
        AlwaysOnTop = param;
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
