from sqlalchemy.orm import sessionmaker, scoped_session

from app.data.dbmeta import HouseEntity
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from test.integration_test import IntegrationTest


class UOWTestCase(IntegrationTest):
    def test_can_write_house(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:

            new_house = HouseEntity(
                id="some_id", alias="some_alias", description="some_desc"
            )
            uow.houses.save(new_house)

            uow.commit()

            house = uow.houses.get(new_house.id)

            self.assertEqual(new_house.id, house.id)
            self.assertEqual(new_house.alias, house.alias)
            self.assertEqual(new_house.description, house.description)

    def test_can_write_house_2(self):
        self._session_maker = scoped_session(sessionmaker(self.engine))

        uowm = SqlAlchemyUnitOfWorkManager(self._session_maker)

        with uowm.start() as uow:

            new_house = HouseEntity(
                id="some_id", alias="some_alias", description="some_desc"
            )
            uow.houses.save(new_house)

            uow.rollback()

            house = uow.houses.get(new_house.id)

            self.assertIsNone(house)
