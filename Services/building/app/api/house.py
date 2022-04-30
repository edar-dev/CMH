from pprint import pprint

from flask import Blueprint, request, g, current_app
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, scoped_session

from app.api.uowm_app_context import get_uowm
from app.api.schemas import CreateHouseSchema
from app.use_cases.create_house_use_case import create_house_use_case

bp = Blueprint("house", __name__, url_prefix="/house")


@bp.route("/create", methods=("GET", "POST"))
def create():
    data = request.get_json()
    try:
        create_house_model = CreateHouseSchema().load(data)
    except Exception:
        return {"error": True, "errorMessage": "error while deserializing payload"}

    with get_uowm().start() as uow:
        create_house_use_case(create_house_model, uow)

    return create_house_model.alias
