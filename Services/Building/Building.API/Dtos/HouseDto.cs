namespace Building.Dtos
{
    public class HouseDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }

        public HouseDto(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}