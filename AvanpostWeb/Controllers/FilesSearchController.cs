using Microsoft.AspNetCore.Mvc;

namespace AvanpostWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesSearchController : ControllerBase
    {
        private readonly ILogger<FilesSearchController> _logger;
        private readonly string _examplesPath = "examples";

        public FilesSearchController(ILogger<FilesSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet("search")]
        public IActionResult Search(string word)
        {
            try
            {
                string[] fileNames = Directory.GetFiles(_examplesPath);

                var filesWithKeyword = fileNames.Where(fileName =>
                    System.IO.File.ReadAllText(fileName).Contains(word, StringComparison.OrdinalIgnoreCase));

                var foundFileNames = filesWithKeyword.Select(filePath => Path.GetFileName(filePath));

                return new JsonResult(foundFileNames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении поиска.");
                return StatusCode(500, "Произошла ошибка при выполнении поиска.");
            }
        }
    }
}
