from sqlalchemy.orm import declarative_base, relationship
from sqlalchemy import Column, String, ForeignKey

Base = declarative_base()


class HouseEntity(Base):
    __tablename__ = "houses"

    id = Column(String, primary_key=True)
    alias = Column(String, nullable=False)
    description = Column(String, nullable=False)
    rooms = relationship("RoomEntity", back_populates="parent")


class RoomEntity(Base):
    __tablename__ = "rooms"

    id = Column(String, primary_key=True)
    parent_id = Column(String, ForeignKey("houses.id"))
    alias = Column(String, nullable=False)
    description = Column(String, nullable=False)
    parent = relationship("HouseEntity", back_populates="rooms")

