<template>
  <form class="w-full max-w-lg">
    <div class="flex flex-wrap -mx-3 mb-6">
      <slot />
    </div>
  </form>
</template>

<script>
export default {
  name: "FormGrid"
};
</script>

<style scoped></style>
