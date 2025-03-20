using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        public string? DoctorName { get; set; }

        [Required]
        public string? Speciality { get; set; }

        public string? Qualification { get; set; }

        [Required]
        public string? DoctorUserName { get; set; }

        [Required]
        public string? DoctorPassword { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Mobile { get; set; }

        // Navigation Property - One Doctor has Many Patients
        public ICollection<Patient> Patients { get; set; }
    }
}
