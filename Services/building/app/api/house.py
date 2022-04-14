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

bp = Blueprint("house", __name__, url_prefix="/house")


@bp.route("/create", methods=("GET", "POST"))
def register():
    return "create"
