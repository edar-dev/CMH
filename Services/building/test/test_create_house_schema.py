import unittest

from app.api.schemas import CreateHouseSchema


class CreateHouseSchemaTestCase(unittest.TestCase):
    def test_can_deserialize_schema(self):

        payload = {
            "alias": "housealias",
            "description": "housedescription",
            "rooms": [{"alias": "roomalias", "description": "roomdescription"}],
        }

        house_domain = CreateHouseSchema().load(payload)

        self.assertIsNotNone(house_domain)

    def test_can_NOT_deserialize_schema(self):

        payload = {
            "not_an_alias": "housealias",
            "description": "housedescription",
            "rooms": [{"alias": "roomalias", "description": "roomdescription"}],
        }

        with self.assertRaises(Exception) as context:
            house_domain = CreateHouseSchema().load(payload)

        self.assertTrue(context.exception)
