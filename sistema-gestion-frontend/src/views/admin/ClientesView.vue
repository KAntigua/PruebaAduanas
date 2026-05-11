<script setup>
import { ref, onMounted } from 'vue'
import api from '../../services/api'

const clientes = ref([])

const cliente = ref({
  name: '',
  correo: '',
  numero: ''
})

const editando = ref(false)
const clienteId = ref(0)

const error = ref('')
const success = ref('')

async function obtenerClientes() {

  try {

    const response = await api.get('/clientes')

    clientes.value = response.data

  } catch (error) {

    console.log(error)

  }
}

async function guardarCliente() {

  error.value = ''
  success.value = ''

  if (!cliente.value.name.trim()) {

    error.value = 'El nombre es obligatorio'
    return

  }

  if (!cliente.value.correo.trim()) {

    error.value = 'El correo es obligatorio'
    return

  }

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

  if (!emailRegex.test(cliente.value.correo)) {

    error.value = 'Ingrese un correo valido'
    return

  }

  if (!cliente.value.numero.trim()) {

    error.value = 'El numero es obligatorio'
    return

  }

  const numeroRegex = /^[0-9]+$/

  if (!numeroRegex.test(cliente.value.numero)) {

    error.value = 'El numero solo debe contener digitos'
    return

  }

  try {

    if (editando.value) {

      await api.put(
        `/clientes/${clienteId.value}`,
        cliente.value
      )

      editando.value = false
      clienteId.value = 0

      success.value = 'Cliente actualizado correctamente'

    } else {

      await api.post(
        '/clientes',
        cliente.value
      )

      success.value = 'Cliente registrado correctamente'

    }

    // LIMPIAR FORMULARIO
    cliente.value = {
      name: '',
      correo: '',
      numero: ''
    }

    obtenerClientes()

  } catch (err) {

    console.log(err)

    error.value = 'Ocurrio un error al guardar el cliente'

  }
}

function editar(clienteEditar) {

  error.value = ''
  success.value = ''

  editando.value = true

  clienteId.value = clienteEditar.id

  cliente.value = {
    name: clienteEditar.name,
    correo: clienteEditar.correo,
    numero: clienteEditar.numero
  }
}

async function eliminar(id) {

  try {

    if (!confirm('¿Eliminar cliente?')) return

    await api.delete(`/clientes/${id}`)

    success.value = 'Cliente eliminado correctamente'

    obtenerClientes()

  } catch (error) {

    console.log(error)

    error.value = 'Error al eliminar el cliente'

  }
}

onMounted(() => {

  obtenerClientes()

})
</script>

<template>

<div class="container py-4">

  <div class="d-flex justify-content-between align-items-center mb-4">

    <h2 class="fw-bold text-primary m-0">
      Gestión de Clientes
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
        {{ editando ? 'Editar Cliente' : 'Registrar Cliente' }}
      </h4>

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
            Nombre
          </label>

          <input
            v-model="cliente.name"
            type="text"
            class="form-control"
            placeholder="Ingrese el nombre"
          />

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Correo
          </label>

          <input
            v-model="cliente.correo"
            type="email"
            class="form-control"
            placeholder="Ingrese el correo"
          />

        </div>

        <div class="col-md-6 mb-3">

          <label class="form-label">
            Número
          </label>

          <input
            v-model="cliente.numero"
            type="text"
            class="form-control"
            placeholder="Ingrese el número"
          />

        </div>

      </div>

      <button
        @click="guardarCliente"
        class="btn btn-primary"
      >
        {{ editando ? 'Actualizar Cliente' : 'Guardar Cliente' }}
      </button>

    </div>

  </div>

  <div class="card shadow border-0">

    <div class="card-body p-4">

      <h4 class="mb-4">
        Lista de Clientes
      </h4>

      <div class="table-responsive">

        <table class="table table-hover align-middle">

          <thead class="table-dark">

            <tr>
              <th>Nombre</th>
              <th>Correo</th>
              <th>Número</th>
              <th width="220">Acciones</th>
            </tr>

          </thead>

          <tbody>

            <tr
              v-for="c in clientes"
              :key="c.id"
            >

              <td>{{ c.name }}</td>
              <td>{{ c.correo }}</td>
              <td>{{ c.numero }}</td>

              <td>

                <button
                  class="btn btn-warning btn-sm me-2"
                  @click="editar(c)"
                >
                  Editar
                </button>

                <button
                  class="btn btn-danger btn-sm"
                  @click="eliminar(c.id)"
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