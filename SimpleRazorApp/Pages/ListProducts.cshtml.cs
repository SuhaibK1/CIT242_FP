using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SimpleRazorApp.Services;
using SimpleRazorApp.Models;

public class ListProductsModel : PageModel
{
    private readonly ProductManager _productManager;
    public ListProductsModel(ProductManager productManager)
    {
        _productManager = productManager;
    }
    
    public IEnumerable<Product> Products { get; private set; }
    public async Task OnGetAsync()
    {
        Products = await _productManager.GetProductList();
    }
} 