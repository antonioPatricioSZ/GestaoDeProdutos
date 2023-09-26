namespace Communication.Responses;

public class ResponseErrorJson {

    public List<string> Messages { get; set; }

    public ResponseErrorJson(string message) {
        Messages = new List<string> { 
            message
        };
    }

    public ResponseErrorJson(List<string> messages) {
        Messages = messages;
    }

}
