using Bogus;
using Communication.Requests;

namespace TestUtility.Requests;

public class RequestUserRegistrationBuilder {
  
    public static JsonUserRegistrationRequest RampUp(int tamanhoSenha = 10) {

        return new Faker<JsonUserRegistrationRequest>()
            .RuleFor(request => request.Name, f => f.Person.FullName)
            .RuleFor(request => request.Email, f => f.Internet.Email())
            .RuleFor(request => request.Password, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(request => request.PhoneNumber, f => f.Phone.PhoneNumber(
                    "!#9########"
                ).Replace("!", $"{f.Random.Int(min: 1, max: 9)}")
            )
            .RuleFor(request => request.BirthDate, f => f.Person.DateOfBirth);
 
    }

}
