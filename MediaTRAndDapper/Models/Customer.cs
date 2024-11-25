using MediaTRAndDapper.Models.BaseEntity;

namespace MediaTRAndDapper.Models
{
    public class Customer : BaseEntity<int>
    {
        private byte[] _passwordHash;

        private byte[] _passwordSalt;

        public required string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public required string Address { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public DateTime? CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Customer(string email, string phoneNumber, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            _passwordHash = passwordHash;
            _passwordSalt = passwordSalt;
            CreatedDate = DateTime.UtcNow;
            Active = true;
            Deleted = false;
            Orders = new List<Order>();
        }

    }
}
