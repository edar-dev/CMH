from sqlalchemy.orm import declarative_base
from sqlalchemy import Column, String

Base = declarative_base()


class HouseEntity(Base):
    __tablename__ = "houses"

    id = Column(String, primary_key=True)
    alias = Column(String, nullable=False)
    description = Column(String, nullable=False)
