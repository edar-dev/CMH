namespace Building.Dtos
{
    public class RoomDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public double Dimension { get; }
        public Guid HouseId { get; }

        public RoomDto(Guid id, string name, double dimension, Guid houseId)
        {
            Id = id;
            Name = name;
            Dimension = dimension;
            HouseId = houseId;
        }
    }
}
