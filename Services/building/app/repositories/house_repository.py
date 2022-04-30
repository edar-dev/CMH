from sqlalchemy.orm import Session

from app.data.dbmeta import HouseEntity


# TODO: Refactor the repository so that accept domain model and covert to db entities internally
class HouseRepository:
    def __init__(self, db_session: Session):
        self._session = db_session

    def save(self, house: HouseEntity) -> None:
        self._session.add(house)

    def get(self, house_id) -> HouseEntity:
        return self._session.get(HouseEntity, str(house_id))
