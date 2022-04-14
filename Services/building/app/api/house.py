import functools

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
    schema = CreateHouseSchema()
    pass
