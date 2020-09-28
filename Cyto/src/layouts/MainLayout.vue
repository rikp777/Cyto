<template>
  <div class="flex">
    <top-nav-bar />
    <div class="container">
      <router-view />
    </div>
  </div>
</template>

<script>
import TopNavBar from "@/components/common/menu/TopNavBar";
export default {
name: "MainLayout",
  components: {TopNavBar}
}
</script>

<style scoped>

</style>