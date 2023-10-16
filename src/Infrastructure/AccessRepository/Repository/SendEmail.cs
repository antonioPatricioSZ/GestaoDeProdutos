using Domain.Repositories.Email.SendEmail;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Infrastructure.AccessRepository.Repository;

public class SendEmail : ISendEmail {

    private readonly string ApiKey = "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";

    public void Send(string emailsTo, string subject, string message) {

        Configuration.Default.ApiKey["api-key"] = ApiKey;

        var apiInstance = new TransactionalEmailsApi();

        string SenderName = "Patricio Teste";
        string SenderEmail = "jug73881@gmail.com";
        SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

        SendSmtpEmailTo emailReceiver1 = new SendSmtpEmailTo(emailsTo);
        List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
        To.Add(emailReceiver1);

        string HtmlContent = message;
        string TextContent = null;

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(
                emailSender,
                To,
                null,
                null,
                HtmlContent,
                TextContent,
                subject
            );
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Console.WriteLine("Response: \n " + result.ToJson());
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro: " + e.Message);
        }

    }


    //private MailMessage PrepareteMessage(List<string> emailsTo, string subject)
    //{

        

        //var mail = new MailMessage();
        //mail.From = new MailAddress("jug73881@gmail.com");

        //foreach (var email in emailsTo)
        //{
        //    if (ValidateEmail(email))
        //    {
        //        mail.To.Add(email);
        //    }
        //}

        //mail.Subject = subject;
        //mail.IsBodyHtml = true;

        //mail.Subject = subject;
        //mail.IsBodyHtml = true;


        //var imageUrl = "https://testedeployazurestorage.blob.core.windows.net/user-images/1ffd4197-9d13-45e7-9c9f-6641c9131722.jpg";

        //mail.Body = $"<img src=\"{imageUrl}\" alt=\"Azure Blob Image\" />";

        //return mail;

    //}


    //private static bool ValidateEmail(string email)
    //{
    //    var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    //    if (regex.IsMatch(email))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
  

    //private static void SendEmailBySmtp(MailMessage message) {
    //    var smtpClient = new SmtpClient();
    //    smtpClient.Host = "smtp.gmail.com";
    //    smtpClient.Port = 587;
    //    smtpClient.EnableSsl = false;
    //    smtpClient.Timeout = 50000;
    //    smtpClient.UseDefaultCredentials = false;
    //    smtpClient.Credentials = new NetworkCredential("jug73881@gmail.com", "Patricio123@");
    //    smtpClient.Send(message);
    //    smtpClient.Dispose();
    //}

}
