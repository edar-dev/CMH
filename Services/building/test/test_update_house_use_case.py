import uuid

from sqlalchemy.orm import sessionmaker, scoped_session

from app.domain.house import House
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.use_cases.create_house_use_case import create_house_use_case
from test.integration_test import IntegrationTest
from app.use_cases.update_house_use_case import update_house_use_case


class UpdateHouseUseCaseTestCase(IntegrationTest):
    def test_can_save_new_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            updated_house = House(house.id, "updatedHouseAlias", "updatedHouseDesc", [])
            update_house_use_case(updated_house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(updated_house.id, house_entity.id)
            self.assertEqual(updated_house.alias, house_entity.alias)
            self.assertEqual(updated_house.description, house_entity.description)

    def test_can_save_new_house_partially(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            updated_house = House(house.id, "updatedHouseAlias", "", [])
            update_house_use_case(updated_house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(updated_house.id, house_entity.id)
            self.assertEqual(updated_house.alias, house_entity.alias)
            self.assertEqual(house.description, house_entity.description)

    def test_can_save_new_house_partially_again(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            updated_house = House(house.id, "", "updatedHouseDesc", [])
            update_house_use_case(updated_house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(updated_house.id, house_entity.id)
            self.assertEqual(house.alias, house_entity.alias)
            self.assertEqual(updated_house.description, house_entity.description)
