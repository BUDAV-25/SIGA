using SIGA.Domain.Entities.Base;

namespace SIGA.Domain.Entities
{
    public class Estudiantes : BaseUsers
    {
        public int EstudianteId { get; set; }
        public string UserId { get; set; }
        public string Matricula { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public string Nota { get; set; }
        public int GradoId { get; set; }
    }
}
