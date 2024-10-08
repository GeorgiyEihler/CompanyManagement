﻿using Microsoft.AspNetCore.Authorization;

namespace CompanyManagement.Infrastructure.Authorization;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }  

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
