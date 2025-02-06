namespace services_order_add.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string TeamCode { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Priority { get; set; }
        public string OrderStatus { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
