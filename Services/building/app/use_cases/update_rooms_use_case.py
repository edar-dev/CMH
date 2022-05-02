import uuid

from app.repositories.ports import UnitOfWork
from app.domain.room import Room


def update_rooms_use_case(house_id: uuid, rooms: list[Room], uow: UnitOfWork):
    db_house = uow.houses.get(str(house_id))

    for room in rooms:
        room_to_update = next(filter(lambda r: r.id == room.id, db_house.rooms), None)
        if room_to_update:
            if room.alias:
                room_to_update.alias = room.alias
            if room.description:
                room_to_update.description = room.description

    uow.commit()
