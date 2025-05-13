using MachineInspection.Application.IHelper;

namespace MachineInspection.Infrastructure.Helper
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> SaveImageAsync(IFormFile file, string machineId, int inspectionId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File tidak valid");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(uploadsFolder);

            // Pakai ekstensi .jpg karena di client sudah dijadikan JPEG
            var fileName = $"{machineId}-{inspectionId.ToString()}.jpg";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
