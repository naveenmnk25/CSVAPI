using CSVAPI.Dto;
using CSVAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;

namespace CSVAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CSVOneController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CSVDbContext _context;

        public int MyProperty { get; set; }
        public CSVOneController(ILogger<WeatherForecastController> logger,  CSVDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("UpdateMigrationFile")]
        public async Task<IActionResult> UpdateCSVFile(IFormFile file)
        {
            var files = Request.Form.Files;
            var temp = file;
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                List<Dictionary<string, string>> rows = ReadCSVFile(file);

                string name = "a1"; // Replace with the target column name
                string targetValue = "some_value"; // Replace with the desired target value

                // Convert the filtered rows to JSON
                string jsonText = JsonConvert.SerializeObject(rows, Formatting.Indented);

                return Ok(jsonText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        static List<Dictionary<string, string>> ReadCSVFile(IFormFile file)
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            string[] columnNames = null;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    if (columnNames == null)
                    {
                        columnNames = values;
                    }
                    else
                    {
                        var row = new Dictionary<string, string>();
                        for (int i = 0; i < columnNames.Length && i < values.Length; i++)
                        {
                            row[columnNames[i]] = values[i];
                        }
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        static List<Dictionary<string, string>> FilterRowsByColumnValue(List<Dictionary<string, string>> rows, string columnName, string targetValue)
        {
            return rows.FindAll(row => row.ContainsKey(columnName) && row[columnName] == targetValue);
        }

    }
}