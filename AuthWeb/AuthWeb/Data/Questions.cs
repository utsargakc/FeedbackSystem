using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthWeb.Data
{

    [Table("Topics")]
    public class Topics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        public string Status { get; set; } = "Inactive";

        public string EnableRating { get; set; } = "Disable";

        public DateOnly startDate { get; set; }

        public DateOnly endDate { get; set; }

        public int repeatingNumber { get; set; }

        public string? recurrenceType { get; set; }

        public string? selectedDays { get; set; }
       
        public int? dayOfMonth { get; set; }

        public string Created_by { get; set; }

        [ForeignKey("Created_by")]
        public virtual ApplicationUser User { get; set; }
    }

    [Table("Responses")]
    public class Responses
    {
        [Key]
        public int Id { get; set; }
        public string Response { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TopicId { get; set; }
        public string isEdited { get; set; } = "False";

        [ForeignKey("TopicId")]
        public virtual Topics Topic { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual ApplicationUser User { get; set; }
    }

    [Table("AdditionalFeedback")]
    public class AdditionalFeedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Feedback { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string isEdited { get; set; } = "False";
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

}
