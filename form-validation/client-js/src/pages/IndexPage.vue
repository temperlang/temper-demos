<template>
  <q-page class="row items-start justify-evenly q-pa-xl">
    <q-form>
      <q-card style="min-width: 300px">
        <q-card-section class="q-pb-none">
          <h5>Specification</h5>
        </q-card-section>
        <q-card-section>
          <q-input
            name="minValue"
            label="Min Value"
            v-model="form.minValue"
            reactive-rules
            :rules="[() => validateMinValue(form)]"
          />
          <q-input
            name="maxValue"
            label="Max Value"
            v-model="form.maxValue"
            reactive-rules
            :rules="[() => validateMaxValue(form)]"
          />
        </q-card-section>
        <q-card-section>
          <q-btn name="submit" label="Submit" color="primary" />
        </q-card-section>
      </q-card>
    </q-form>
    <!-- <example-component
      title="Example component"
      active
      :todos="todos"
      :meta="meta"
    ></example-component> -->
  </q-page>
</template>

<script lang="ts">
// import { P } from 'app/dist/spa/assets/index.9f261a1d';
// import { Todo, Meta } from 'components/models';
// import ExampleComponent from 'components/ExampleComponent.vue';
import { defineComponent, ref } from 'vue';
import {
  Form,
  validateMinValue,
  validateMaxValue,
} from 'temper-validation-demo/form.js';

function numeric(val: string): boolean | string {
  return !val || !isNaN(parseFloat(val)) || 'Must be numeric';
}

// function minBelowMax(val: string, form: Form): boolean | string {
//   return (
//     (parseFloat(form.maxValue) || Infinity) >=
//       (parseFloat(form.minValue) || 0) || "Min can't be above max"
//   );
// }

// const rulesByName = {
//   minValue: [numeric, minBelowMax],
//   maxValue: [numeric, minBelowMax],
// };

// function validate(
//   form: Form,
//   name: keyof typeof rulesByName
// ): (val: string) => boolean | string {
//   const rules = rulesByName[name];
//   return (val: string) => {
//     for (const rule of rules) {
//       const result = rule(val, form);
//       if (result !== true) {
//         return result;
//       }
//     }
//     return true;
//   };
// }

type RawForm = {
  minValue: string;
  maxValue: string;
};

function applyValidation(
  rawForm: RawForm,
  key: keyof RawForm,
  validation: (form: Form) => Array<string>
) {
  // Validate the focus field for being numeric.
  const error = numeric(rawForm[key]);
  if (typeof error == 'string') {
    return error;
  }
  // Given at least that, process common validation.
  const form = parseForm(rawForm);
  const errors = validation(form);
  // Just return one error at most for interactive.
  return errors.length ? errors[0] : true;
}

function parseForm(rawForm: RawForm): Form {
  return new Form(
    parseNumeric(rawForm.minValue) ?? 0,
    parseNumeric(rawForm.maxValue) ?? Infinity
  );
}

function parseNumeric(val: string): number | undefined {
  return val ? Number(val) : undefined;
}

export default defineComponent({
  name: 'IndexPage',
  components: {
    // ExampleComponent,
  },
  setup() {
    return {
      form: ref({
        maxValue: ref(),
        minValue: ref(),
      }),
      numeric,
      validateMinValue(rawForm: RawForm) {
        return applyValidation(rawForm, 'minValue', validateMinValue);
      },
      validateMaxValue(rawForm: RawForm) {
        return applyValidation(rawForm, 'maxValue', validateMaxValue);
      },
    };
  },
});
</script>
