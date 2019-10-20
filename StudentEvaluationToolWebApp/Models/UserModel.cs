namespace StudentEvaluationToolWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Name:           Giancarlo Rhodes 
    /// Company:        Onshore Outsourcing
    /// Description:    Using the register process    
    /// </summary>
    public class UserModel : BaseModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "User must be min of 6 character and max of 20 character.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be min of 8 character and max of 20 character.")]
        [RegularExpression(".*[0-9][@#$%^&+=].*", ErrorMessage = "Password must have at least one number and one special character.")]
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

    }
}