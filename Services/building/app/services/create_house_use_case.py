from app.domain.house import House
from app.repositories.ports import UnitOfWork
from app.data.dbmeta import HouseEntity


def create_house_use_case(house: House, uow: UnitOfWork):
    house_entity = HouseEntity(
        id=house.id, alias=house.alias, description=house.description
    )
    uow.houses.add(house_entity)
    uow.commit()
    pass
