import uuid

from app.domain.room import Room


class House:
    def __init__(self, id: uuid, alias: str, description: str, rooms: list[Room]):
        self.id = str(id)
        self.alias = alias
        self.description = description
        self.rooms = rooms
