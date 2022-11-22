using Employee_Records_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace Employee_Records_API.DTO
{
    public class ImageDto
    {
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Photo { get; set; }
    }
}
