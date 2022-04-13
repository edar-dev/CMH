import unittest

from app.data.dbmeta import HouseEntity
from test.integration_test import IntegrationTest
from sqlalchemy.orm import Session
from sqlalchemy import select


class BuildingRepositoryTestCase(IntegrationTest):
    def test_can_write_house(self):
        with Session(self.engine) as session:
            new_house = HouseEntity(id="some_id", alias="some_alias", description="some_desc")

            session.add(new_house)
            session.commit()

        with Session(self.engine) as session:
            result = session.execute(select(HouseEntity).where(HouseEntity.id == "some_id"))

            house_just_inserted = result.scalars().one_or_none()

            self.assertIsNotNone(house_just_inserted)

