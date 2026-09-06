using CatFactFetcher.ConsoleApp.Interfaces;
using CatFactFetcher.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CatFactFetcher.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.AddLogging(builder => builder.ClearProviders());
                services.AddTransient <IFileStorageService, FileStorageService>();
                services.AddHttpClient<ICatFactService, CatFactService>();
            }).Build();

            var catFactService = host.Services.GetRequiredService<ICatFactService>();
            var fileStorageService = host.Services.GetRequiredService<IFileStorageService>();

            AnsiConsole.Write(
                new FigletText("Cat Facts")
                .LeftJustified()
                .Color(Color.Blue));

            AnsiConsole.MarkupLine("[bold green]Witaj w aplikacji do pobierania ciekawostek o kotach![/]");
            AnsiConsole.MarkupLine("Aplikacja rekrutacyjna\n");

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Co chcesz zrobić?")
                    .AddChoices(new[] { "Pobierz ciekawostkę", "Wyjdź" }));
                if (choice == "Wyjdź")
                {
                    AnsiConsole.MarkupLine("[bold red]Do zobaczenia![/]");
                    break;
                }

                string? fact = null;

                await AnsiConsole.Status()
                    .StartAsync("Łączenie z serwerem i pobieranie danych...", async ctx =>
                    {
                        fact = await catFactService.GetRandomCatFactAsync();
                    });

                if (!String.IsNullOrEmpty(fact))
                {
                    var panel = new Panel(fact)
                    {
                        Border = BoxBorder.Rounded,
                        Padding = new Padding(1, 1, 1, 1)
                    };
                    panel.Header = new PanelHeader("[bold cyan]Nowy fakt o kotach[/]");
                    AnsiConsole.Write(panel);

                    await fileStorageService.AppendFactToFileAsync(fact);
                    string fullPath = System.IO.Path.GetFullPath("cat_facts.txt");
                    AnsiConsole.MarkupLine($"[green]Pomyślnie dopisano fakt do pliku:\n[bold]{fullPath}[/][/]\n");

                } else
                {
                    AnsiConsole.MarkupLine("[red]Nie udało się pobrać ciekawostki.[/]\n");
                }
            }
        }
    }
}
