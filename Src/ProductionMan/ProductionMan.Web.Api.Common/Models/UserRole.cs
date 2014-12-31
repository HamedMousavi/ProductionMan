using System.Collections.Generic;


namespace ProductionMan.Web.Api.Common.Models
{

    public class UserRole
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
        
        public override string ToString()
        {
            return RoleName;
        }

        #region SerializationLimitations
        private bool _shouldSerializePermissions;

        public void SetShouldSerializePermissions(bool shouldSerialize)
        {
            _shouldSerializePermissions = shouldSerialize;
        }

        public bool ShouldSerializePermissions()
        {
            return _shouldSerializePermissions;
        }
        #endregion SerializationLimitations
    }
}
