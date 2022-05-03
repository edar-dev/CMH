from logging.config import fileConfig
import os

import structlog
from flask import Flask
import logging
from structlog.stdlib import LoggerFactory

def create_app(test_config=None):
    # create and configure the app
    app = Flask(__name__, instance_relative_config=True)
    fileConfig('logging.cfg', defaults={'logfilename': 'mylog.log', 'errorlogfilename': 'myerror.log'})
    structlog.configure(logger_factory=LoggerFactory())
    app.config.from_mapping(
        SECRET_KEY="dev",
        DATABASE=os.path.join(app.instance_path, "flaskr.sqlite"),
    )
    if test_config is None:
        # load the instance config, if it exists, when not testing
        app.config.from_pyfile("config.py", silent=True)
    else:
        # load the test config if passed in
        app.config.from_mapping(test_config)

    # ensure the instance folder exists
    try:
        os.makedirs(app.instance_path)
    except OSError:
        pass

    # a simple page that says hello
    @app.route("/hello")
    def hello():
        return "Hello, World!"

    from app.api import house

    app.register_blueprint(house.bp)

    return app
