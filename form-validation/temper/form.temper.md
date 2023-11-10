# Temper Form Validation Demo

## Form

This represents arbitrary data for business needs. We might should find a more
specific example that's still easy to imagine how it applies generally.

    export class Form {
      public minValue: Float64;
      public maxValue: Float64;
    }

## Validation

For simplicity, just use strings for error reporting.

    export let ErrorMessage = String;

Export a variety of functions that can be used in different contexts. We might
want to validate all before submitting or accepting a submission.

    export let validateAll(form: Form): List<ErrorMessage> {
      runValidations(
        form,
        [minValid, maxValid, minNotAboveMax],
      )
    }

Or we might validate individual fields when interactive. Note that some
validations apply to multiple fields.

    export let validateMinValue(form: Form): List<ErrorMessage> {
      runValidations(form, [minNonNegative, minNotAboveMax])
    }

    export let validateMaxValue(form: Form): List<ErrorMessage> {
      runValidations(form, [maxNonNegative, minNotAboveMax])
    }

## Internal Validation Logic

We don't need to export specific rules, just appropriate combos above. From here
down, give specifics that are used by the exported combos.

These still might have multiple validations but should only apply to individual
fields, not combinations of fields.

    let minValid(form: Form): List<ErrorMessage> {
      runValidations(form, [minNonNegative])
    }

    let maxValid(form: Form): List<ErrorMessage> {
      runValidations(form, [maxNonNegative])
    }

    let minNonNegative = validateLowerLimit("Min value", 0.0) { (form);;
      form.minValue
    };

    let maxNonNegative = validateLowerLimit("Max value", 0.0) { (form);;
      form.maxValue
    };

And here's a combo validation.

    let minNotAboveMax(form: Form): List<ErrorMessage> {
      if (form.minValue > form.maxValue) {
        ["Min can't be above max"]
      } else {
        []
      }
    }

## Machinery

Here we have general validation machinery and support functions used for
building rules.

    let Validation = fn (Form): List<ErrorMessage>;

    let runValidations(
      form: Form, validations: Listed<Validation>
    ): List<ErrorMessage> {
      let errors = new ListBuilder<ErrorMessage>();
      for (var i = 0; i < validations.length; i += 1) {
        errors.addAll(validations[i](form));
      }
      errors.toList()
    }

    let validateLowerLimit(
      name: String, limit: Float64, getValue: fn (Form): Float64
    ): Validation {
      fn (form: Form): List<ErrorMessage> {
        let value = getValue(form);
        if (value < limit) {
          [
            "${name} ${value.toString()} can't be below ${limit.toString()}",
          ]
        } else {
          []
        }
      }
    }
