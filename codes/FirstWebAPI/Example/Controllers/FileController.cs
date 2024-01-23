using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public FileController(IWebHostEnvironment environment)
    {
        _env = environment;
    }

    [HttpGet("stream")]
    public async Task DownloadStreamAsync()
    {
        Response.Headers.ContentDisposition = "attachment; filename=test.zip";
        var filePath = @"D:\Downloads\VisualStudio.GitHub.Copilot.vsix";
        using (var archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(filePath, "test.vsix");
        }

        await Response.CompleteAsync();
    }

    [HttpPost("upload")]
    public async Task<ActionResult<string>> UploadImgAsync(IFormFile file)
    {
        if (file == null)
        {
            ModelState.AddModelError("file", "û���ϴ����ļ�");
            return BadRequest(ModelState);
        }

        if (file.Length > 0)
        {
            string fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            long fileSize = file.Length; // ����ļ���С�����ֽ�Ϊ��λ
            // �жϺ�׺�Ƿ���ͼƬ
            string[] permittedExtensions = [".jpeg", ".jpg", ".png", ".bmp", ".svg", ".webp"];

            if (fileExt == null)
            {
                ModelState.AddModelError("file", "�ϴ����ļ�û�к�׺");
                return BadRequest(ModelState);
            }
            if (!permittedExtensions.Contains(fileExt))
            {
                ModelState.AddModelError("file", "��֧�ֵ�ͼƬ��ʽ");
                return BadRequest(ModelState);
            }
            if (fileSize > 1024 * 500 * 1)
            {
                // �ϴ����ļ����ܴ���1M
                ModelState.AddModelError("file", "�ϴ����ļ����ܴ���1M");
                return BadRequest(ModelState);
            }
            // TODO:�����ļ�
            var path = Path.Combine(_env.WebRootPath, file.Name);
            using (var fileStream = System.IO.File.Create(path))
            {
                await file.OpenReadStream().CopyToAsync(fileStream);
            }
            return path;
        }
        return Problem("�ļ�����ȷ", title: "ҵ�����");
    }
}
