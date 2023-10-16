namespace Domain.Repositories.Email.SendEmail;

public interface ISendEmail {

    void Send(string emailsTo, string subject, string message);

}
