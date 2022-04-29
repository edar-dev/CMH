import uuid
from pprint import pprint

from marshmallow import Schema, fields, post_load

from app.domain.house import House
from app.domain.room import Room


class CreateRoomSchema(Schema):
    alias = fields.Str(required=True)
    description = fields.Str(required=True)

    @post_load
    def make_room_domain_entity(self, data, **kwargs) -> Room:
        return Room(uuid.uuid1(), **data)


class CreateHouseSchema(Schema):
    alias = fields.Str(required=True)
    description = fields.Str(required=True)
    rooms = fields.List(fields.Nested(CreateRoomSchema))

    @post_load
    def make_house_domain_entity(self, data, **kwargs) -> House:
        pprint(data)
        house = House(uuid.uuid1(), **data)
        return house

