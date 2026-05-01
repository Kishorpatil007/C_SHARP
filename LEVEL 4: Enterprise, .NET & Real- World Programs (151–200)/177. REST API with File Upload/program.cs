using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var path = Path.Combine("Uploads", file.FileName);
        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        return Ok();
    }

    [HttpGet("download/{fileName}")]
    public IActionResult Download(string fileName)
    {
        var path = Path.Combine("Uploads", fileName);
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "application/octet-stream", fileName);
    }
}
