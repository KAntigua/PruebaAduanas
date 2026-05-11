<script setup>

import { ref, onMounted, watch } from 'vue'
import api from '../../services/api'

const ventas = ref([])
const clientes = ref([])
const productos = ref([])

const venta = ref({
  fecha: '',
  listaProductos: '',
  cantidad: 1,
  total: 0,
  clienteId: ''
})

const productoSeleccionado = ref('')

const editando = ref(false)
const ventaId = ref(0)

const error = ref('')
const success = ref('')

async function obtenerVentas() {

  try {

    const response = await api.get('/ventas')

    ventas.value = response.data

  } catch (error) {

    console.log(error)

  }
}

async function obtenerClientes() {

  try {

    const response = await api.get('/clientes')

    clientes.value = response.data

  } catch (error) {

    console.log(error)

  }
}

async function obtenerProductos() {

  try {

    const response = await api.get('/productos')

    productos.value = response.data

  } catch (error) {

    console.log(error)

  }
}

watch(productoSeleccionado, (nuevoProducto) => {

  const producto = productos.value.find(
    p => p.nombre === nuevoProducto
  )

  if (producto) {

    venta.value.listaProductos = producto.nombre

    venta.value.total =
      producto.precio * venta.value.cantidad

  }

})

watch(
  () => venta.value.cantidad,
  (cantidad) => {

    const producto = productos.value.find(
      p => p.nombre === productoSeleccionado.value
    )

    if (producto) {

      venta.value.total =
        producto.precio * cantidad

    }

  }
)

async function guardarVenta() {

  error.value = ''
  success.value = ''


  if (!venta.value.fecha) {

    error.value = 'La fecha es obligatoria'
    return

  }

  if (!venta.value.listaProductos) {

    error.value = 'Debe seleccionar un producto'
    return

  }

  if (venta.value.cantidad <= 0) {

    error.value = 'La cantidad debe ser mayor que 0'
    return

  }

  if (venta.value.total <= 0) {

    error.value = 'El total debe ser mayor que 0'
    return

  }

  if (!venta.value.clienteId) {

    error.value = 'Debe seleccionar un cliente'
    return

  }

  try {

    if (editando.value) {

      await api.put(
        `/ventas/${ventaId.value}`,
        venta.value
      )

      editando.value = false
      ventaId.value = 0

      success.value = 'Venta actualizada correctamente'

    } else {

      await api.post(
        '/ventas',
        venta.value
      )

      success.value = 'Venta registrada correctamente'

    }

    venta.value = {
      fecha: '',
      listaProductos: '',
      cantidad: 1,
      total: 0,
      clienteId: ''
    }

    productoSeleccionado.value = ''

    obtenerVentas()

  } catch (error) {

    console.log(error)

    error.value = 'Error al guardar la venta'

  }
}

function editar(v) {

  error.value = ''
  success.value = ''

  editando.value = true

  ventaId.value = v.id

  venta.value = {
    fecha: v.fecha?.substring(0, 10),
    listaProductos: v.listaProductos,
    cantidad: v.cantidad,
    total: v.total,
    clienteId: v.clienteId
  }

  productoSeleccionado.value = v.listaProductos

}

async function eliminar(id) {

  try {

    if (!confirm('¿Eliminar venta?')) return

    await api.delete(`/ventas/${id}`)

    success.value = 'Venta eliminada correctamente'

    obtenerVentas()

  } catch (error) {

    console.log(error)

    error.value = 'Error al eliminar la venta'

  }
}

onMounted(() => {

  obtenerVentas()
  obtenerClientes()
  obtenerProductos()

})

</script>

<template>

<div class="container py-4">

  <div class="d-flex justify-content-between align-items-center mb-4">

    <h2 class="fw-bold text-primary">
      Gestión de Ventas
    </h2>

    <router-link
      to="/dashboard"
      class="btn btn-secondary"
    >
      Volver al Dashboard
    </router-link>

  </div>

  <div class="card shadow border-0 mb-4">

    <div class="card-body p-4">

      <h4 class="mb-4">
        {{ editando ? 'Editar Venta' : 'Registrar Venta' }}
      </h4>

      <!-- ALERTAS -->
      <div
        v-if="error"
        class="alert alert-danger"
      >
        {{ error }}
      </div>

      <div
        v-if="success"
        class="alert alert-success"
      >
        {{ success }}
      </div>

      <div class="row">

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Fecha
          </label>

          <input
            v-model="venta.fecha"
            type="date"
            class="form-control"
          />

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Producto
          </label>

          <select
            v-model="productoSeleccionado"
            class="form-select"
          >

            <option value="">
              Seleccione un producto
            </option>

            <option
              v-for="p in productos"
              :key="p.id"
              :value="p.nombre"
            >
              {{ p.nombre }} - ${{ p.precio }}
            </option>

          </select>

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Cantidad
          </label>

          <input
            v-model="venta.cantidad"
            type="number"
            min="1"
            class="form-control"
          />

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Total
          </label>

          <input
            v-model="venta.total"
            type="number"
            class="form-control"
            readonly
          />

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Cliente
          </label>

          <select
            v-model="venta.clienteId"
            class="form-select"
          >

            <option value="">
              Seleccione un cliente
            </option>

            <option
              v-for="c in clientes"
              :key="c.id"
              :value="c.id"
            >
              {{ c.name }}
            </option>

          </select>

        </div>

      </div>

      <button
        @click="guardarVenta"
        class="btn btn-primary"
      >
        {{ editando ? 'Actualizar Venta' : 'Guardar Venta' }}
      </button>

    </div>

  </div>

  <div class="card shadow border-0">

    <div class="card-body p-4">

      <h4 class="mb-4">
        Lista de Ventas
      </h4>

      <div class="table-responsive">

        <table class="table table-hover align-middle">

          <thead class="table-dark">

            <tr>

              <th>Fecha</th>
              <th>Producto</th>
              <th>Cantidad</th>
              <th>Total</th>
              <th>Cliente</th>
              <th width="200">
                Acciones
              </th>

            </tr>

          </thead>

          <tbody>

            <tr
              v-for="v in ventas"
              :key="v.id"
            >

              <td>{{ v.fecha }}</td>

              <td>{{ v.listaProductos }}</td>

              <td>{{ v.cantidad }}</td>

              <td>${{ v.total }}</td>

              <td>
                {{
                  clientes.find(
                    c => c.id === v.clienteId
                  )?.name
                }}
              </td>

              <td>

                <button
                  class="btn btn-warning btn-sm me-2"
                  @click="editar(v)"
                >
                  Editar
                </button>

                <button
                  class="btn btn-danger btn-sm"
                  @click="eliminar(v.id)"
                >
                  Eliminar
                </button>

              </td>

            </tr>

          </tbody>

        </table>

      </div>

    </div>

  </div>

</div>

</template>