using Microsoft.Extensions.Configuration;

namespace Gotorz14.Services
{
    public static class ReadJsonFile
    {
        //readonly string Filepath;
        //public ReadJsonFile()
        //{
        //    //var builder = new ConfigurationBuilder()
        //    //.SetBasePath()
        //    //.AddJsonFile("cheapestPerDay.json", optional: false, reloadOnChange: true);

        //    //Configuration = builder.Build();

        //}
        public static string StringFromJsonFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Services/cheapestPerDay.json");
            string json = File.ReadAllText(path);
            return json;
        }



    }
}
