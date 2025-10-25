using SIGA.Domain.Entities.Base;

namespace SIGA.Domain.Entities
{
    public class Maestros : BaseUsers
    {
        public int MaestroId { get; set; }
        public string UserId { get; set; }
        public string Especialidad { get; set; }
        public string GradoAcademico { get; set; }
    }
}
