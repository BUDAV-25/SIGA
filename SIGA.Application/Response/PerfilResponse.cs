using SIGA.Application.Response.Base;

namespace SIGA.Application.Response
{
    public class PerfilResponse : BaseResponse
    {
        public PerfilResponse()
        {
            HasError = false;
        }

        public dynamic Data { get; set; }
    }
}
