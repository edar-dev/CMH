import uuid


class House:
    def __init__(self, id: uuid, alias: str, description: str, rooms):
        self.id = id
        self.alias = alias
        self.description = description
        self.rooms = rooms
