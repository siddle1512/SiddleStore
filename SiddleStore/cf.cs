namespace SiddleStore
{
    public class cf
    {
        public static IConfiguration AppSetting { get; }
        static cf()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
