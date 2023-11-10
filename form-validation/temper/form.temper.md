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
      runRules(form, allRules)
    }

Or we might validate individual fields when interactive. Note that some
validations apply to multiple fields.

    export let validateMinValue(form: Form): List<ErrorMessage> {
      runRules(form, minValueRules)
    }

    export let validateMaxValue(form: Form): List<ErrorMessage> {
      runRules(form, maxValueRules)
    }

## Internal Rule Tables

We don't need to export specific rules, just appropriate combos above. From here
down, give specifics that are used by the exported combos.

These still might have multiple validations but should only apply to individual
fields, not combinations of fields. These maps enable arbitrary combos of rules
while listing only one rule at most.

    let ruleMap = new Map([
      new Pair("maxNonNegative", maxNonNegative),
      new Pair("maxNumeric", maxNumeric),
      new Pair("minNonNegative", minNonNegative),
      new Pair("minNotAboveMax", minNotAboveMax),
      new Pair("minNumeric", minNumeric),
    ]);

Map to strings rather than functions in case we want to use rule names as keys.

    let fieldRuleMap = new Map([
      new Pair("maxValue", [
        "maxNonNegative",
        "minNotAboveMax",
        "maxNumeric",
      ]),
      new Pair("minValue", [
        "minNonNegative",
        "minNotAboveMax",
        "minNumeric",
      ]),
    ]);

We can cache different combinations here. This could potentially include
specific subsets of fields.

    let allRules = ruleMap.toList().map { (pair): Rule;; pair.value };

    let minValueRules =
      fieldRuleMap["minValue"].map { (name): Rule;; ruleMap[name] };

    let maxValueRules =
      fieldRuleMap["maxValue"].map { (name): Rule;; ruleMap[name] };

## Internal Rule Implementation

Here's where we actually implement individual rules.

    let minNumeric = validateNumeric("Min value") { (form);; form.minValue };
    let minNonNegative = validateLowerLimit("Min value", 0.0) { (form);;
      form.minValue
    };

    let maxNumeric = validateNumeric("Max value") { (form);; form.maxValue };
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

    let Rule = fn (Form): List<ErrorMessage>;

    let runRules(
      form: Form, validations: Listed<Rule>
    ): List<ErrorMessage> {
      let errors = new ListBuilder<ErrorMessage>();
      for (var i = 0; i < validations.length; i += 1) {
        errors.addAll(validations[i](form));
      }
      errors.toList()
    }

    let validateLowerLimit(
      name: String, limit: Float64, getValue: fn (Form): Float64
    ): Rule {
      fn (form: Form): List<ErrorMessage> {
        let value = getValue(form);
        if (value < limit) {
          ["${name} can't be below ${limit.toString()}"]
        } else {
          []
        }
      }
    }

    let validateNumeric(name: String, getValue: fn (Form): Float64): Rule {
      fn (form: Form): List<ErrorMessage> {
        let value = getValue(form);
        if (value == NaN || value != value) {
          ["${name} must be numeric"]
        } else {
          []
        }
      }
    }
