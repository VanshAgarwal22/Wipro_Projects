using Hospital.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    [Required]
    public string? PatientName { get; set; }

    [Required]
    public string? HealthProblem { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? MobileNo { get; set; }

    [Required]
    public int Age { get; set; }

    // Foreign Key (DoctorId) to link with Doctor Table
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; } // Ensure DoctorId is provided

    [JsonIgnore]
    public Doctor Doctor { get; set; }
}
