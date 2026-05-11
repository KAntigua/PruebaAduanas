<script setup>

import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

const username = ref('')
const password = ref('')
const error = ref('')

const router = useRouter()

const login = async () => {

    error.value = ''

    if (!username.value.trim()) {

        error.value = 'El usuario es obligatorio'
        return

    }

    if (!password.value.trim()) {

        error.value = 'La contraseña es obligatoria'
        return

    }

    try {

        const response = await api.post('/auth/login', {

            username: username.value,
            password: password.value

        })

        // GUARDAR TOKEN

        localStorage.setItem(
            'token',
            response.data.token
        )

        // GUARDAR ROL

        localStorage.setItem(
            'rol',
            response.data.rol
        )

        // REDIRECCION SEGUN ROL

        if (response.data.rol === 'Admin') {

            router.push('/dashboard')

        } else {

            router.push('/tienda')

        }

    } catch (err) {

        console.log(err.response?.data)

        error.value =
            err.response?.data ||
            'Usuario o contraseña incorrectos'

    }

}

const irRegistro = () => {
    router.push('/register')
}

</script>

<template>

<div class="container-fluid bg-light min-vh-100 d-flex align-items-center justify-content-center">

    <div class="card shadow border-0 p-4" style="width: 100%; max-width: 420px; border-radius: 12px;">

        <div class="text-center mb-4">

            <h3 class="fw-bold text-primary">Inicio de Sesión</h3>
            <p class="text-muted mb-0">Accede al sistema</p>

        </div>

        <div v-if="error" class="alert alert-danger py-2 text-center">
            {{ error }}
        </div>

        <div class="mb-3">

            <label class="form-label">Usuario</label>

            <input
                v-model="username"
                type="text"
                class="form-control"
                placeholder="Ingrese su usuario"
            >

        </div>

        <div class="mb-3">

            <label class="form-label">Contraseña</label>

            <input
                v-model="password"
                type="password"
                class="form-control"
                placeholder="Ingrese su contraseña"
            >

        </div>

        <button
            @click="login"
            class="btn btn-primary w-100 mb-3"
        >
            Iniciar sesión
        </button>

        <button
            @click="irRegistro"
            class="btn btn-outline-secondary w-100"
        >
            Crear cuenta
        </button>

    </div>

</div>

</template>