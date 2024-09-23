using BlazorATPRankings.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<PlayerService>();
builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("https://localhost:7030/") });

await builder.Build().RunAsync();
