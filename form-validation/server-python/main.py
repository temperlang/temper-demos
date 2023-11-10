from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from temper_validation_demo.form import Form


app = FastAPI()
app.add_middleware(
    CORSMiddleware,
    allow_headers=["*"],
    allow_methods=["*"],
    allow_origins=["http://localhost:9000"],
)


class RawForm(BaseModel):
    minValue: float
    maxValue: float


@app.post("/")
def post_form(form: RawForm):
    print(form)
    return {}
