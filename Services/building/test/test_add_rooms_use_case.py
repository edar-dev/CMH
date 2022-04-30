import uuid

from sqlalchemy.orm import sessionmaker, scoped_session

from app.domain.house import House
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.use_cases.create_house_use_case import create_house_use_case
from app.domain.room import Room
from test.integration_test import IntegrationTest
from app.use_cases.add_rooms_use_case import add_rooms_use_case


class AddRoomsUseCaseTestCase(IntegrationTest):
    def test_can_add_rooms_to_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        room3 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room4 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            add_rooms_use_case(house.id, [room3, room4], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)

            self.assertEqual(len(house_entity.rooms), 4)

    def test_can_add_rooms_to_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        room3 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room4 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            with self.assertRaises(Exception) as context:
                add_rooms_use_case(house.id, [room1, room3, room4], uow)

            self.assertTrue(context.exception)


