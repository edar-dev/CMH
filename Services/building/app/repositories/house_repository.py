from sqlalchemy import select
from sqlalchemy.orm import Session
from data.dbmeta import HouseEntity


def get_house(session: Session, id):
    result = session.execute(select(HouseEntity).where(HouseEntity.id == id))

    return result.scalars().one_or_none()


def save_house(session: Session):
    session.add()
    pass