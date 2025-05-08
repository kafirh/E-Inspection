using MachineInspection.Application.DTO;
using MachineInspection.Application.IHelper;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Repositories;

namespace MachineInspection.Application.Service
{
    public class MachineService
    {
        private readonly IMachineRepository _machineRepository;
        public MachineService(IMachineRepository machineRepository) 
        {
            _machineRepository = machineRepository;
        }

        public async Task<List<MachineDto>> GetMachineDtosAsync(string buId) 
        {
            List<Machine> machines;

            if (buId == "ALL")
            {
                machines = await _machineRepository.GetAll();  // Ambil semua data
            }
            else
            {
                machines = await _machineRepository.GetAll(buId);  // Ambil data berdasarkan BU
            }
            var machineDtos = machines.Select(m => new MachineDto
            {
                machineId = m.machineId,
                machineName = m.machineName,
                machineNumber = m.machineNumber,
                line = m.line,
                sectionName = m.sectionName,
                documentNo = m.documentNo,
                buId = m.buId,
            }).ToList();

            return machineDtos;
        }

        public async Task<bool> CreateMachineAsync(MachineCreateDto machineCreateDto)
        {
            try
            {
                var machine = new Machine
                {
                    machineId = machineCreateDto.MachineId,
                    sectionName = machineCreateDto.SectionName,
                    machineName = machineCreateDto.MachineName,
                    line = machineCreateDto.Line,
                    machineNumber = machineCreateDto.MachineNumber,
                    documentNo = machineCreateDto.DocumentNo,
                    buId = machineCreateDto.BuId
                };
                await _machineRepository.Create(machine);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        public async Task<MachineDto> GetMachineByIdAsync(string machineId)
        {
            var machine = await _machineRepository.GetMachineById(machineId);
            var machineDto = new MachineDto
            {
                machineId = machine.machineId,
                machineName = machine.machineName,
                machineNumber = machine.machineNumber,
            };
            return machineDto;
        }
    }
}
