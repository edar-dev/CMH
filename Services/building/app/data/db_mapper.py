from app.domain.house import House
from app.data.dbmeta import HouseEntity, RoomEntity
from app.domain.room import Room


def to_house_entity(house: House) -> HouseEntity:
    rooms = [to_room_entity(room) for room in house.rooms]
    return HouseEntity(
        id=house.id, alias=house.alias, description=house.description, rooms=rooms
    )


def to_room_entity(room: Room) -> RoomEntity:
    return RoomEntity(id=room.id, alias=room.alias, description=room.description)
