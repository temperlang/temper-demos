<template>
  <q-page class="row items-start justify-evenly q-pa-xl">
    <q-form>
      <q-card>
        <q-card-section class="q-pb-none">
          <h5>Product Search</h5>
        </q-card-section>
        <q-card-section>
          <q-input name="minPrice" label="Min Price" model-value="" />
          <q-input name="maxPrice" label="Max Price" model-value="" />
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

export default defineComponent({
  name: 'IndexPage',
  components: {
    // ExampleComponent,
  },
  setup() {
    // const todos = ref<Todo[]>([
    //   {
    //     id: 1,
    //     content: 'ct1',
    //   },
    //   {
    //     id: 2,
    //     content: 'ct2',
    //   },
    //   {
    //     id: 3,
    //     content: 'ct3',
    //   },
    //   {
    //     id: 4,
    //     content: 'ct4',
    //   },
    //   {
    //     id: 5,
    //     content: 'ct5',
    //   },
    // ]);
    // const meta = ref<Meta>({
    //   totalCount: 1200,
    // });
    // return { todos, meta };
    return {};
  },
});
</script>
