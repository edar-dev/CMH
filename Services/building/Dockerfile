FROM python:3.10-alpine

WORKDIR /building

RUN pip install pipenv

COPY Pipfile Pipfile.lock ./

RUN PIPENV_VENV_IN_PROJECT=1 pipenv install
COPY src .
CMD ["python3", "app.py"]