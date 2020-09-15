<template>
  <div class="flex">
    <MainSideBar />
    <div class="content">
      <MainNavBar />
      <router-view />
    </div>
  </div>
</template>
<script>
import MainNavBar from '@/layouts/Navigation/Navbar/MainNavBar'
import MainSideBar from '@/layouts/Navigation/SideBar/MainSideBar'

export default {
  components: { MainSideBar, MainNavBar },
}
</script>
