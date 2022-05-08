import uuid
from logging import Logger
import logging

from app.repositories.ports import UnitOfWork
from app.data.db_mapper import to_room_entity
from app.domain.room import Room


def add_rooms_use_case(house_id: uuid, rooms: list[Room], uow: UnitOfWork, logger: Logger = logging.getLogger()):
    logger.info("adding rooms to house ", house_id=house_id, rooms=rooms.copy())

    try:
        house = uow.houses.get(str(house_id))
        new_rooms = [to_room_entity(room) for room in rooms]
        house.rooms = house.rooms + new_rooms
        uow.commit()
    except Exception:
        logger.exception("something bad happened while adding rooms to house")
