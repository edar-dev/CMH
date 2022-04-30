import uuid

from app.repositories.ports import UnitOfWork
from app.data.db_mapper import to_room_entity
from app.domain.room import Room


def add_rooms_use_case(house_id: uuid, rooms: list[Room], uow: UnitOfWork):
    house = uow.houses.get(str(house_id))

    new_rooms =  [to_room_entity(room) for room in rooms]
    house.rooms = house.rooms + new_rooms
    uow.commit()
