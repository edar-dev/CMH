from app.domain.house import House
from app.repositories.ports import UnitOfWork
from app.data.dbmeta import HouseEntity
from app.data.db_mapper import to_house_entity


def create_house_use_case(house: House, uow: UnitOfWork):
    house_entity = to_house_entity(house)
    uow.houses.save(house_entity)
    uow.commit()
