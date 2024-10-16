namespace Althaus_Warehouse.Models.Entities
{
    public class Item
    {

        public Item() { }
        public Item(int id, string name, string description, int quantity, int price)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
