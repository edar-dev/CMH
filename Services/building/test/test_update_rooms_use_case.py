import uuid

from sqlalchemy.orm import sessionmaker, scoped_session

from app.domain.house import House
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.use_cases.create_house_use_case import create_house_use_case
from app.domain.room import Room
from test.integration_test import IntegrationTest
from app.use_cases.update_house_use_case import update_house_use_case
from app.use_cases.update_rooms_use_case import update_rooms_use_case


class UpdateRoomsUseCaseTestCase(IntegrationTest):
    def test_can_save_updated_room(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            room_to_update = Room(room1.id, "aliasUpdated", "descUpdated")
            update_rooms_use_case(house.id, [room_to_update], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            room_updated = next(filter(lambda r: r.id == room_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room_updated)
            self.assertEqual(room_updated.id, room1.id)
            self.assertEqual(room_updated.alias, room_to_update.alias)
            self.assertEqual(room_updated.description, room_to_update.description)

    def test_can_save_updated_rooms(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            room1_to_update = Room(room1.id, "aliasUpdated1", "descUpdated1")
            room2_to_update = Room(room2.id, "aliasUpdated2", "descUpdated2")
            update_rooms_use_case(house.id, [room1_to_update,room2_to_update], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            room1_updated = next(filter(lambda r: r.id == room1_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room1_updated)
            self.assertEqual(room1_updated.id, room1.id)
            self.assertEqual(room1_updated.alias, room1_to_update.alias)
            self.assertEqual(room1_updated.description, room1_to_update.description)

            room2_updated = next(filter(lambda r: r.id == room2_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room2_updated)
            self.assertEqual(room2_updated.id, room2.id)
            self.assertEqual(room2_updated.alias, room2_to_update.alias)
            self.assertEqual(room2_updated.description, room2_to_update.description)

    def test_can_save_update_rooms_with_not_existing_room(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            room1_to_update = Room(room1.id, "aliasUpdated1", "descUpdated1")
            wrong_room_to_update = Room(uuid.uuid1(), "aliasUpdated2", "descUpdated2")
            update_rooms_use_case(house.id, [room1_to_update,wrong_room_to_update], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            room1_updated = next(filter(lambda r: r.id == room1_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room1_updated)
            self.assertEqual(room1_updated.id, room1.id)
            self.assertEqual(room1_updated.alias, room1_to_update.alias)
            self.assertEqual(room1_updated.description, room1_to_update.description)

            room2_updated = next(filter(lambda r: r.id == room2.id, house_entity.rooms), None)
            self.assertIsNotNone(room2_updated)
            self.assertEqual(room2_updated.id, room2.id)
            self.assertEqual(room2_updated.alias, room2.alias)
            self.assertEqual(room2_updated.description, room2.description)

    def test_can_save_updated_room_partially(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            room_to_update = Room(room1.id, "aliasUpdated", "")
            update_rooms_use_case(house.id, [room_to_update], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            room_updated = next(filter(lambda r: r.id == room_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room_updated)
            self.assertEqual(room_updated.id, room1.id)
            self.assertEqual(room_updated.alias, room_to_update.alias)
            self.assertEqual(room_updated.description, room1.description)

    def test_can_save_updated_room_partially2(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        room1 = Room(uuid.uuid1(), "roomAlias", "roomDesc")
        room2 = Room(uuid.uuid1(), "roomAlias", "roomDesc")

        with uowm.start() as uow:
            house = House(uuid.uuid1(), "houseAlias", "houseDesc", [room1, room2])
            create_house_use_case(house, uow)

        with uowm.start() as uow:
            room_to_update = Room(room1.id, "", "descUpdated")
            update_rooms_use_case(house.id, [room_to_update], uow)

        with uowm.start() as uow:
            house_entity = uow.houses.get(house.id)
            self.assertIsNotNone(house_entity)
            room_updated = next(filter(lambda r: r.id == room_to_update.id, house_entity.rooms), None)
            self.assertIsNotNone(room_updated)
            self.assertEqual(room_updated.id, room1.id)
            self.assertEqual(room_updated.alias, room1.alias)
            self.assertEqual(room_updated.description, room_to_update.description)
