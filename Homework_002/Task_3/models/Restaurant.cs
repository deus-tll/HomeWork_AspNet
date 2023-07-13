namespace Task_3.models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public required float Rating { get; set; }
    }
}
