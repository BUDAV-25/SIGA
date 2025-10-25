using SIGA.Application.Dtos.Entities;
using SIGA.Application.Helpers;

namespace SIGA.Web.Middlewares
{
    public class ValidateUserSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool HasUser()
        {
            UsuariosDto usuarios = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

            if (usuarios == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
