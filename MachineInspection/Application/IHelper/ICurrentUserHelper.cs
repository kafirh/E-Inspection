﻿namespace MachineInspection.Application.IHelper
{
    public interface ICurrentUserHelper
    {
        string? username { get; }
        string? userId { get; }
        string? roleId { get; }
        string? roleName { get; }
        string? buId { get; }
        string? buName { get; }
    }
}
