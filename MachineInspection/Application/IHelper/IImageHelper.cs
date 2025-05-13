namespace MachineInspection.Application.IHelper
{
    public interface IImageHelper
    {
        Task<string> SaveImageAsync(IFormFile file, string machineId, int inspectionId);
    }
}
