using AutoMapper;
using ProductionMan.Data;
using ProductionMan.Data.MsAdo;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    public class RolesController : ApiController
    {

        private readonly MembershipRepository _repository;


        public RolesController()
        {
            _repository = new MembershipRepository(UnitOfWorkFactory.Create());
        }


        public IEnumerable<UserRole> GetRoles()
        {
            var list = _repository.GetRoles(string.Empty);

            return list.Select(Mapper.Map<UserRole>).ToList();
        }
    }
}
