using System.Collections;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CourseView;
using MudBlazor.Services;
using Newtonsoft.Json;

namespace CourseView
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });
            
            builder.Services.AddMudServices(); 
           
            //Console.WriteLine($"Payload: {JsonConvert.DeserializeObject<Hashtable>(new HttpClient().GetAsync("http://localhost:5157/course").Result.Content.ReadAsStringAsync().Result)["Payload"]}");

            CourseManager.LoadConfig();
            
            await builder.Build().RunAsync();
        }
    }
}