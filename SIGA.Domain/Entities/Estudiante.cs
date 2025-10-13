namespace SIGA.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string MedicalConditions { get; set; }
        public string MedicalNote { get; set; }
        public string EmergencyContact { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
