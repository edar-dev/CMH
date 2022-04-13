from sqlalchemy import select
from sqlalchemy.orm import Session
from app.data.dbmeta import HouseEntity


def get_house(session: Session, house_id) -> HouseEntity:
    result = session.execute(select(HouseEntity).where(HouseEntity.id == house_id))

    return result.scalars().one_or_none()


def save_house(session: Session, house: HouseEntity):
    session.add(house)
