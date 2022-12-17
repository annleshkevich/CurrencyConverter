using AutoMapper;
using CurrencyConverter.BusinessLogic.Services.Implementations;
using CurrencyConverter.BusinessLogic.Services.Interfaces;
using CurrencyConverter.Common.Mapper;
using CurrencyConverter.Model;
using CurrencyConverter.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CurrencyConverterContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<ICurrencyService, CurrencyService>();
builder.Services.AddTransient<IExchangeRateService, ExchangeRateService>();

var optionsBuilder = new DbContextOptionsBuilder<CurrencyConverterContext>();
var options = optionsBuilder.UseSqlServer(connection).Options;
builder.Services.AddControllers();

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
    db.SaveChanges();

}

var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}");
});


app.UseHttpsRedirection();
app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();

app.Run();