<template>
  <card-stack title="Create Experiment">
    <form-grid>
      <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
        <label
            class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
            for="grid-first-name"
        >
          First Name
        </label>
        <input
            class="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            id="grid-first-name"
            type="text"
            placeholder="Jane"
        />
        <p class="text-red-500 text-xs italic">Please fill out this field.</p>
      </div>
    </form-grid>
    <button
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
    >
      Button
    </button>
  </card-stack>
</template>

<script>
import FormGrid from "../common/forms/FormGrid";
import CardStack from "../common/cards/CardStack";
export default {
  name: "CreateExperiment",
  components: { CardStack, FormGrid }
};
</script>