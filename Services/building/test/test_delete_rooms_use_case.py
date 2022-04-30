import uuid

from sqlalchemy.orm import sessionmaker, scoped_session

from app.domain.house import House
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.use_cases.create_house_use_case import create_house_use_case
from app.domain.room import Room
from test.integration_test import IntegrationTest
from app.use_cases.delete_house_use_case import delete_house_use_case
from app.use_cases.delete_rooms_use_case import delete_rooms_use_case


class DeleteRoomsUseCaseTestCase(IntegrationTest):
    def test_can_delete_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room3 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room4 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room5 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(
                uuid.uuid1(),
                "houseAlias",
                "houseDesc",
                [room1, room2, room3, room4, room5],
            )
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            delete_rooms_use_case(house.id, [room2.id, room4.id], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertEqual(3, len(house_entity.rooms))
