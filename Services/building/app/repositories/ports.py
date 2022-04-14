import abc

from app.repositories.house_repository import HouseRepository


class UnitOfWork(abc.ABC):
    @abc.abstractmethod
    def __enter__(self):
        pass

    @abc.abstractmethod
    def __exit__(self, type, value, traceback):
        pass

    @abc.abstractmethod
    def commit(self):
        pass

    @abc.abstractmethod
    def rollback(self):
        pass

    @property
    @abc.abstractmethod
    def houses(self) -> HouseRepository:
        pass


class UnitOfWorkManager(abc.ABC):
    @abc.abstractmethod
    def start(self) -> UnitOfWork:
        pass
