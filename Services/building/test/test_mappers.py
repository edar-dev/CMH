import unittest

from app.api.schemas import CreateHouseSchema, CreateRoomSchema
from app.data.db_mapper import to_room_entity, to_house_entity


class RoomMapperTestCase(unittest.TestCase):
    def test_can_map_room(self):

        payload = {"alias": "roomalias", "description": "roomdescription"}

        room_domain = CreateRoomSchema().load(payload)

        room_entity = to_room_entity(room_domain)

        self.assertIsNotNone(room_entity)

    def test_can_map_house(self):
        payload = {
            "alias": "housealias",
            "description": "housedescription",
            "rooms": [
                {"alias": "roomalias", "description": "roomdescription"},
                {"alias": "roomalias2", "description": "roomdescription2"},
            ],
        }

        house_domain = CreateHouseSchema().load(payload)

        house_entity = to_house_entity(house_domain)

        self.assertIsNotNone(house_entity)
