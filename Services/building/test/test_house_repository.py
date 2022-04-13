import unittest

from app.data.dbmeta import HouseEntity
from test.integration_test import IntegrationTest
from sqlalchemy.orm import Session
from app.repositories.house_repository import get_house, save_house


class HouseRepositoryTestCase(IntegrationTest):
    def test_can_write_house(self):
        with Session(self.engine) as session:
            new_house = HouseEntity(
                id="some_id", alias="some_alias", description="some_desc"
            )

            save_house(session, new_house)
            house = get_house(session, new_house.id)

            self.assertEqual(new_house.id, house.id)
            self.assertEqual(new_house.alias, house.alias)
            self.assertEqual(new_house.description, house.description)
