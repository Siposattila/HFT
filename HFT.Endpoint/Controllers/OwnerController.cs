using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HFT.Logic.Interfaces;
using HFT.Models;

[Route("/api/owner")]
[ApiController]
public class OwnerController : ControllerBase
{
    IOwnerLogic logic;
    public OwnerController(IOwnerLogic logic)
    {
        this.logic = logic;
    }

    [HttpGet]
    public IEnumerable<Owner> ReadAll()
    {
        return this.logic.ReadAll();
    }

    [HttpGet("{id}")]
    public Owner Read(int id)
    {
        return this.logic.Read(id);
    }

    [HttpPost]
    public void Create([FromBody] Owner owner)
    {
        this.logic.Create(owner);
    }

    [HttpPut]
    public void Put([FromBody] Owner owner)
    {
        this.logic.Update(owner);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        this.logic.Delete(id);
    }
}
