using System.ComponentModel.DataAnnotations;

namespace IINSwalayan.Models
{
    public class Banner
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? LinkUrl { get; set; }
        
        public int Order { get; set; } = 0; // Untuk urutan tampil
        
        public bool IsActive { get; set; } = true;
        
        public DateTime StartDate { get; set; } = DateTime.Now;
        
        public DateTime? EndDate { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [StringLength(50)]
        public string? ButtonText { get; set; } = "KLIK DI SINI";
        
        [StringLength(7)]
        public string? BackgroundColor { get; set; } = "#FF6B6B"; // Hex color
        
        [StringLength(7)]
        public string? TextColor { get; set; } = "#FFFFFF"; // Hex color
        
        // Check if banner is currently active based on date range
        public bool IsCurrentlyActive => 
            IsActive && 
            DateTime.Now >= StartDate && 
            (EndDate == null || DateTime.Now <= EndDate);
    }
}