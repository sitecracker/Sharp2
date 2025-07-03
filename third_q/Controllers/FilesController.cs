using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net2_third.Domain;

namespace net2_third.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultiple(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest("No files uploaded");

            foreach (var file in files)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok("ყველა ფაილი წარმატებით ატვირთუალია");
        }


        [HttpPost("generate-excel")]
        public IActionResult GenerateAndSaveExcel()
        {
            var fileName = $"Test_ExcelFile.xlsx";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            var fullPath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("data");

            worksheet.Cell(1, 1).Value = "სახელი";
            worksheet.Cell(1, 2).Value = "გვარი";
            worksheet.Cell(2, 1).Value = "სახელი1";
            worksheet.Cell(2, 2).Value = "გვარი1";
            worksheet.Cell(3, 1).Value = "სახელი2";
            worksheet.Cell(3, 2).Value = "გვარი2";

            worksheet.Columns().AdjustToContents();
            worksheet.Range("A1:B1").SetAutoFilter();
            workbook.SaveAs(fullPath);

            return Ok($"Excel file '{fileName}' generated and saved successfully.");
        }


        [HttpPost("address")]
        public IActionResult ValidateAddress([FromBody] AddressModal model)
        {
            return Ok("Address is valid");
        }
    }
}
