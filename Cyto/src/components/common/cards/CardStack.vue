<template>
  <div class="max-w-sm rounded overflow-hidden shadow-lg">
    <img v-if="image" class="w-full" :src="image" alt="Image">
    <div class="px-6 py-4">
      <div class="font-bold text-xl mb-2">{{ title }}</div>
      <slot />
    </div>
  </div>
</template>

<script>
export default {
  name: "CardStack",
  props: ["title", "image"]
};
</script>

<style scoped></style>