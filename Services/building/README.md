Add in zsh the entry:
export PIPENV_VENV_IN_PROJECT=1

After clone go in the project folder and run 
>  pipenv update

Pycharm settings, add the correct interpreter

configure the run configuration


to generate a new migration run 
> alembic revision --autogenerate -m "Really fancy message"

end then 
> alembic upgrade head 


docker network create -d bridge my-bridge-network

docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d --network="my-bridge-network" postgres

docker run --name cmh-pgadmin -p 65123:80 -d -e PGADMIN_DEFAULT_EMAIL=admin@admin.ch -e PGADMIN_DEFAULT_PASSWORD=Password01! --network="my-bridge-network" dpage/pgadmin4