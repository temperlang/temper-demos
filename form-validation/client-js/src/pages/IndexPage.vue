<template>
  <q-page class="row items-start justify-evenly q-pa-xl">
    <q-form>
      <q-card style="min-width: 300px">
        <q-card-section class="q-pb-none">
          <h5>Product Search</h5>
        </q-card-section>
        <q-card-section>
          <q-input
            name="minPrice"
            label="Min Price"
            v-model="minPrice"
            reactive-rules
            :rules="[numeric]"
          />
          <q-input
            name="maxPrice"
            label="Max Price"
            v-model="maxPrice"
            reactive-rules
            :rules="[numeric]"
          />
        </q-card-section>
        <q-card-section>
          <q-btn name="submit" label="Search" color="primary" />
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

export default defineComponent({
  name: 'IndexPage',
  components: {
    // ExampleComponent,
  },
  setup() {
    return {
      maxPrice: ref<number>(),
      minPrice: ref<number>(),
      numeric,
    };
  },
});
</script>
