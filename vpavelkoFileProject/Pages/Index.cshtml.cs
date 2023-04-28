using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vpavelkoFileProject.models;

namespace vpavelkoFileProject.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<PasswordDetails> passwordDetailsList { get; set; }
        public int ValidPasswordNumber { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            passwordDetailsList = new List<PasswordDetails>();
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if(file is null)
                return Page();
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >=0)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            lines.ForEach(line =>
            {
                var details = new PasswordDetails(line);
                passwordDetailsList.Add(details);
            });

            ValidPasswordNumber = passwordDetailsList.Where(p => p.IsValidPassword).Count();

            return Page();
        }
    }
}