    export let ErrorMessage = String;

    export class Form {
      public minValue: Float64;
      public maxValue: Float64;
    }

    export let validateAll(form: Form): ErrorMessage | Null {
      runValidations(
        form,
        [minValid, maxValid, validateMinMaxValues],
      )
    }

    export let validateMinValue(form: Form): ErrorMessage | Null {
      runValidations(form, [minNonNegative, validateMinMaxValues])
    }

    export let validateMaxValue(form: Form): ErrorMessage | Null {
      runValidations(form, [maxNonNegative, validateMinMaxValues])
    }

    export let validateMinMaxValues(form: Form): ErrorMessage | Null {
      if (form.minValue > form.maxValue) {
        return "Min can't be above max";
      }
      null
    }

    let minValid(form: Form): ErrorMessage | Null {
      runValidations(form, [minNonNegative])
    }

    let maxValid(form: Form): ErrorMessage | Null {
      runValidations(form, [maxNonNegative])
    }

    let minNonNegative = validateLowerLimit("Min value", 0.0) { (form);;
      form.minValue
    };

    let maxNonNegative = validateLowerLimit("Max value", 0.0) { (form);;
      form.maxValue
    };

    let Validation = fn (Form): String | Null;

    let runValidations(
      form: Form, validations: Listed<Validation>
    ): String | Null {
      for (var i = 0; i < validations.length; i += 1) {
        let error = validations[i](form);
        if (error != null) {
          return error;
        }
      }
      null
    }

    let validateLowerLimit(
      name: String, limit: Float64, getValue: fn (Form): Float64
    ): Validation {
      fn (form: Form): String | Null {
        let value = getValue(form);
        if (value < limit) {
          return (
            "${name} ${value.toString()} can't be below ${limit.toString()}"
          );
        }
        null
      }
    }
