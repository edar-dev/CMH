from repositories.house_repository import HouseRepository
from repositories.ports import UnitOfWorkManager, UnitOfWork


class SqlAlchemyUnitOfWorkManager(UnitOfWorkManager):
    """The Unit of work manager returns a new unit of work.
    Our UOW is backed by a sql alchemy session whose
    lifetime can be scoped to a web request, or a
    long-lived background job."""

    def __init__(self, session_maker):
        self.session_maker = session_maker

    def start(self):
        return SqlAlchemyUnitOfWork(self.session_maker)


class SqlAlchemyUnitOfWork(UnitOfWork):
    def __init__(self, session_factory):
        self.session_factory = session_factory

    def __enter__(self):
        self.session = self.session_factory()
        return self

    def __exit__(self, type, value, traceback):
        self.session.close()

    def commit(self):
        self.session.commit()

    def rollback(self):
        self.session.rollback()

    @property
    def houses(self):
        return HouseRepository(self.session)
