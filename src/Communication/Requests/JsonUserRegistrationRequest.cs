namespace Communication.Requests;

public class JsonUserRegistrationRequest
{

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }

}
