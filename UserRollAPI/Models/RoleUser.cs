namespace UserRollAPI.Models
{
    public class RoleUser
    {
        public Guid UsersId { get; set; }
        public User User { get; set; }

        public Guid RolesId { get; set; }
        public Role Role { get; set; }

       

    }
}
