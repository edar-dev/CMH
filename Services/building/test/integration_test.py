import os.path
import unittest
import nanoid
from alembic.config import Config
from alembic.command import upgrade
from sqlalchemy import create_engine
from sqlalchemy.exc import SQLAlchemyError
from sqlalchemy_utils import database_exists, create_database, drop_database

_DATABASE_NAME_ALPHABET = "qwertyuyiopasdfghjklzxcvbnm"

migrations_location = os.path.join(
    os.path.dirname(os.path.abspath(__file__)), "..", "app", "migrations"
)


def _run_migrations(database_connection_string):
    test_migrations_config = Config()

    test_migrations_config.set_main_option("script_location", migrations_location)
    test_migrations_config.set_main_option("sqlalchemy.url", database_connection_string)

    upgrade(test_migrations_config, "head")


class IntegrationTest(unittest.TestCase):
    def setUp(self):
        self.database_name = nanoid.generate(_DATABASE_NAME_ALPHABET, 64)
        self.connection_string = f"postgresql+psycopg2://postgres:mysecretpassword@localhost:5432/{self.database_name}"

        self.engine = create_engine(self.connection_string, echo=True, future=True)

        if not database_exists(self.engine.url):
            create_database(self.engine.url)

        _run_migrations(self.connection_string)

    def tearDown(self):
        try:
            if database_exists(self.engine.url):
                drop_database(self.engine.url)
        except SQLAlchemyError as er:
            print(er)
