using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleRazorApp.Services;
using SimpleRazorApp.Models;

public class EditProductModel : PageModel
{
    private readonly ProductManager _productManager;

    [BindProperty]
    public Product Product { get; set; }

    public List<Product> Products { get; set; }

    public EditProductModel(ProductManager productManager)
    {
        _productManager = productManager;
    }

    public async Task OnGetAsync()
    {
        // Fetch all products for the dropdown
        Products = await _productManager.GetProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Update product details
        await _productManager.UpdateProductInfo(Product);
        return RedirectToPage("./ListProducts");
    }
}