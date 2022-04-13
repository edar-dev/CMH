import os.path
import unittest
import nanoid
from alembic.config import Config
from alembic.command import upgrade
from sqlalchemy import create_engine

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
        self.database_file = f"{self.database_name}.db"
        self.connection_string = f"sqlite:///{self.database_file}"

        _run_migrations(self.connection_string)

        self.engine = create_engine(self.connection_string, echo=True, future=True)

    def tearDown(self):
        try:
            os.remove(self.database_file)
        except OSError:
            pass
