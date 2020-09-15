<template>
  <div>
    <div v-for="(menu, menuKey) in menuItems" :key="menuKey">
      <li
        v-if="menu == 'devider'"
        :key="menuKey"
        class="side-nav__devider my-6"
      ></li>
      <li v-else :key="menuKey">
        <tool-tip
          tag="a"
          :content="menu.title"
          href="javascript:;"
          class="side-menu"
          :class="{
            'side-menu--active': menu.active,
            'side-menu--open': menu.activeDropdown,
          }"
          @click.native="linkTo(menu)"
        >
          <div class="side-menu__title">{{ menu }}{{ menu.subMenu }}</div>
        </tool-tip>

        <!-- BEGIN: Second Child -->
        <transition @enter="enter" @leave="leave">
          <ul v-if="menu.subMenu && menu.activeDropdown">
            <li v-for="(subMenu, subMenuKey) in menu.subMenu" :key="subMenuKey">
              <tool-tip
                tag="a"
                :content="subMenu.title"
                href="javascript:;"
                class="side-menu"
                :class="{ 'side-menu--active': subMenu.active }"
                @click.native="linkTo(subMenu)"
              >
                <div class="side-menu__title">
                  {{ subMenu.title }}
                </div>
              </tool-tip>
              <!-- BEGIN: Third Child -->
              <transition @enter="enter" @leave="leave">
                <ul v-if="subMenu.subMenu && subMenu.activeDropdown">
                  <li
                    v-for="(lastSubMenu, lastSubMenuKey) in subMenu.subMenu"
                    :key="lastSubMenuKey"
                  >
                    <tool-tip
                      tag="a"
                      :content="lastSubMenu.title"
                      href="javascript:;"
                      class="side-menu"
                      :class="{ 'side-menu--active': lastSubMenu.active }"
                      @click.native="linkTo(lastSubMenu)"
                    >
                      <div class="side-menu__title">
                        {{ lastSubMenu.title }}
                      </div>
                    </tool-tip>
                  </li>
                </ul>
              </transition>
              <!-- END: Third Child -->
            </li>
          </ul>
        </transition>
        <!-- END: Second Child -->
      </li>
    </div>
  </div>
</template>
<script>
import Velocity from 'velocity-animate'
import { mapGetters } from 'vuex'
import ToolTip from '~/components/ToolTip/ToolTip'

export default {
  name: 'SideMenuItem',
  // eslint-disable-next-line vue/no-unused-components
  components: { ToolTip },
  computed: {
    ...mapGetters('SideMenuModule', {
      menuItems: 'menuItems',
    }),
  },
  // watch: {
  //   $route() {
  //     this.menuItems = JSON.parse(JSON.stringify(this.sideMenu))
  //   },
  // },
  mounted() {
    // console.log(this.menuItems)
  },
  methods: {
    // nestedMenu(menu) {
    //   menu.forEach((item, key) => {
    //     if (typeof item !== 'string') {
    //       menu[key].active =
    //         (item.pageName === this.$route.name ||
    //           (this.$h.isset(item.subMenu) &&
    //             this.findActiveMenu(item.subMenu))) &&
    //         !item.ignore
    //     }
    //
    //     if (this.$h.isset(item.subMenu)) {
    //       menu[key].activeDropdown = this.findActiveMenu(item.subMenu)
    //       menu[key] = {
    //         ...item,
    //         ...this.nestedMenu(item.subMenu),
    //       }
    //     }
    //   })
    //
    //   return menu
    // },
    activeSubMenu(subMenu) {
      let match = false
      subMenu.forEach((item) => {
        if (item.pageNumber === this.$route.name && !item.ignore) {
          match = true
        } else if (!match && item.subMenu) {
          match = this.activeSubMenu(item.subMenu)
        }
      })
      return match
    },
    linkTo(menu) {
      if (menu.subMenu) {
        menu.activeDropDown = !menu.activeDropDown
      } else {
        this.$router.push({
          name: menu.pageName,
        })
      }
    },
    enter(el, done) {
      Velocity(el, 'slideDown', { duration: 300 }, { complete: done })
    },
    leave(el, done) {
      Velocity(el, 'slideUp', { duration: 300 }, { complete: done })
    },
  },
}
</script>
