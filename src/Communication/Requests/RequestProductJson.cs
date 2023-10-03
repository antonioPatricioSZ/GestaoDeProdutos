namespace Communication.Requests;

public class RequestProductJson {

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PricePurchase { get; set; }
    public decimal PriceSale { get; set; }
    public long CategoryId { get; set; }

}
