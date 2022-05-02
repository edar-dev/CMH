from app.domain.house import House
from app.repositories.ports import UnitOfWork


def update_house_use_case(house: House, uow: UnitOfWork):
    db_house = uow.houses.get(str(house.id))

    if house.alias:
        db_house.alias = house.alias

    if house.description:
        db_house.description = house.description

    uow.commit()
