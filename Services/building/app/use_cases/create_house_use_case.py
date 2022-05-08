import logging
from logging import Logger

from app.domain.house import House
from app.repositories.ports import UnitOfWork
from app.data.db_mapper import to_house_entity


def create_house_use_case(house: House, uow: UnitOfWork, logger: Logger = logging.getLogger()):
    logger.info("create new house", house_id=house.id, house_alias=house.alias, house_desc=house.description)
    try:
        house_entity = to_house_entity(house)
        uow.houses.save(house_entity)
        uow.commit()
    except Exception:
        logger.exception("something bad happened while creating new house")
