using System;
using System.Collections.Generic;
using ConsoleTools;
using HFT.Models;

namespace HFT.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:5000");

            var car = new ConsoleMenu(args, level: 1)
                .Add("ReadAll", () => {
                    List<Car> cars = rest.Get<Car>("/api/car");
                    foreach (Car car in cars)
                    {
                        string ownerName = (car.Owner == null)?"nincs":car.Owner.Name;
                        Console.WriteLine($"Márka: {car.Brand.Name}\nTípus: {car.Model}\nÁra: {car.BasePrice}\nTulaj: {ownerName}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("Read", () => {
                    Console.Write("Kérem adja meg az autó azonosítóját: ");
                    Car car = rest.Get<Car>(int.Parse(Console.ReadLine()), "/api/car");
                    string ownerName = (car.Owner == null)?"nincs":car.Owner.Name;
                    Console.WriteLine($"Márka: {car.Brand.Name}\nTípus: {car.Model}\nÁra: {car.BasePrice}\nTulaj: {ownerName}");
                    Console.ReadKey();
                })
                .Add("Create", () => {
                    Console.Write("Kérem adja meg az autó modeljét: ");
                    string model = Console.ReadLine();
                    Console.Write("Kérem adja meg az autó árát: ");
                    int price = int.Parse(Console.ReadLine());
                    Console.Write("Kérem adja meg az autó márka azonosítóját: ");
                    int brand = int.Parse(Console.ReadLine());

                    Car car = new Car()
                    {
                        Model = model,
                        BasePrice = price,
                        BrandId = brand
                    };
                    rest.Post<Car>(car, "/api/car");
                })
                .Add("Update", () => {
                    Console.Write("Kérem adja meg az autó azonosítóját: ");
                    Car car = rest.Get<Car>(int.Parse(Console.ReadLine()), "/api/car");
                    Console.Write($"Jelenlegi érték: {car.Model}\nKérem adja meg az autó modeljét vagy hagyja üresen: ");
                    string model = Console.ReadLine();
                    car.Model = (model == string.Empty)?car.Model:model;
                    Console.Write($"Jelenlegi érték: {car.BasePrice}\nKérem adja meg az autó árát vagy hagyja üresen: ");
                    string priceString = Console.ReadLine();
                    int price = (priceString == string.Empty)?0:int.Parse(priceString);
                    car.BasePrice = (price == 0)?car.BasePrice:price;
                    Console.Write($"Jelenlegi érték: {car.BrandId}\nKérem adja meg az autó márka azonosítóját vagy hagyja üresen: ");
                    string brandString = Console.ReadLine();
                    int brand = (brandString == string.Empty)?0:int.Parse(brandString);;
                    car.BrandId = (brand == 0)?car.BrandId:brand;

                    rest.Put<Car>(car, "/api/car");
                })
                .Add("Delete", () => {
                    Console.Write("Kérem adja meg az autó azonosítóját: ");
                    try
                    {                        
                        rest.Delete(int.Parse(Console.ReadLine()), "/api/car");
                        Console.WriteLine("Az autó sikeresen törölve!");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Hiba lépett fel az autó törlése közben!");
                    }
                    Console.ReadKey();
                })
                .Add("AVGPrice", () => {
                    double avgprice = rest.GetSingle<double>("/api/car/avgprice");
                    Console.WriteLine($"Átlag ár: {avgprice}");
                    Console.ReadKey();
                })
                .Add("AVGPriceByBrands", () => {
                    List<KeyValuePair<string, double>> avgprices = rest.Get<KeyValuePair<string, double>>("/api/car/avgpricebybrands");
                    foreach (KeyValuePair<string, double> avg in avgprices)
                    {
                        Console.WriteLine($"Márka: {avg.Key}\nÁtlag ár: {avg.Value}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("AVGPriceByOwners", () => {
                    List<KeyValuePair<string, double>> avgprices = rest.Get<KeyValuePair<string, double>>("/api/car/avgpricebyowners");
                    foreach (KeyValuePair<string, double> avg in avgprices)
                    {
                        Console.WriteLine($"Tulajdonos: {avg.Key}\nÁtlag ár: {avg.Value}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("OwnersByBrandName", () => {
                    Console.Write("Kérem adja meg a márka nevét: ");
                    List<string> owners = rest.Get<string>("/api/car/ownersbybrandname/"+Console.ReadLine());
                    foreach (string owner in owners)
                    {
                        Console.WriteLine($"{owner}");
                    }
                    Console.ReadKey();
                })
                .Add("GetOwnersWithCars", () => {
                    List<KeyValuePair<string, string>> owners = rest.Get<KeyValuePair<string, string>>("/api/car/getownerswithcars");
                    foreach (KeyValuePair<string, string> ownersCars in owners)
                    {
                        Console.WriteLine($"Tulajdonos: {ownersCars.Key}");
                        Console.WriteLine("------");
                        Console.WriteLine($"{ownersCars.Value}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("GetCarsByBrands", () => {
                    List<KeyValuePair<string, string>> brands = rest.Get<KeyValuePair<string, string>>("/api/car/getcarsbybrands");
                    foreach (KeyValuePair<string, string> brandsCars in brands)
                    {
                        Console.WriteLine($"Márka: {brandsCars.Key}");
                        Console.WriteLine("------");
                        Console.WriteLine($"{brandsCars.Value}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("Back", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "=> ";
                    config.Title = "Car";
                });

            var brand = new ConsoleMenu(args, level: 1)
                .Add("ReadAll", () => {
                    List<Brand> brands = rest.Get<Brand>("/api/brand");
                    foreach (Brand brand in brands)
                    {
                        Console.WriteLine($"Azonosító: {brand.Id}\nMárka: {brand.Name}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("Read", () => {
                    Console.Write("Kérem adja meg a márka azonosítóját: ");
                    Brand brand = rest.Get<Brand>(int.Parse(Console.ReadLine()), "/api/brand");
                    Console.WriteLine(brand.Name); // TODO: megjeleníteni a márkához tartozó kocsikat
                    Console.ReadKey();
                })
                .Add("Create", () => {
                    Console.Write("Kérem adja meg az márka nevét: ");
                    string name = Console.ReadLine();

                    Brand brand = new Brand()
                    {
                        Name = name
                    };
                    rest.Post<Brand>(brand, "/api/brand");
                })
                .Add("Update", () => {
                    Console.Write("Kérem adja meg a márka azonosítóját: ");
                    Brand brand = rest.Get<Brand>(int.Parse(Console.ReadLine()), "/api/brand");
                    Console.Write($"Jelenlegi érték: {brand.Name}\nKérem adja meg a márka nevét vagy hagyja üresen: ");
                    string name = Console.ReadLine();
                    brand.Name = (name == string.Empty)?brand.Name:name;

                    rest.Put<Brand>(brand, "/api/brand");
                })
                .Add("Delete", () => {
                    Console.Write("Kérem adja meg a márka azonosítóját: ");
                    try
                    {                        
                        rest.Delete(int.Parse(Console.ReadLine()), "/api/brand");
                        Console.WriteLine("A márka sikeresen törölve!");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Hiba lépett fel a márka törlése közben!");
                    }
                    Console.ReadKey();
                })
                .Add("Back", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "=> ";
                    config.Title = "Brand";
                });
            
            var owner = new ConsoleMenu(args, level: 1)
                .Add("ReadAll", () => {
                    List<Owner> owners = rest.Get<Owner>("/api/owner");
                    foreach (Owner owner in owners)
                    {
                        Console.WriteLine($"Azonosító: {owner.Id}\nNév: {owner.Name}");
                        Console.WriteLine("------------------------");
                    }
                    Console.ReadKey();
                })
                .Add("Read", () => {
                    Console.Write("Kérem adja meg a tulajdonos azonosítóját: ");
                    Owner owner = rest.Get<Owner>(int.Parse(Console.ReadLine()), "/api/owner");
                    Console.WriteLine(owner.Name); // TODO: megjeleníteni a tulajhoz tartozó kocsikat
                    Console.ReadKey();
                })
                .Add("Create", () => {
                    Console.Write("Kérem adja meg a tulajdonos nevét: ");
                    string name = Console.ReadLine();

                    Owner owner = new Owner()
                    {
                        Name = name
                    };
                    rest.Post<Owner>(owner, "/api/owner");
                })
                .Add("Update", () => {
                    Console.Write("Kérem adja meg a tulajdonos azonosítóját: ");
                    Owner owner = rest.Get<Owner>(int.Parse(Console.ReadLine()), "/api/owner");
                    Console.Write($"Jelenlegi érték: {owner.Name}\nKérem adja meg a tulajdonos nevét vagy hagyja üresen: ");
                    string name = Console.ReadLine();
                    owner.Name = (name == string.Empty)?owner.Name:name;

                    rest.Put<Owner>(owner, "/api/owner");
                })
                .Add("Delete", () => {
                    Console.Write("Kérem adja meg a tulajdonos azonosítóját: ");
                    try
                    {                        
                        rest.Delete(int.Parse(Console.ReadLine()), "/api/owner");
                        Console.WriteLine("A tulajdonos sikeresen törölve!");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Hiba lépett fel a tulajdonos törlése közben!");
                    }
                    Console.ReadKey();
                })
                .Add("Back", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "=> ";
                    config.Title = "Owner";
                });
                
            var main = new ConsoleMenu(args, level: 0)
                .Add("Car", car.Show)
                .Add("Brand", brand.Show)
                .Add("Owner", owner.Show)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "=> ";
                    config.Title = "Main menu";
                });

            main.Show();
        }
    }
}
