namespace BlogRest.Services;

public interface ISettingsService
{
    public string GetSettingByName(string name);
}