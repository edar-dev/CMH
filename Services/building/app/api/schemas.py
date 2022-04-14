from marshmallow import Schema, fields

class CreateHouseSchema(Schema):
    alias = fields.Str(required=True)
    description = fields.Str(required=True)

