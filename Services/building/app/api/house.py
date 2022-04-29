import functools
from pprint import pprint

from flask import (
    Blueprint,
    flash,
    g,
    redirect,
    render_template,
    request,
    session,
    url_for,
)

from app.api.schemas import CreateHouseSchema

bp = Blueprint("house", __name__, url_prefix="/house")


@bp.route("/create", methods=("GET", "POST"))
def create():
    data = request.get_json()
    try:
        schema = CreateHouseSchema().load(data)
    except Exception:
        return {"error": True, "errorMessage": "error while deserializing payload"}

    pprint(schema)
    return schema.alias
