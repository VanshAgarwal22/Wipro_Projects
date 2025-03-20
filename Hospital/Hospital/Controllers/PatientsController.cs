using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Models;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PatientsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients.Include(p => p.Doctor).ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _context.Patients.Include(p => p.Doctor)
                                                 .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Invalid patient data.");
            }

            // Ensure DoctorId is provided
            if (patient.DoctorId <= 0)
            {
                return BadRequest("DoctorId is required.");
            }

            // Check if the doctor exists in the database
            var doctorExists = await _context.Doctors.AnyAsync(d => d.DoctorId == patient.DoctorId);
            if (!doctorExists)
            {
                return BadRequest("Invalid DoctorId. Doctor not found.");
            }

            // Add the patient to the database
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            // Return the created patient along with the doctor details
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest("Patient ID mismatch.");
            }

            // Check if the doctor exists in the database
            var doctorExists = await _context.Doctors.AnyAsync(d => d.DoctorId == patient.DoctorId);
            if (!doctorExists)
            {
                return BadRequest("Invalid DoctorId. Doctor not found.");
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
