using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class tbl_Login_Info
    {
        public int id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        public string? Password { get; set; }
        public bool active { get; set; }
        public DateTime cDate { get; set; }
        public DateTime lastLogin { get; set; }
        public int wrongLogin { get; set; }
        public string? IpAddress { get; set; }
        public string? macAddress { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public int mobileOtp { get; set; }
        public int emailOtp { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberLogin { get; set; }
    }

}
