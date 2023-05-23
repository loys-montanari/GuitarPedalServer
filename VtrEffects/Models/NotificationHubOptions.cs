using System.ComponentModel.DataAnnotations;

namespace VtrEffects.Models
{
    public class NotificationHubOptions
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ConnectionString { get; set; }
    }
}
