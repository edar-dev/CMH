import logging

from flask import Blueprint, request

from app.api.schemas import CreateHouseSchema
from app.api.uowm_app_context import get_uowm
from app.use_cases.create_house_use_case import create_house_use_case
import  structlog

bp = Blueprint("house", __name__, url_prefix="/house")


@bp.route("/create", methods=("GET", "POST"))
def create():
    data = request.get_json()
    try:
        create_house_model = CreateHouseSchema().load(data)
    except Exception:
        return {"error": True, "errorMessage": "error while deserializing payload"}

    with get_uowm().start() as uow:
        create_house_use_case(create_house_model, uow, structlog.get_logger("devLogger"))

    return create_house_model.alias
