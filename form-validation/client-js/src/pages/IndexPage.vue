<template>
  <q-page class="row items-start justify-evenly q-pa-xl">
    <q-form @submit="submit(form)">
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
        <q-expansion-item label="Settings">
          <q-card>
            <q-card-section>
              <q-select
                label="server"
                v-model="form.server"
                :options="serverOptions"
              />
            </q-card-section>
          </q-card>
        </q-expansion-item>
        <q-card-section>
          <q-btn
            type="submit"
            name="submit"
            label="Submit"
            color="primary"
            @click="submitIfInvalid(form)"
          />
        </q-card-section>
      </q-card>
    </q-form>
  </q-page>
</template>

<script lang="ts">
import { QVueGlobals, useQuasar } from 'quasar';
import {
  Form,
  validateMinValue,
  validateMaxValue,
} from 'temper-validation-demo/form.js';
import { defineComponent, ref } from 'vue';

type RawForm = {
  minValue: string;
  maxValue: string;
  server: string;
};

type Response = {
  errors?: string[];
  message?: string;
};

function applyValidation(
  rawForm: RawForm,
  key: keyof RawForm,
  validation: (form: Form) => Array<string>
): string | true {
  // Given at least that, process common validation.
  const form = parseForm(rawForm);
  const errors = validation(form);
  // Just return one error at most for interactive.
  return errors.length ? errors[0] : true;
}

function parseFormObject(rawForm: RawForm): Form {
  return {
    minValue: parseNumeric(rawForm.minValue) ?? 0,
    maxValue: parseNumeric(rawForm.maxValue) ?? Infinity,
  };
}

function parseForm(rawForm: RawForm): Form {
  let object = parseFormObject(rawForm);
  return new Form(object.minValue, object.maxValue);
}

function parseNumeric(val: string): number | undefined {
  return val ? Number(val) : undefined;
}

function stringify(object: unknown): string {
  return JSON.stringify(object, (_, value: string) => {
    return (
      {
        Infinity: 'Infinity',
        [-Infinity]: '-Infinity',
        NaN: 'NaN',
      }[value] ?? value
    );
  });
}

const rules = {
  validateMinValue(rawForm: RawForm) {
    return applyValidation(rawForm, 'minValue', validateMinValue);
  },
  validateMaxValue(rawForm: RawForm) {
    return applyValidation(rawForm, 'maxValue', validateMaxValue);
  },
};

const serverPort = new Map(
  Object.entries({
    'C#': 5013,
    Python: 8000,
  })
);

async function submit($q: QVueGlobals, rawForm: RawForm) {
  const response: Response = await (
    await fetch(`http://localhost:${serverPort.get(rawForm.server)}/`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: stringify(parseFormObject(rawForm)),
    })
  ).json();
  if (response.errors) {
    $q.notify({ message: response.errors.join(', '), type: 'negative' });
  } else if (response.message) {
    $q.notify({ message: response.message, type: 'positive' });
  } else {
    $q.notify({ message: 'Unexpected response', type: 'negative' });
    console.log(response);
  }
}

function submitIfInvalid($q: QVueGlobals, form: RawForm) {
  // This is a hack to allow submission even if invalid.
  const anyInvalid = [rules.validateMinValue, rules.validateMaxValue].find(
    (rule) => rule(form) !== true
  );
  if (anyInvalid) {
    submit($q, form);
  }
}

export default defineComponent({
  name: 'IndexPage',
  setup() {
    const $q = useQuasar();
    console.log($q);
    return {
      form: ref({
        maxValue: ref(),
        minValue: ref(),
        server: ref('C#'),
      }),
      serverOptions: ['C#', 'Python'],
      submit: (rawForm: RawForm) => submit($q, rawForm),
      submitIfInvalid: (rawForm: RawForm) => submitIfInvalid($q, rawForm),
      ...rules,
    };
  },
});
</script>
