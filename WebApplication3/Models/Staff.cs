namespace WebApplication3.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty; // Quản lý, Lễ tân, HLV...
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty; // Ca sáng, Ca chiều, Ca đêm
        public StaffStatus Status { get; set; }
    }

    public enum StaffStatus
    {
        Active,   
        Inactive
    }
}