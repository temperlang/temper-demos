# Validation Testing

## Tests of Good Data

    test("good forms") {
      assert(validateAll(new Form(0.1, 2.5)).length == 0);
    }

## Tests of Bad Data

    test("bad forms") {
      let negativeMin = validateAll(new Form(-1.0, 7.5));
      assert(negativeMin.length == 1);
      assert(negativeMin[0] == "Min value can't be below 0.0");

      let nanMax = validateAll(new Form(1.0, NaN));
      assert(nanMax.length == 1);
      assert(nanMax[0] == "Max value must be numeric");
    }

## Imports

    let { Form, validateAll } = import("./form");
