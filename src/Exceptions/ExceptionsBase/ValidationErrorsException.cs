using System.Runtime.Serialization;

namespace Exceptions.ExceptionsBase;

[Serializable]
public class ValidationErrorsException : GestaoDeProdutosException {

    public List<string> ErrorMessages { get; set; }

    public ValidationErrorsException(List<string> errorMessages ) : base(string.Empty) {
        ErrorMessages = errorMessages;
    }

    protected ValidationErrorsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

}
