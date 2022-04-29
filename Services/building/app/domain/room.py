import uuid


class Room:
    def __init__(self, id: uuid, alias: str, description: str):
        self.id = id
        self.alias = alias
        self.description = description