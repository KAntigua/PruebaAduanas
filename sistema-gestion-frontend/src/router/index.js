import { createRouter, createWebHistory } from 'vue-router'

// LOGIN
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/Register.vue'

// ADMIN
import DashboardView from '../views/admin/DashboardView.vue'
import ProductosView from '../views/admin/ProductosView.vue'
import ClientesView from '../views/admin/ClientesView.vue'
import VentasView from '../views/admin/VentasView.vue'

// CLIENTE
import TiendaView from '../views/cliente/TiendaView.vue'

const routes = [

  // LOGIN
  {
    path: '/',
    component: LoginView
  },

  {
    path: '/register',
    component: RegisterView
  },

  // ADMIN
  {
    path: '/dashboard',
    component: DashboardView
  },

  {
    path: '/productos',
    component: ProductosView
  },

  {
    path: '/clientes',
    component: ClientesView
  },

  {
    path: '/ventas',
    component: VentasView
  },

  // CLIENTE
  {
    path: '/tienda',
    component: TiendaView
  }

]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router