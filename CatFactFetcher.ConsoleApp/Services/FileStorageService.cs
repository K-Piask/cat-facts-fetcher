using CatFactFetcher.ConsoleApp.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatFactFetcher.ConsoleApp.Services
{
    public class FileStorageService:IFileStorageService
    {
        private readonly ILogger<FileStorageService> _logger;
        private const string FilePath = "cat_facts.txt";

        public FileStorageService(ILogger<FileStorageService> logger)
        {
            _logger = logger;
        }
        public async Task AppendFactToFileAsync(string fact)
        {
            try
            {
                await File.AppendAllTextAsync(FilePath, fact + Environment.NewLine);
                _logger.LogInformation("Zapisano do pliku: {FilePath}", FilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił problem podczas zapisu do pliku.");
            }
        }
    }
}
