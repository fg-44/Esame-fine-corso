using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TheAncientInn.Models
{
    public class Role : RoleProvider
    {
        ModelDbContext db = new ModelDbContext();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleNames, bool throwOnPopulateRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleNames, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();

            Tbl_Users User = db.Tbl_Users.Where(u => u.Username_User == username).FirstOrDefault();

            if (User != null)

                roles.Add(User.Role_User);

            return roles.ToArray();

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] username, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleNames)
        {
            throw new NotImplementedException();
        }

    }
}