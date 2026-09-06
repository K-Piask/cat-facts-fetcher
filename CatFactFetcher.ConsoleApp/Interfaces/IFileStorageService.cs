using System;
using System.Collections.Generic;
using System.Text;

namespace CatFactFetcher.ConsoleApp.Interfaces
{
    public interface IFileStorageService
    {
        Task AppendFactToFileAsync(string fact);
    }
}
