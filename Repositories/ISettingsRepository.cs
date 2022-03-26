namespace BlogRest.Repositories;

public interface ISettingsRepository
{
    public string GetSettingByName(string name);
}