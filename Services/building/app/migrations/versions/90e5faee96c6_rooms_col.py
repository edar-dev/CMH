"""Rooms col

Revision ID: 90e5faee96c6
Revises: 7cfd5e934654
Create Date: 2022-04-30 13:36:47.677161

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = "90e5faee96c6"
down_revision = "7cfd5e934654"
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.create_table(
        "rooms",
        sa.Column("id", sa.String(), nullable=False),
        sa.Column("parent_id", sa.String(), nullable=True),
        sa.Column("alias", sa.String(), nullable=False),
        sa.Column("description", sa.String(), nullable=False),
        sa.ForeignKeyConstraint(
            ["parent_id"],
            ["houses.id"],
        ),
        sa.PrimaryKeyConstraint("id"),
    )
    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.drop_table("rooms")
    # ### end Alembic commands ###
