import Vue from 'vue'
import VueTippy, {TippyComponent} from 'vue-tippy'

import 'tippy.js/themes/light.css'

console.log('Tippy Component')

Vue.use(VueTippy)
Vue.component('tippy', TippyComponent)
