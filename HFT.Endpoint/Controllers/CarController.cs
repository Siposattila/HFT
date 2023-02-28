using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HFT.Logic.Interfaces;
using HFT.Models;

[Route("/api/car")]
[ApiController]
public class CarController : ControllerBase
{
    ICarLogic logic;
    public CarController(ICarLogic logic)
    {
        this.logic = logic;
    }

    [HttpGet]
    public IEnumerable<Car> ReadAll()
    {
        return this.logic.ReadAll();
    }

    [HttpGet("{id}")]
    public Car Read(int id)
    {
        return this.logic.Read(id);
    }

    [HttpPost]
    public void Create([FromBody] Car car)
    {
        this.logic.Create(car);
    }

    [HttpPut]
    public void Put([FromBody] Car car)
    {
        this.logic.Update(car);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        this.logic.Delete(id);
    }

    [HttpGet]
    [Route("avgprice")]
    public double AVGPrice()
    {
        return this.logic.AVGPrice();
    }

    [HttpGet]
    [Route("avgpricebybrands")]
    public IEnumerable<KeyValuePair<string, double>> AVGPriceByBrands()
    {
        return this.logic.AVGPriceByBrands();
    }

    [HttpGet]
    [Route("avgpricebyowners")]
    public IEnumerable<KeyValuePair<string, double>> AVGPriceByOwners()
    {
        return this.logic.AVGPriceByOwners();
    }

    [HttpGet]
    [Route("ownersbybrandname/{name}")]
    public IEnumerable<string> OwnersByBrandName(string name)
    {
        return this.logic.OwnersByBrandName(name);
    }

    [HttpGet]
    [Route("getownerswithcars")]
    public IEnumerable<KeyValuePair<string, string>> GetOwnersWithCars()
    {
        return this.logic.GetOwnersWithCars();
    }

    [HttpGet]
    [Route("getcarsbybrands")]
    public IEnumerable<KeyValuePair<string, string>> GetCarsByBrands()
    {
        return this.logic.GetCarsByBrands();
    }
}
