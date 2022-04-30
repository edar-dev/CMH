import uuid

from sqlalchemy.orm import sessionmaker, scoped_session

from app.domain.house import House
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.use_cases.create_house_use_case import create_house_use_case
from app.domain.room import Room
from test.integration_test import IntegrationTest


class CreateHouseUseCaseTestCase(IntegrationTest):
    def test_can_save_new_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(house.id, house_entity.id)
            self.assertEqual(house.alias, house_entity.alias)
            self.assertEqual(house.description, house_entity.description)

    def test_can_save_new_house_with_room(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            room = Room(uuid.uuid1(), "roomAlias", "roomDesc")
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(house.id, house_entity.id)
            self.assertEqual(house.alias, house_entity.alias)
            self.assertEqual(house.description, house_entity.description)
            self.assertEqual(len(house_entity.rooms), 1)

    def test_can_save_new_house_with_rooms(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:
            room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
            room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            self.assertEqual(house.id, house_entity.id)
            self.assertEqual(house.alias, house_entity.alias)
            self.assertEqual(house.description, house_entity.description)
            self.assertEqual(len(house_entity.rooms), 2)
