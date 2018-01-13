﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_mvc.Entity;
using Inventory_mvc.Models;

namespace Inventory_mvc.Function
{
    public class RoleService
    {
        public RoleInfo GetById(int id)
        { 
            List<rolePermission> rolePermissionList = new List<rolePermission>();
            List<permissionInfo> permissions = new List<permissionInfo>();
            string rolename = "";
            string description = "";
            using (userPermissionTestEntities context = new userPermissionTestEntities())
            {
                rolePermissionList = context.rolePermission.Where(x => x.roleID == id).ToList();
                if (context.roleInfo.Where(x => x.roleID == id).ToList().Count > 0)
                {
                    rolename = context.roleInfo.Where(x => x.roleID == id).First().roleName.ToString();
                    description = context.roleInfo.Where(x => x.roleID == id).First().description.ToString();

                    foreach (var p in rolePermissionList)
                    {
                        permissions.Add(context.permissionInfo.Where(x => x.permissionID == p.permissionID).First());
                    }
                }
            
            }
            return new RoleInfo
            {
                RoleId = id,
                RoleName = rolename,
                Description = description,
                Permissions = permissions,
            };
        }

        //saving the permission into DB
        public void CreatePermissions(int permissionID, string controller, string action)
        {
            using (userPermissionTestEntities context = new userPermissionTestEntities())
            {
                permissionInfo p = new permissionInfo();
                p.permissionID = permissionID;
                p.action = action;
                p.controller = controller;
                p.description = "";
                context.permissionInfo.Add(p);
                context.SaveChanges();
            }
        }

        //get permission from DB
        public List<permissionInfo> GetDefinedPermissions()
        {
            using (userPermissionTestEntities context = new userPermissionTestEntities())
            {
                return context.permissionInfo.ToList();
            }   
        }

        //Add role in DB
        public void AddRole(RoleInfo role)
        {
            using (userPermissionTestEntities context = new userPermissionTestEntities())
            {
                roleInfo r = new roleInfo();
                r.roleID = role.RoleId;
                r.roleName = role.RoleName;
                r.description = role.Description;
                context.roleInfo.Add(r);
                foreach (var ps in role.Permissions)
                {
                    rolePermission rp = new rolePermission();
                    rp.roleID = role.RoleId;
                    rp.permissionID = ps.permissionID;

                    context.rolePermission.Add(rp);
                }
                context.SaveChanges();
            }
        }

    }
}