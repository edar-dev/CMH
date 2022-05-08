import uuid
from logging import Logger
import logging

from app.repositories.ports import UnitOfWork


def delete_house_use_case(house_id: uuid, uow: UnitOfWork, logger: Logger = logging.getLogger()):
    logger.info("delete house", house_id=house_id)

    try:
        uow.houses.delete(house_id)
        uow.commit()
    except:
        logger.exception("something bad happened while deleting an house")

