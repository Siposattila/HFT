using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HFT.Logic.Interfaces;
using HFT.Models;

[Route("/api/brand")]
[ApiController]
public class BrandController : ControllerBase
{
    IBrandLogic logic;
    public BrandController(IBrandLogic logic)
    {
        this.logic = logic;
    }

    [HttpGet]
    public IEnumerable<Brand> ReadAll()
    {
        return this.logic.ReadAll();
    }

    [HttpGet("{id}")]
    public Brand Read(int id)
    {
        return this.logic.Read(id);
    }

    [HttpPost]
    public void Create([FromBody] Brand brand)
    {
        this.logic.Create(brand);
    }

    [HttpPut]
    public void Put([FromBody] Brand brand)
    {
        this.logic.Update(brand);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        this.logic.Delete(id);
    }
}
