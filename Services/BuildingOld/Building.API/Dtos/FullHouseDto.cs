namespace Building.Dtos;

public class FullHouseDto
{
    public HouseDto HouseDto { get; }
    public IEnumerable<RoomDto> RoomDtos { get; }

    public FullHouseDto(HouseDto houseDto, IEnumerable<RoomDto> roomDtos)
    {
        HouseDto = houseDto;
        RoomDtos = roomDtos;
    }
}