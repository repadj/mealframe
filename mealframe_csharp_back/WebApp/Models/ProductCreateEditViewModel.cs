using App.Domain;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ProductCreateEditViewModel
{
    public Product Product { get; set; } = default!;
    public SelectList CategorySelectList { get; set; } = default!;
}