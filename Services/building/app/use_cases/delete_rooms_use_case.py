import uuid
from logging import Logger
import logging
from app.repositories.ports import UnitOfWork


def delete_rooms_use_case(house_id: uuid, rooms_ids: list[uuid], uow: UnitOfWork, logger: Logger = logging.getLogger()):
    logger.info("delete rooms from house", house_id=house_id, rooms=rooms_ids.count())

    try:
        house = uow.houses.get(str(house_id))
        rooms_ids_converted = list(map(lambda room_id: str(room_id), rooms_ids))
        rooms_to_keep = list(
            filter(lambda room: room.id not in rooms_ids_converted, house.rooms)
        )
        house.rooms = rooms_to_keep
        uow.commit()
    except:
        logger.exception("something bad happened while deleting rooms from an house")

