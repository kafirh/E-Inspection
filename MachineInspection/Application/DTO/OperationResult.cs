﻿namespace MachineInspection.Application.DTO
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static OperationResult Ok(string message = "") => new OperationResult { Success = true, Message = message };
        public static OperationResult Fail(string message) => new OperationResult { Success = false, Message = message };
    }

}
