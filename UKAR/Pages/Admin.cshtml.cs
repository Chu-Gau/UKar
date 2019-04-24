using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UKAR.Pages
{
    public class AdminModel : PageModel
    {
        [ViewData]
        public string Title { get; set; }
        public void OnGet()
        {

        }
    }
}