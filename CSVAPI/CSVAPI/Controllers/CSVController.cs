using CSVAPI.Dto;
using CSVAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
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
        public CSVController(ILogger<WeatherForecastController> logger,  CSVDbContext context)
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

                var entity = new Company
                {
                    Name = "Test",
                };
                _context.Companies.Add(entity);

                await _context.SaveChangesAsync();


                List<Dictionary<string, string[]>> rows = await ProcessCSVFileAsync(entity.Id,file);

                return Ok(rows.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        private async Task<List<Dictionary<string, string[]>>> ProcessCSVFileAsync(int companyid , IFormFile file)
        {
            List<Dictionary<string, string[]>> rows = new List<Dictionary<string, string[]>>();
            string[] columnNames = null;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    string[] values = line.Split(',');

                    if (columnNames == null)
                    {
                        // Assume the first row contains column names
                        columnNames = values;
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            var entity = new Column
                            {
                                Name = columnNames[i],
                                CompanyId=companyid,
                            };
                            _context.Columns.Add(entity);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        // Create a dictionary for the row data
                        var row = new Dictionary<string, string[]>();
                        for (int i = 0; i < columnNames.Length && i < values.Length; i++)
                        {
                            string columnName = columnNames[i];
                            string[] columnValues = new string[] { values[i] }; // Convert value to array
                            row[columnName] = columnValues;

                            var columlId = _context.Columns.Where(x=>x.Name== columnName && x.CompanyId== companyid).ToList()[0].Id;
                            var entity = new ColumnValue
                            {
                                Value = columnValues[0],
                                ColumnId = columlId,
                            };
                            _context.ColumnValues.Add(entity);
                            await _context.SaveChangesAsync();

                        }
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }
    }
}