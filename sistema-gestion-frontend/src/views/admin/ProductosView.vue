<script setup>

import { ref, onMounted } from 'vue'
import api from '../../services/api'

const productos = ref([])

const nombre = ref('')
const descripcion = ref('')
const precio = ref(0)
const stock = ref(0)

const editando = ref(false)
const productoId = ref(0)

const error = ref('')
const success = ref('')

const cargarProductos = async () => {

    try {

        const response = await api.get('/productos')

        productos.value = response.data

    } catch (err) {

        console.log(err)

        error.value = 'Error al cargar productos'

    }

}

const guardarProducto = async () => {

    error.value = ''
    success.value = ''

    if (!nombre.value.trim()) {

        error.value = 'El nombre es obligatorio'
        return

    }

    if (!descripcion.value.trim()) {

        error.value = 'La descripción es obligatoria'
        return

    }

    if (precio.value <= 0) {

        error.value = 'El precio debe ser mayor que 0'
        return

    }

    if (stock.value < 0) {

        error.value = 'El stock no puede ser negativo'
        return

    }

    const producto = {

        nombre: nombre.value,
        descripcion: descripcion.value,
        precio: precio.value,
        stock: stock.value

    }

    try {

        if (editando.value) {

            await api.put(
                `/productos/${productoId.value}`,
                producto
            )

            success.value = 'Producto actualizado correctamente'

        } else {

            await api.post(
                '/productos',
                producto
            )

            success.value = 'Producto registrado correctamente'

        }

        limpiarFormulario()
        cargarProductos()

    } catch (err) {

        console.log(err)

        error.value = 'Ocurrió un error al guardar el producto'

    }

}

const editarProducto = (producto) => {

    error.value = ''
    success.value = ''

    editando.value = true
    productoId.value = producto.id

    nombre.value = producto.nombre
    descripcion.value = producto.descripcion
    precio.value = producto.precio
    stock.value = producto.stock

}

const eliminarProducto = async (id) => {

    try {

        if (!confirm('¿Eliminar producto?')) return

        await api.delete(`/productos/${id}`)

        success.value = 'Producto eliminado correctamente'

        cargarProductos()

    } catch (err) {

        console.log(err)

        error.value = 'Error al eliminar el producto'

    }

}

const limpiarFormulario = () => {

    nombre.value = ''
    descripcion.value = ''
    precio.value = 0
    stock.value = 0

    editando.value = false
    productoId.value = 0

}

onMounted(() => {

    cargarProductos()

})

</script>

<template>

<div class="container mt-5">

    <!-- BARRA SUPERIOR -->
    <div class="d-flex justify-content-between align-items-center mb-4">

        <h2 class="fw-bold text-success m-0">
            Gestión de Productos
        </h2>

        <router-link
            to="/dashboard"
            class="btn btn-secondary"
        >
            Volver al Dashboard
        </router-link>

    </div>

    <div class="card shadow border-0 p-4 mb-4">

        <h3 class="mb-4">
            {{ editando ? 'Editar Producto' : 'Registrar Producto' }}
        </h3>

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
                    Nombre
                </label>

                <input
                    v-model="nombre"
                    type="text"
                    class="form-control"
                    placeholder="Ingrese el nombre"
                >

            </div>

            <div class="col-md-6 mb-3">

                <label class="form-label">
                    Descripción
                </label>

                <input
                    v-model="descripcion"
                    type="text"
                    class="form-control"
                    placeholder="Ingrese la descripción"
                >

            </div>

            <div class="col-md-6 mb-3">

                <label class="form-label">
                    Precio
                </label>

                <input
                    v-model="precio"
                    type="number"
                    class="form-control"
                    placeholder="Ingrese el precio"
                >

            </div>

            <div class="col-md-6 mb-3">

                <label class="form-label">
                    Stock
                </label>

                <input
                    v-model="stock"
                    type="number"
                    class="form-control"
                    placeholder="Ingrese el stock"
                >

            </div>

        </div>

        <div class="d-flex gap-2">

            <button
                @click="guardarProducto"
                class="btn btn-success"
            >
                {{ editando ? 'Actualizar' : 'Guardar' }}
            </button>

            <button
                @click="limpiarFormulario"
                class="btn btn-secondary"
            >
                Limpiar
            </button>

        </div>

    </div>

    <div class="card shadow border-0 p-4">

        <h3 class="mb-4">
            Lista de Productos
        </h3>

        <div class="table-responsive">

            <table class="table table-hover table-bordered align-middle">

                <thead class="table-dark">

                    <tr>

                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Precio</th>
                        <th>Stock</th>
                        <th width="180">
                            Acciones
                        </th>

                    </tr>

                </thead>

                <tbody>

                    <tr
                        v-for="producto in productos"
                        :key="producto.id"
                    >

                        <td>{{ producto.id }}</td>
                        <td>{{ producto.nombre }}</td>
                        <td>{{ producto.descripcion }}</td>
                        <td>${{ producto.precio }}</td>
                        <td>{{ producto.stock }}</td>

                        <td>

                            <button
                                @click="editarProducto(producto)"
                                class="btn btn-primary btn-sm me-2"
                            >
                                Editar
                            </button>

                            <button
                                @click="eliminarProducto(producto.id)"
                                class="btn btn-danger btn-sm"
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

</template>