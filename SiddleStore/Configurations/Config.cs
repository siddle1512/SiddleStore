namespace SiddleStore.Configurations
{
    public class config
    {
        public static IConfiguration AppSetting { get; }
        static config()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
