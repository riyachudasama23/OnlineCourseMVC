using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseMVC.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string? Course { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Date of Joining is required")]
        public DateTime? DOJ { get; set; }

        public bool IsActive { get; set; }
    }
}
