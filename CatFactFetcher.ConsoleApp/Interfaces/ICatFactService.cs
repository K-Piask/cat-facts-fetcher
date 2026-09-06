using System;
using System.Collections.Generic;
using System.Text;

namespace CatFactFetcher.ConsoleApp.Interfaces
{
    public interface ICatFactService
    {
        Task<string?> GetRandomCatFactAsync();
    }
}
