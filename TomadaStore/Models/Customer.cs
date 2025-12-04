namespace TomadaStore.Models.Models
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool Active { get; private set; }

        public Customer(string firstName, string lastName, string email, bool active)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Active = active;
        }

        public Customer(string firstName, string lastName, string email, string? phoneNumber, bool active)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Active = active;
        }
    }
}
