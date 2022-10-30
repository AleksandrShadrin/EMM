using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using DS.Presentation;
using DS.Presentation.Services;
using DS.SeriesAnalysis.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazorise(opt => { opt.Immediate = true; })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
builder.Services.AddTransient<ISeriesAnalyzerService, SeriesAnalyzerService>();
builder.Services.AddSingleton<ISeriesGenerator>(_ =>
{
    return new SeriesGeneratorWithNoise(new Random(123));
});

builder.Services.AddSingleton<ISeriesSmoothingService, SeriesSmoothingService>();

await builder.Build().RunAsync();
