using CurrencyConverter.BusinessLogic.Services.Implementations;
using CurrencyConverter.Common.DTOs;
using CurrencyConverter.Model;
using CurrencyConverter.Model.DatabaseModels;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.Entity;
using System.Drawing;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Program
{
    public static void Main(string[] args)
    {
        HtmlWeb web = new();
        HtmlDocument document = web.Load("https://myfin.by/bank/kursy_valjut_nbrb");

        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<CurrencyConverterContext>();
        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        using (CurrencyConverterContext db = new(options))
        {
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            Currency usd = new() { Name = "Доллар США", Code = "USD" };
            Currency eur = new() { Name = "Евро", Code = "EUR" };
            Currency byn = new() { Name = "Белорусский рубль", Code = "BYN" };
            Currency rub = new() { Name = "Российский рубль", Code = "RUB" };
            Currency uah = new() { Name = "Украинская гривна", Code = "UAH" };
            Currency pln = new() { Name = "Польский злотый", Code = "PLN" };
            Currency cny = new() { Name = "Китайский юань", Code = "CNY" };
            db.Currencies.AddRange(usd, eur, byn, rub, uah, pln, cny);


            ExchangeRate exchangeRateUSD = new()
            {
                RateAmount = double.Parse(RateAmount(document, 1)),
                Currency = usd,
                UnitAmount = double.Parse(UnitAmount(document, 1)),
                Period = DateTime.Now,
                CurrencyCode = "USD"
            };
            db.ExchangeRates.Add(exchangeRateUSD);

            ExchangeRate exchangeRateEUR = new()
            {
                RateAmount = double.Parse(RateAmount(document, 2)),
                Currency = eur,
                UnitAmount = double.Parse(UnitAmount(document, 2)),
                Period = DateTime.Now,
                CurrencyCode = "EUR"
            };
            db.ExchangeRates.Add(exchangeRateEUR);

            ExchangeRate exchangeRateRUB = new()
            {
                RateAmount = double.Parse(RateAmount(document, 3)),
                Currency = rub,
                UnitAmount = double.Parse(UnitAmount(document, 3)),
                Period = DateTime.Now, 
                CurrencyCode = "RUB"
            };
            db.ExchangeRates.Add(exchangeRateRUB);

            ExchangeRate exchangeRateUAH = new()
            {
                RateAmount = double.Parse(RateAmount(document, 4)),
                Currency = uah,
                UnitAmount = double.Parse(UnitAmount(document, 4)),
                Period = DateTime.Now,
                CurrencyCode = "UAH"
            };
            db.ExchangeRates.Add(exchangeRateUAH);

            ExchangeRate exchangeRatePLN = new()
            {
                RateAmount = double.Parse(RateAmount(document, 5)),
                Currency = pln,
                UnitAmount = double.Parse(UnitAmount(document, 5)),
                Period = DateTime.Now,
                CurrencyCode = "PLN"
            };
            db.ExchangeRates.Add(exchangeRatePLN);

            ExchangeRate exchangeRateCNY = new()
            {
                RateAmount = double.Parse(RateAmount(document, 7)),
                Currency = cny,
                UnitAmount = double.Parse(UnitAmount(document, 7)),
                Period = DateTime.Now, 
                CurrencyCode = "CNY"
            };
            db.ExchangeRates.Add(exchangeRateCNY);
            //db.SaveChanges();

            Console.WriteLine("save");
            double amountInBYN = 250;
            double amountInCNY = 860;
            Console.WriteLine("из бел в долл");
            Console.WriteLine(ConverterFromBYN(amountInBYN, exchangeRateUSD));

            double amountInUSD = 1200;
            Console.WriteLine("долл в кит юань");
            Console.WriteLine(ConverterFromBYN(ConverterInBYN(amountInUSD, exchangeRateUSD), exchangeRateCNY));

            Console.WriteLine("юань в кит польск");
            Console.WriteLine(ConverterFromBYN(ConverterInBYN(amountInCNY, exchangeRateCNY), exchangeRatePLN));

            Console.WriteLine($"Выберите валюту, которую хотите перевести:\nUSD, EUR, BYN, RUB, UAH, PLN, CNY ");
            string exchangeRateFirst = Console.ReadLine();
            Console.WriteLine("Введите сумму, которую хотите перевети");
            double amount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Выберите валюту, в которую хотите перевести:\nUSD, EUR, BYN, RUB, UAH, PLN, CNY ");
            string exchangeRateSecond = Console.ReadLine();

            ExchangeRate exchangeRateOne = new();
            ExchangeRate exchangeRateTwo = new();

            if(exchangeRateFirst != "BYN"&& exchangeRateSecond != "BYN")
            {
                for (int i = 0; i < db.ExchangeRates.Count(); i++)
                {
                    if (db.ExchangeRates.ToList()[i].CurrencyCode == exchangeRateFirst && db.ExchangeRates.ToList()[i].CurrencyCode != "BYN")
                    {
                        exchangeRateOne = db.ExchangeRates.ToList()[i];
                    }
                    if (db.ExchangeRates.ToList()[i].CurrencyCode == exchangeRateSecond)
                    {
                        exchangeRateTwo = db.ExchangeRates.ToList()[i];
                    }
                  
                }
                Console.WriteLine("Результат: " + ConverterFromBYN(ConverterInBYN(amount, exchangeRateOne), exchangeRateTwo));
            }
            if (exchangeRateFirst == "BYN"&& exchangeRateSecond != "BYN")
            {
                for (int i = 0; i < db.ExchangeRates.Count(); i++)
                {
                    if (db.ExchangeRates.ToList()[i].CurrencyCode == exchangeRateSecond)
                    {
                        exchangeRateTwo = db.ExchangeRates.ToList()[i];
                    }
                }
                Console.WriteLine("Результат: " + ConverterFromBYN(amount, exchangeRateTwo));
            }
            if(exchangeRateFirst != "BYN" && exchangeRateSecond == "BYN")
            {
                for (int i = 0; i < db.ExchangeRates.Count(); i++)
                {
                    if (db.ExchangeRates.ToList()[i].CurrencyCode == exchangeRateFirst && db.ExchangeRates.ToList()[i].CurrencyCode != "BYN")
                    {
                        exchangeRateOne = db.ExchangeRates.ToList()[i];
                    }
                }
                Console.WriteLine("Результат: " + ConverterInBYN(amount, exchangeRateOne));

            }

        }

        Console.ReadKey();
    }
    public static string ConverterFromBYN(double amountInBYR, ExchangeRate exchangeRate)
    {

        return (amountInBYR * double.Parse(exchangeRate.UnitAmount.ToString())
            / double.Parse(exchangeRate.RateAmount.ToString())).ToString("0.0000");
    }
    public static double ConverterInBYN(double amountInAnotherCurrency, ExchangeRate exchangeRate)
    {

        return (double)(amountInAnotherCurrency * (exchangeRate.RateAmount) / exchangeRate.UnitAmount);
    }
    public static string RateAmount(HtmlDocument document, int number)
    {
        return document.DocumentNode.SelectNodes("//*[@id=\"w0\"]/table/tbody/tr["+number+"]/td[2]").First().InnerText; 
    }
    public static string UnitAmount(HtmlDocument document, int number)
    {
        return document.DocumentNode.SelectNodes("//*[@id=\"w0\"]/table/tbody/tr["+number+"]/td[5]").First().InnerText;
    }
}