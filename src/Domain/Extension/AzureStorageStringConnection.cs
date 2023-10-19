namespace Domain.Extension;

public class AzureStorageStringConnection {

    private readonly string _azureConnectionString;

    public AzureStorageStringConnection(string azureConnectionString)
    {
        _azureConnectionString = azureConnectionString;
    }

    public string AzureStorageConnectionString()
    {
        return _azureConnectionString;
    }

}
