namespace exercise.wwwapi.Configurations;

public class ConfigurationSettings
{
    public readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    public string GetValue(string key)
    {
        return Configuration.GetValue<string>(key)!;
    }
}