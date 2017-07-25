using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
{
    public class TrackInputModel
    {
        [Required]
        [MinLength(1)]
        public string TrackName { get; set; }
    }
}