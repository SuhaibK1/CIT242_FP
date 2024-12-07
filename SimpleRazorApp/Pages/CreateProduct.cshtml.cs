using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleRazorApp.Services;
using SimpleRazorApp.Models;

public class CreateProductModel : PageModel
{
    private readonly ProductManager _productManager;

    [BindProperty]
    public Product Product { get; set; }

    public CreateProductModel(ProductManager productManager)
    {
        _productManager = productManager;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
    
        await _productManager.SaveProductInfo(Product);
        return RedirectToPage("./ListProducts");
    }
}
