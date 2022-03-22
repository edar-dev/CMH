namespace Building.Dtos
{
    public class RoomDto
    {
        private string name;
        private double dimension;

        public RoomDto(string name, double dimension)
        {
            this.name = name;
            this.dimension = dimension;
        }
    }
}
