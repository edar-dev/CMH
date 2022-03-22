namespace Building.Dtos
{
    public class HouseDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<object> Rooms { get; }

        public HouseDto(Guid id, string name, IEnumerable<object> enumerable)
        {
            Id = id;
            Name = name;
            Rooms = enumerable;
        }
    }
}
