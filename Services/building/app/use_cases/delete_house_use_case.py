import uuid

from repositories.ports import UnitOfWork


def delete_house_use_case(house_id: uuid, uow: UnitOfWork):
    uow.houses.delete(house_id)
    uow.commit()
