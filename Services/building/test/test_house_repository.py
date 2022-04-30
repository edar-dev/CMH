from app.data.dbmeta import HouseEntity
from test.integration_test import IntegrationTest
from sqlalchemy.orm import Session
from app.repositories.house_repository import HouseRepository


class HouseRepositoryTestCase(IntegrationTest):
    def test_can_write_house(self):
        with Session(self.engine) as session:
            new_house = HouseEntity(
                id="some_id", alias="some_alias", description="some_desc"
            )

            repo = HouseRepository(session)
            repo.save(new_house)
            session.commit()

        with Session(self.engine) as session:
            repo = HouseRepository(session)
            house = repo.get("some_id")

            self.assertEqual("some_id", house.id)
            self.assertEqual("some_alias", house.alias)
            self.assertEqual("some_desc", house.description)

    def test_can_delete_house(self):
        with Session(self.engine) as session:
            new_house = HouseEntity(
                id="some_id", alias="some_alias", description="some_desc"
            )

            repo = HouseRepository(session)
            repo.save(new_house)
            session.commit()

        with Session(self.engine) as session:
            repo = HouseRepository(session)
            repo.delete("some_id")
            session.commit()

        with Session(self.engine) as session:
            repo = HouseRepository(session)
            house = repo.get("some_id")

            self.assertIsNone(house)
