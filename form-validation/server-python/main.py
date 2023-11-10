from fastapi import FastAPI
from fastapi.responses import JSONResponse
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from temper_validation_demo.form import Form, validate_all


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
    errors = validate(form)
    if errors:
        return JSONResponse(status_code=422, content={"errors": errors})
    return {"message": "Form processed"}


def validate(raw_form: RawForm):
    form = Form(raw_form.minValue, raw_form.maxValue)
    return validate_all(form)
