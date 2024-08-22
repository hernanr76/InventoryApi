namespace PablitosCustom.Model
{
    public class InventoryResponse
    {
        public Guid guid { get; set; }
        public int delay { get; set; }
        public required List<InventoryItem> data { get; set; }
    }
}
