using System;
using System.Linq;
using Building.Dtos;
using Building.Repositories;
using Building.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Building.Integration;

public class BuildingServiceTest
{
    private readonly InMemoryHouseRepository _inMemoryHouseRepository;
    private readonly InMemoryRoomRepository _inMemoryRoomRepository;
    private readonly HouseService _houseService;

    public BuildingServiceTest()
    {
        var houseServiceLogger = new Mock<ILogger<HouseService>>().Object;

        _inMemoryHouseRepository = new InMemoryHouseRepository();
        _inMemoryRoomRepository = new InMemoryRoomRepository();
        _houseService = new HouseService(houseServiceLogger, _inMemoryHouseRepository, _inMemoryRoomRepository);
    }

    [Fact]
    public async void Create()
    {
        var houseDto = new HouseDto(Guid.NewGuid(), "HouseName", "House Description");
        var roomDto = new RoomDto(Guid.NewGuid(), "RoomName", 123, houseDto.Id);
        var roomDtos = new[] {roomDto};
        await _houseService.CreateHouse(houseDto, roomDtos);

        var dbHouse = await _inMemoryHouseRepository.Get(houseDto.Id);
        dbHouse.Should().NotBeNull();
        dbHouse.Id.Should().Be(houseDto.Id);
        dbHouse.Name.Should().Be(houseDto.Name);
        dbHouse.Description.Should().Be(houseDto.Description);

        var dbRooms = await _inMemoryRoomRepository.GetByHouseId(houseDto.Id);
        dbRooms.Should().HaveCount(1);
        var dbRoom = dbRooms.ElementAt(0);
        dbRoom.Id.Should().Be(roomDto.Id);
        dbRoom.Name.Should().Be(roomDto.Name);
        dbRoom.Dimension.Should().Be(roomDto.Dimension);
        dbRoom.HouseId.Should().Be(roomDto.HouseId);
    }

    [Fact]
    public async void Get()
    {
        var houseDto = new HouseDto(Guid.NewGuid(), "HouseName", "House Description");
        var roomDto = new RoomDto(Guid.NewGuid(), "RoomName", 123, houseDto.Id);
        var roomDtos = new[] {roomDto};
        await _houseService.CreateHouse(houseDto, roomDtos);

        var houseResponse = await _houseService.GetHouse(houseDto.Id);

        houseResponse.HouseDto.Should().NotBeNull();
        houseResponse.HouseDto.Id.Should().Be(houseDto.Id);
        houseResponse.HouseDto.Description.Should().Be(houseDto.Description);
        houseResponse.HouseDto.Name.Should().Be(houseDto.Name);
        houseResponse.RoomDtos.Should().NotBeNullOrEmpty();
        houseResponse.RoomDtos.Should().HaveCount(1);
        var room = houseResponse.RoomDtos.ElementAt(0);
        room.Id.Should().Be(roomDto.Id);
        room.Name.Should().Be(roomDto.Name);
        room.Dimension.Should().Be(roomDto.Dimension);
        room.HouseId.Should().Be(roomDto.HouseId);
    }

    [Fact]
    public async void UpdateHouse()
    {
        var houseDto = new HouseDto(Guid.NewGuid(), "HouseName", "House Description");
        var roomDto = new RoomDto(Guid.NewGuid(), "RoomName", 123, houseDto.Id);
        var roomDtos = new[] {roomDto};
        await _houseService.CreateHouse(houseDto, roomDtos);

        var houseToUpdateDto = new HouseDto(houseDto.Id, "UpdatedHouseName", "House Description");
        await _houseService.UpdateHouse(houseToUpdateDto);
        
        var dbHouse = await _inMemoryHouseRepository.Get(houseDto.Id);
        dbHouse.Should().NotBeNull();
        dbHouse.Id.Should().Be(houseToUpdateDto.Id);
        dbHouse.Name.Should().Be(houseToUpdateDto.Name);
        dbHouse.Description.Should().Be(houseToUpdateDto.Description);

    }
    
    [Fact]
    public async void AddRoom()
    {
        var houseDto = new HouseDto(Guid.NewGuid(), "HouseName", "House Description");
        var roomDto = new RoomDto(Guid.NewGuid(), "RoomName", 123, houseDto.Id);
        var roomDtos = new[] {roomDto};
        await _houseService.CreateHouse(houseDto, roomDtos);

        var roomToAddDto = new RoomDto(Guid.NewGuid(), "SecondRoom", 123, houseDto.Id);
        await _houseService.AddRoom(roomToAddDto);
     
        var dbRooms = await _inMemoryRoomRepository.GetByHouseId(houseDto.Id);
        dbRooms.Should().HaveCount(2);
        var dbRoom = dbRooms.Single(e => e.Name == roomToAddDto.Name);
        dbRoom.Id.Should().Be(roomToAddDto.Id);
        dbRoom.Name.Should().Be(roomToAddDto.Name);
        dbRoom.Dimension.Should().Be(roomToAddDto.Dimension);
        dbRoom.HouseId.Should().Be(roomToAddDto.HouseId);

    }
    
    [Fact]
    public async void UpdateRoom()
    {
        var houseDto = new HouseDto(Guid.NewGuid(), "HouseName", "House Description");
        var roomDto = new RoomDto(Guid.NewGuid(), "RoomName", 123, houseDto.Id);
        var roomDtos = new[] {roomDto};
        await _houseService.CreateHouse(houseDto, roomDtos);

        var roomToUpdateDto = new RoomDto(roomDto.Id, "RoomNameUpdated", 1234, houseDto.Id);
        await _houseService.UpdateRoom(roomToUpdateDto);
     
        var dbRooms = await _inMemoryRoomRepository.GetByHouseId(houseDto.Id);
        dbRooms.Should().HaveCount(1);
        var dbRoom = dbRooms.Single();
        dbRoom.Id.Should().Be(roomToUpdateDto.Id);
        dbRoom.Name.Should().Be(roomToUpdateDto.Name);
        dbRoom.Dimension.Should().Be(roomToUpdateDto.Dimension);
        dbRoom.HouseId.Should().Be(roomToUpdateDto.HouseId);

    }

}