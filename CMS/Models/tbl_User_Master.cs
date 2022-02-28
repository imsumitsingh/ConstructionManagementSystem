using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class tbl_User_Master
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? mobile { get; set; }
        [Required]

        public string? village { get; set; }
        [Required]
        public string? post { get; set; }
        [Required]
        public string? policeStation { get; set; }
        [Required]
        public string? district { get; set; }
        [Required]
        public string? state { get; set; }
        [Required]
        public string? country { get; set; }
        
        public IFormFile? SavePhoto { get; set; }
        public string? photo { get; set; }
        public DateTime cDate { get; set; }
    }


}
