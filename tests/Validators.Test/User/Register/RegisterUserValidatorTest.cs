using Application.UseCases.User.Register;
using Exceptions;
using FluentAssertions;
using TestUtility.Requests;
using Xunit;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest {

    [Fact]
    public void Validar_Sucesso() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();

    }


    [Fact]
    public void Validar_Erro_Nome_Vazio() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Name = string.Empty;
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            error => error.ErrorMessage.Equals(ResourceErrorMessages.NOME_USUARIO_EM_BRANCO)    
        );

    }


    [Fact]
    public void Validar_Erro_Email_Vazio() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Email = string.Empty;
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_USUARIO_EM_BRANCO)    
        );

    }


    [Fact]
    public void Validar_Erro_Senha_Vazia() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Password = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.SENHA_USUARIO_EM_BRANCO)
        );

    }


    [Fact]
    public void Validar_Erro_Telefone_Vazio() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.PhoneNumber = string.Empty;
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.TELEFONE_USUARIO_EM_BRANCO)
        );

    }


    [Fact]
    public void Validar_Erro_DataNascimento_Vazia() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.BirthDate = DateTime.MinValue;
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.DATA_NASCIMENTO_EM_BRANCO)
        );

    }


    [Fact]
    public void Validar_Erro_Email_Invalido() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Email = "patricio6630";
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO)    
        );

    }


    [Fact]
    public void Validar_Erro_Telefone_Invalido() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.PhoneNumber = "123456";
        requisicao.Password = "Patricio123@";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(
            erro => erro.ErrorMessage.Equals(ResourceErrorMessages.TELEFONE_USUARIO_INVALIDO)
        );

    }


    [Fact]
    public void Validar_Erro_Senha_Fraca() {

        var validator = new RegisterUserValidator();

        var requisicao = RequestUserRegistrationBuilder.RampUp();
        requisicao.Password = "patricio123";

        var isWeak = 
            requisicao.Password.Any(char.IsUpper) && 
            requisicao.Password.Any(char.IsLower) &&
            requisicao.Password.Any(char.IsDigit) &&
            requisicao.Password.Any(char.IsSymbol) &&
            requisicao.Password.Length >= 8;

        var resultado = validator.Validate(requisicao);

        if(!isWeak) {
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().ContainSingle().And.Contain(
                erro => erro.ErrorMessage.Equals(ResourceErrorMessages.SENHA_FRACA)
            );
        }

    }


}
