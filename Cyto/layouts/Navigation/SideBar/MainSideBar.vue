<template>
  <div>
    <DarkModeSwitcher />
    <!-- BEGIN: Side Menu -->
    <nav class="side-nav">
      <!-- BEGIN: Logo -->
      <router-link
        :to="{ name: 'Dashboard' }"
        tag="a"
        class="intro-x flex items-center pl-5 pt-4"
      >
        <img
          alt="Midone Tailwind HTML Admin Template"
          class="w-6"
          src="@/assets/images/logo.svg"
        />
        <span class="hidden xl:block text-white text-lg ml-3">
          Cyto-<span class="font-medium">Playground</span>
        </span>
      </router-link>
      <!-- END: Logo -->

      <div class="side-nav__devider my-6"></div>
      <ul>
        <!-- BEGIN: First Child -->
        <template v-for="(menu, menuKey) in formattedMenu">
          <li
            v-if="menu == 'devider'"
            :key="menuKey"
            class="side-nav__devider my-6"
            @click="linkTo(menu)"
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
              <div class="side-menu__icon">
                <component :is="menu.icon" />
              </div>
              <div class="side-menu__title">
                {{ menu.title }} {{ menu.activeDropdown }}
                <ChevronDownIcon
                  v-if="$h.isset(menu.subMenu)"
                  class="side-menu__sub-icon"
                  :class="{ 'transform rotate-180': menu.activeDropdown }"
                />
              </div>
            </tool-tip>

            <!-- BEGIN: Second Child -->
            <transition @enter="enter" @leave="leave">
              <ul v-if="$h.isset(menu.subMenu) && menu.activeDropdown">
                <li
                  v-for="(subMenu, subMenuKey) in menu.subMenu"
                  :key="subMenuKey"
                >
                  <tool-tip
                    tag="a"
                    :content="subMenu.title"
                    href="javascript:;"
                    class="side-menu"
                    :class="{ 'side-menu--active': subMenu.active }"
                    @click.native="linkTo(subMenu)"
                  >
                    <div class="side-menu__icon">
                      <ActivityIcon />
                    </div>
                    <div class="side-menu__title">
                      {{ subMenu.title }}
                      <ChevronDownIcon
                        v-if="$h.isset(subMenu.subMenu)"
                        class="side-menu__sub-icon"
                        :class="{
                          'transform rotate-180': subMenu.activeDropdown,
                        }"
                      />
                    </div>
                  </tool-tip>
                  <!-- BEGIN: Third Child -->
                  <transition @enter="enter" @leave="leave">
                    <ul
                      v-if="$h.isset(subMenu.subMenu) && subMenu.activeDropdown"
                    >
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
                          <div class="side-menu__icon">
                            <ZapIcon />
                          </div>
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
        </template>
        <!-- END: First Child -->
      </ul>
    </nav>
  </div>
</template>

<script>
import Velocity from 'velocity-animate'
import cash from 'cash-dom'
import DarkModeSwitcher from '~/components/Options/DarkModeSwitcher'
import SideMenuItem from '~/components/Menu/SideMenuItem'
import ToolTip from '~/components/ToolTip/ToolTip'

export default {
  name: 'MainSideBar',
  components: { ToolTip, SideMenuItem, DarkModeSwitcher },

  data() {
    return {
      formattedMenu: [],
    }
  },
  computed: {
    sideMenu() {
      return this.nestedMenu(this.$store.state.SideMenuModule.menuItems)
    },
  },
  watch: {
    $route() {
      this.formattedMenu = this.$h.assign(this.sideMenu)
    },
  },
  mounted() {
    cash('body').removeClass('login').addClass('app')
    this.formattedMenu = this.$h.assign(this.sideMenu)
  },
  methods: {
    nestedMenu(menu) {
      menu.forEach((item, key) => {
        if (typeof item !== 'string') {
          menu[key].active =
            (item.page === this.$route.name ||
              (this.$h.isset(item.subMenu) &&
                this.findActiveMenu(item.subMenu))) &&
            !item.ignore
        }

        if (this.$h.isset(item.subMenu)) {
          menu[key].activeDropdown = this.findActiveMenu(item.subMenu)
          menu[key] = {
            ...item,
            ...this.nestedMenu(item.subMenu),
          }
        }
      })

      return menu
    },
    findActiveMenu(subMenu) {
      let match = false
      subMenu.forEach((item) => {
        if (item.page === this.$route.name && !item.ignore) {
          match = true
        } else if (!match && this.$h.isset(item.subMenu)) {
          match = this.findActiveMenu(item.subMenu)
        }
      })
      return match
    },
    linkTo(menu) {
      console.log('click')
      if (this.$h.isset(menu.subMenu)) {
        menu.activeDropdown = !menu.activeDropdown
        this.$router.push({
          name: menu.page,
        })
      } else {
        console.log(menu.page)
        this.$router.push({
          name: menu.page,
        })
      }
    },
    enter(el, done) {
      Velocity(
        el,
        'slideDown',
        {
          duration: 300,
        },
        {
          complete: done,
        }
      )
    },
    leave(el, done) {
      Velocity(
        el,
        'slideUp',
        {
          duration: 300,
        },
        {
          complete: done,
        }
      )
    },
  },
}
</script>
