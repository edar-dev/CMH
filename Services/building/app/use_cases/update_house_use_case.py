from app.domain.house import House
from app.repositories.ports import UnitOfWork
from logging import Logger
import logging


def update_house_use_case(house: House, uow: UnitOfWork, logger: Logger = logging.getLogger()):
    logger.info("update house", house_id=house.id, house_alias=house.alias, house_desc=house.description)

    try:
        db_house = uow.houses.get(str(house.id))

        if house.alias:
            db_house.alias = house.alias

        if house.description:
            db_house.description = house.description

        uow.commit()
    except:
        logger.exception("something bad happened while updating an house")

