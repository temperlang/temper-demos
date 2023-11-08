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
            :rules="[numeric, validate(form, 'minValue')]"
          />
          <q-input
            name="maxValue"
            label="Max Value"
            v-model="form.maxValue"
            reactive-rules
            :rules="[numeric, validate(form, 'maxValue')]"
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

function numeric(val: string): boolean | string {
  return !val || !isNaN(parseFloat(val)) || 'Must be numeric';
}

function minBelowMax(val: string, form: Form): boolean | string {
  return (
    (parseFloat(form.maxValue ?? '') || Infinity) >=
      (parseFloat(form.minValue ?? '') || 0) || "Min can't be above max"
  );
}

const rulesByName = {
  minValue: [numeric, minBelowMax],
  maxValue: [numeric, minBelowMax],
};

function validate(
  form: Form,
  name: keyof typeof rulesByName
): (val: string) => boolean | string {
  const rules = rulesByName[name];
  return (val: string) => {
    for (const rule of rules) {
      const result = rule(val, form);
      if (result !== true) {
        return result;
      }
    }
    return true;
  };
}

type Form = {
  maxValue: string | undefined;
  minValue: string | undefined;
};

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
      validate,
    };
  },
});
</script>
