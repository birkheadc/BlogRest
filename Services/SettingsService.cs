using BlogRest.Repositories;

namespace BlogRest.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsRepository repository;

    public SettingsService(ISettingsRepository repository)
    {
        this.repository = repository;
    }

    public string GetSettingByName(string name)
    {
        return repository.GetSettingByName(name);
    }
}