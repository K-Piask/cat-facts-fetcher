using CatFactFetcher.ConsoleApp.Interfaces;
using CatFactFetcher.ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace CatFactFetcher.ConsoleApp.Services
{
    public class CatFactService:ICatFactService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatFactService> _logger;
        private const string ApiUrl = "https://catfact.ninja/fact";
        public CatFactService(HttpClient httpClient, ILogger<CatFactService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string?> GetRandomCatFactAsync()
        {
            try
            {
                _logger.LogInformation("Łączenie z API catfact.ninja...");
                var response = await _httpClient.GetFromJsonAsync<CatFactResponse>(ApiUrl);
                return response?.Fact;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas pobierania danych z API catfact.ninja.");
                return null;
            }
        }
    }
}
