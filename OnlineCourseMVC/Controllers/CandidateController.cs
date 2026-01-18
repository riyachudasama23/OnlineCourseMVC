using Microsoft.AspNetCore.Mvc;
using OnlineCourseMVC.Models;
using System.Data;
using Microsoft.Data.SqlClient;


namespace OnlineCourseMVC.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IConfiguration _configuration;

        public CandidateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gives us a SQL connection using appsettings.json
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("con"));
        }

        public IActionResult Index()
        {
            List<Candidate> list = new List<Candidate>();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Candidates", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Candidate
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FullName = dr["FullName"].ToString(),
                        Course = dr["Course"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        DOB = Convert.ToDateTime(dr["DOB"]),
                        DOJ = Convert.ToDateTime(dr["DOJ"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }
            }

            return View(list);
        }

        public IActionResult AddEdit(int id = 0)
        {
            Candidate c = new Candidate();

            if (id > 0)
            {
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Candidates WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        c.Id = Convert.ToInt32(dr["Id"]);
                        c.FullName = dr["FullName"].ToString();
                        c.Course = dr["Course"].ToString();
                        c.DOB = Convert.ToDateTime(dr["DOB"]);
                        c.DOJ = Convert.ToDateTime(dr["DOJ"]);
                        c.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    }
                }
            }

            return View(c);
        }


        [HttpPost]
        public IActionResult Save(Candidate c)
        {
            // 1. Check if dates are selected
            if (!c.DOB.HasValue || !c.DOJ.HasValue)
            {
                ModelState.AddModelError("", "Please select both Date of Birth and Date of Joining.");
                return View("AddEdit", c);
            }

            DateTime dob = c.DOB.Value;
            DateTime doj = c.DOJ.Value;

            // 2. Age validation (must be at least 10 years old)
            int age = DateTime.Now.Year - dob.Year;
            if (dob > DateTime.Now.AddYears(-age))
                age--;

            if (age < 10)
            {
                ModelState.AddModelError("", "Candidate must be at least 10 years old to register.");
                return View("AddEdit", c);
            }

            // 3. All fields mandatory check (Model validation)
            if (!ModelState.IsValid)
            {
                return View("AddEdit", c);
            }

            // 4. Call stored procedure
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_SaveCandidate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", c.Id);
                cmd.Parameters.AddWithValue("@FullName", c.FullName);
                cmd.Parameters.AddWithValue("@Course", c.Course);
                cmd.Parameters.AddWithValue("@DOB", dob);   // use non-null DateTime
                cmd.Parameters.AddWithValue("@DOJ", doj);   // use non-null DateTime
                cmd.Parameters.AddWithValue("@IsActive", c.IsActive);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // 5. Success message
            TempData["msg"] = "Record saved successfully!";

            // 6. Redirect back to listing page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Candidates WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["msg"] = "Record deleted successfully!";
            return RedirectToAction("Index");
        }


    }
}
