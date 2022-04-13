using System;
using System.Linq;
using Building.Controllers;
using Building.Dtos;
using Building.Repositories;
using Building.Requests;
using Building.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Building.Integration;

public class BuildingControllerTest
{
    private readonly BuildingController _buildingController;
    private readonly InMemoryHouseRepository _inMemoryHouseRepository;
    private readonly InMemoryRoomRepository _inMemoryRoomRepository;

    public BuildingControllerTest()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();

        if (factory == null) return;

        var buildingControllerLogger = factory.CreateLogger<BuildingController>();
        var houseServiceLogger = factory.CreateLogger<HouseService>();

        _inMemoryHouseRepository = new InMemoryHouseRepository();
        _inMemoryRoomRepository = new InMemoryRoomRepository();
        var houseService = new HouseService(houseServiceLogger, _inMemoryHouseRepository, _inMemoryRoomRepository);
        _buildingController = new BuildingController(buildingControllerLogger, houseService);
    }

    [Fact]
    public async void CreateHouse()
    {
        //ARRANGE
        var newRoomsModel = new RoomsModel("Room name", 123);
        var createHouseRequest = new CreateHouseRequest("HouseTest", "House Description",
            new[] {newRoomsModel});

        //ACT
        var result = await _buildingController.Post(createHouseRequest);
        //ASSERT

        result.Should().NotBeNull();
        result.Should().BeOfType<RedirectToActionResult>();
        var redirectToActionResult = (RedirectToActionResult) result;
        redirectToActionResult.ActionName.Should().Be("Get");
        var houseId = redirectToActionResult.RouteValues!["id"] is Guid
            ? (Guid) redirectToActionResult.RouteValues!["id"]
            : default;
        houseId.Should().NotBeEmpty();
        var house = await _inMemoryHouseRepository.Get(houseId);
        house.Name.Should().Be(createHouseRequest.Name);
        house.Description.Should().Be(createHouseRequest.Description);
        var rooms = await _inMemoryRoomRepository.GetByHouseId(houseId);
        rooms.Should().NotBeNullOrEmpty();
        rooms.Should().HaveCount(1);
        rooms.Single().Name.Should().Be(newRoomsModel.Name);
        rooms.Single().Dimension.Should().Be(newRoomsModel.Dimension);
        rooms.Single().Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public async void GetHouse()
    {
        //ARRANGE
        var newRoomsModel = new RoomsModel("Room name", 123);
        var createHouseRequest = new CreateHouseRequest("HouseTest", "House Description",
            new[] {newRoomsModel});
        var createHouseResult = await _buildingController.Post(createHouseRequest);

        var redirectToActionResult = (RedirectToActionResult) createHouseResult;
        redirectToActionResult.ActionName.Should().Be("Get");
        var houseId = redirectToActionResult.RouteValues!["id"] is Guid
            ? (Guid) redirectToActionResult.RouteValues!["id"]
            : default;
        //ACT

        ActionResult result =await _buildingController.Get(houseId);
        //ASSERT
        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        var typedResult = result as OkObjectResult;

        typedResult.Value.Should().NotBeNull();
        var house = typedResult.Value as FullHouseDto;

        house.HouseDto.Id.Should().Be(houseId);
        house.HouseDto.Name.Should().Be(createHouseRequest.Name);
        house.HouseDto.Description.Should().Be(createHouseRequest.Description);
    }
}