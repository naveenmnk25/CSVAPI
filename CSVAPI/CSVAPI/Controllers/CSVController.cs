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
    public class CSVController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CSVDbContext _context;

        public int MyProperty { get; set; }
        public CSVController(ILogger<WeatherForecastController> logger, CSVDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("UpdateMigrationFile")]
        public async Task<string> UpdateCSVFile(IFormFile file)
        {
            var files = Request.Form.Files;
            var temp = file;
            try
            {
                if (file == null || file.Length == 0)
                    return ("No file uploaded.");
                List<Dictionary<string, string>> rows = ReadCSVFile(file);


                // Generate JSON output
                var jsonOutput = GenerateJSONOutput(rows);

                string jsonText = JsonConvert.SerializeObject(jsonOutput, Formatting.Indented);


                return jsonText;
            }
            catch (Exception ex)
            {
                return ($"An error occurred: {ex.Message}");
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

        static object GenerateJSONOutput(List<Dictionary<string, string>> filteredRows)
        {
            var groupedOutput = filteredRows
                .SelectMany(row => row)
                .GroupBy(kvp => kvp.Key)
                .Select(group => new
                {
                    Column = group.Key,
                    ColumnValues = group.Select(kvp => new { Value = kvp.Value }).ToList()
                })
                .ToList();

            return groupedOutput;
        }

      
    }
}