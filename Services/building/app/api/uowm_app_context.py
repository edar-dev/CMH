from flask import g
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, scoped_session
from app.repositories.unit_of_work import SqlAlchemyUnitOfWorkManager
from app.repositories.ports import UnitOfWorkManager


def get_uowm() -> UnitOfWorkManager:
    if "uowm" not in g:
        database_name = "postgres"
        connection_string = f"postgresql+psycopg2://postgres:mysecretpassword@localhost:5432/{database_name}"
        engine = create_engine(connection_string, echo=True, future=True)

        _session_maker = scoped_session(sessionmaker(engine))

        g.uowm = SqlAlchemyUnitOfWorkManager(_session_maker)

    return g.uowm
