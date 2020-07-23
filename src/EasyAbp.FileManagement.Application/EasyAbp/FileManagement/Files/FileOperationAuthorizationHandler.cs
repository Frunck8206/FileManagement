﻿using System.Threading.Tasks;
using EasyAbp.FileManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EasyAbp.FileManagement.Files
{
    public abstract class FileOperationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, File>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement, File resource)
        {
            var hasPermission = requirement.Name switch
            {
                FileManagementPermissions.File.Default => await HasGetInfoPermissionAsync(context, resource),
                FileManagementPermissions.File.Download => await HasDownloadPermissionAsync(context, resource),
                FileManagementPermissions.File.Create => await HasCreatePermissionAsync(context, resource),
                FileManagementPermissions.File.Update => await HasUpdatePermissionAsync(context, resource),
                FileManagementPermissions.File.Move => await HasMovePermissionAsync(context, resource),
                FileManagementPermissions.File.Delete => await HasDeletePermissionAsync(context, resource),
                _ => false
            };

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
        
        protected abstract Task<bool> HasGetInfoPermissionAsync(AuthorizationHandlerContext context, File resource);
        
        protected abstract Task<bool> HasDownloadPermissionAsync(AuthorizationHandlerContext context, File resource);
        
        protected abstract Task<bool> HasCreatePermissionAsync(AuthorizationHandlerContext context, File resource);
        
        protected abstract Task<bool> HasUpdatePermissionAsync(AuthorizationHandlerContext context, File resource);
         
        protected abstract Task<bool> HasMovePermissionAsync(AuthorizationHandlerContext context, File resource);
         
        protected abstract Task<bool> HasDeletePermissionAsync(AuthorizationHandlerContext context, File resource);
    }
}