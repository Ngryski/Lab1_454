using Lab1_454.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1_454.Pages
{
    
    public class AdminIndexModel : PageModel
    {


        private readonly ILogger<AdminIndexModel> _logger;

        public AdminIndexModel(ILogger<AdminIndexModel> logger)
        {
            _logger = (ILogger<AdminIndexModel>?)logger;
        }

        public void OnGet()
        {

        }
    }
}