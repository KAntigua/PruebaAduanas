<script setup>
import { ref, onMounted } from 'vue'
import api from '../../services/api'

const productos = ref([])
const carrito = ref([])

const user = JSON.parse(localStorage.getItem('user'))

const venta = ref({
  fecha: '',
  clienteId: user?.id,
  listaProductos: '',
  cantidad: 1,
  total: 0
})

const productoSeleccionado = ref('')

const error = ref('')
const success = ref('')


async function obtenerProductos() {
  try {
    const res = await api.get('/productos')
    productos.value = res.data
  } catch (e) {
    console.log(e)
  }
}


function agregarProducto() {
  const producto = productos.value.find(
    p => p.nombre === productoSeleccionado.value
  )

  if (!producto) return

  const item = carrito.value.find(p => p.id === producto.id)

  if (item) {
    item.cantidad++
  } else {
    carrito.value.push({
      id: producto.id,
      nombre: producto.nombre,
      precio: producto.precio,
      cantidad: 1
    })
  }

  calcularTotal()
}


function quitarProducto(id) {
  carrito.value = carrito.value.filter(p => p.id !== id)
  calcularTotal()
}


function calcularTotal() {
  venta.value.total = carrito.value.reduce(
    (sum, p) => sum + p.precio * p.cantidad,
    0
  )
}

async function comprar() {

  error.value = ''
  success.value = ''

  if (!venta.value.fecha) {
    error.value = 'Selecciona una fecha'
    return
  }

  if (carrito.value.length === 0) {
    error.value = 'El carrito está vacío'
    return
  }

  if (!user?.id) {
    error.value = 'Usuario no autenticado'
    return
  }

  try {

    const payload = {
      fecha: venta.value.fecha,
      clienteId: user.id,
      total: venta.value.total,
      listaProductos: carrito.value
        .map(p => `${p.nombre} x${p.cantidad}`)
        .join(', '),
      cantidad: carrito.value.reduce((a, p) => a + p.cantidad, 0)
    }

    await api.post('/ventas', payload)

    success.value = 'Compra realizada'

    carrito.value = []
    venta.value.fecha = ''
    venta.value.total = 0

  } catch (e) {
    console.log(e)
    error.value = 'Error al realizar la compra'
  }
}

onMounted(() => {
  obtenerProductos()
})
</script>

<template>

<div class="container py-4">

  <h2 class="text-center mb-4">Tienda</h2>

  <div class="row">

    <div class="col-md-7">

      <div class="card shadow p-3 mb-3">

        <h5>Productos</h5>

        <div class="row">

          <div class="col-md-8">

            <select v-model="productoSeleccionado" class="form-select">
              <option value="">Selecciona un producto</option>
              <option
                v-for="p in productos"
                :key="p.id"
                :value="p.nombre"
              >
                {{ p.nombre }} - ${{ p.precio }}
              </option>
            </select>

          </div>

          <div class="col-md-4">
            <button class="btn btn-primary w-100" @click="agregarProducto">
            Agregar
            </button>
          </div>

        </div>

      </div>

      <div class="card shadow p-3">

        <h5>Carrito</h5>

        <ul class="list-group mb-3">

          <li
            v-for="p in carrito"
            :key="p.id"
            class="list-group-item d-flex justify-content-between"
          >
            {{ p.nombre }} (x{{ p.cantidad }})

            <button
              class="btn btn-sm btn-danger"
              @click="quitarProducto(p.id)"
            >
              X
            </button>

          </li>

        </ul>

        <h5>Total: ${{ venta.total }}</h5>

      </div>

    </div>

    <!-- CHECKOUT -->
    <div class="col-md-5">

      <div class="card shadow p-3">

        <h5>Compra</h5>

        <div class="mb-3">
          <label>Fecha</label>
          <input type="date" v-model="venta.fecha" class="form-control">
        </div>

        <div v-if="error" class="alert alert-danger">
          {{ error }}
        </div>

        <div v-if="success" class="alert alert-success">
          {{ success }}
        </div>

        <button class="btn btn-success w-100" @click="comprar">
         Comprar
        </button>

      </div>

    </div>

  </div>

</div>

</template>