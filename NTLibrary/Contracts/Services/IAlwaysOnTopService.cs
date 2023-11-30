using NTLibrary.Services;

namespace NTLibrary.Contracts.Services;

public interface IAlwaysOnTopService
{
    AlwaysOnTopService.AlwaysOnTop IsAlwaysOnTop
    {
        get;
    }
    Task SetAlwaysOnTopAsync(AlwaysOnTopService.AlwaysOnTop value);
    Task SetRequestedAlwaysOnTopAsync();
    event Action AlwaysOnTopChanged;
    Task InitializeAsync();

}