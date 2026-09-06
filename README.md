# Cat Facts Fetcher - Zadanie Rekrutacyjne Netwise

Nowoczesna aplikacja konsolowa w .NET 10 pobierająca losowe ciekawostki o kotach z zewnętrznego API i zapisująca je w lokalnym pliku. 
Projekt został zrealizowany w ramach zadania rekrutacyjnego do Szkółki .NET w firmie Netwise.

## Technologie i Biblioteki
* **.NET 10.0** - Najnowsza wersja platformy LTS.
* **Dependency Injection (Microsoft.Extensions.Hosting)** - Zarządzanie cyklem życia serwisów.
* **IHttpClientFactory (Microsoft.Extensions.Http)** - Bezpieczne i wydajne zarządzanie połączeniami HTTP.
* **Spectre.Console** - Bogaty i responsywny interfejs użytkownika w terminalu.
* **xUnit & Moq** - Zestaw do testów jednostkowych (izolacja logiki i mockowanie HTTP).

## Architektura (Pragmatic Clean Design)
Aplikacja została zaprojektowana z zachowaniem zasad **SOLID** oraz podziału odpowiedzialności. Kod został podzielony na logiczne warstwy oparte na kontraktach (interfejsach):

* **`ICatFactService`** - Odpowiada wyłącznie za asynchroniczną komunikację z zewnętrznym API.
* **`IFileStorageService`** - Odpowiada wyłącznie za operacje I/O (zapis do pliku).
* **Modele Danych (`CatFactResponse`)** - Mapowanie odpowiedzi JSON.

## Jak uruchomić projekt?
1. Otwórz terminal w głównym folderze rozwiązania.
2. Przywróć pakiety NuGet poleceniem:
```bash
dotnet restore
```
3. Uruchom aplikację komendą:
```bash
dotnet run --project CatFactFetcher.ConsoleApp
```
*Uwaga: Pobrane ciekawostki są automatycznie dopisywane do pliku `cat_facts.txt` w folderze wykonawczym aplikacji (dokładna ścieżka wyświetli się w konsoli).*

## Testy
Projekt zawiera testy jednostkowe (xUnit) oraz integracyjne. Logika API została przetestowana w całkowitej izolacji od warstwy sieciowej przy użyciu biblioteki Moq.

Aby uruchomić testy, wpisz w terminalu:
```bash
dotnet test
```