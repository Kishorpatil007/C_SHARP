
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private static List<Product> data = Enumerable.Range(1, 100).Select(x => new Product { Id = x, Name = "Item" + x, Price = x * 10 }).ToList();

    [HttpGet]
    public IActionResult Get(int page = 1, int pageSize = 10, string name = "")
    {
        var query = data.AsQueryable();
        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.Name.Contains(name));

        var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return Ok(result);
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}
