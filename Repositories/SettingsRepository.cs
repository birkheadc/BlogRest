using BlogRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly IConfiguration configuration;
    public SettingsRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GetSettingByName(string name)
    {
        return configuration.GetSection("Settings").GetValue<string>(name);
    }
}