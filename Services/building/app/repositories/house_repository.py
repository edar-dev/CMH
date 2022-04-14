from sqlalchemy.orm import Session

from app.data.dbmeta import HouseEntity


class HouseRepository:
    def __init__(self, db_session: Session):
        self._session = db_session

    def add(self, house: HouseEntity) -> None:
        self._session.add(house)

    def get(self, house_id) -> HouseEntity:
        return self._session.get(HouseEntity, house_id)
