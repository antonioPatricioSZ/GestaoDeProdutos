using System.Runtime.Serialization;

namespace Exceptions.ExceptionsBase;

[Serializable]
public class InvalidLoginException : GestaoDeProdutosException {

    public InvalidLoginException() : base(ResourceErrorMessages.LOGIN_INVALIDO) { }

    protected InvalidLoginException(SerializationInfo info, StreamingContext context)
    : base(info, context) { }

}
