<script setup>

import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

const username = ref('')
const password = ref('')
const confirmPassword = ref('')

const error = ref('')
const success = ref('')

const router = useRouter()

const register = async () => {

    error.value = ''
    success.value = ''

    // VALIDACIONES

    if (!username.value.trim()) {

        error.value = 'El usuario es obligatorio'
        return

    }

    if (!password.value.trim()) {

        error.value = 'La contraseña es obligatoria'
        return

    }

    if (!confirmPassword.value.trim()) {

        error.value =
            'Debe confirmar la contraseña'

        return

    }

    if (password.value.length < 4) {

        error.value =
            'La contraseña debe tener mínimo 4 caracteres'

        return

    }

    if (password.value !== confirmPassword.value) {

        error.value =
            'Las contraseñas no coinciden'

        return

    }

    try {

        await api.post('/auth/registro', {

            username: username.value,
            password: password.value

        })

        success.value =
            'Usuario creado correctamente'

        username.value = ''
        password.value = ''
        confirmPassword.value = ''

        setTimeout(() => {

            router.push('/')

        }, 1500)

    } catch {

        error.value =
            'Error al crear el usuario'

    }

}

const irLogin = () => {

    router.push('/')

}

</script>

<template>

<div
    class="container-fluid bg-light min-vh-100 d-flex align-items-center justify-content-center"
>

    <div
        class="card shadow-lg border-0 p-4"
        style="width: 100%; max-width: 430px; border-radius: 16px;"
    >

        <div class="text-center mb-4">

            <h2 class="fw-bold text-primary">
                Sistema de Gestión
            </h2>

            <p class="text-muted mb-0">
                Crear una nueva cuenta
            </p>

        </div>

        <!-- ERROR -->
        <div
            v-if="error"
            class="alert alert-danger text-center py-2"
        >
            {{ error }}
        </div>

        <!-- SUCCESS -->
        <div
            v-if="success"
            class="alert alert-success text-center py-2"
        >
            {{ success }}
        </div>

        <!-- USUARIO -->
        <div class="mb-3">

            <label class="form-label fw-semibold">
                Usuario
            </label>

            <input
                v-model="username"
                type="text"
                class="form-control form-control-lg"
                placeholder="Ingrese un usuario"
            >

        </div>

        <!-- PASSWORD -->
        <div class="mb-3">

            <label class="form-label fw-semibold">
                Contraseña
            </label>

            <input
                v-model="password"
                type="password"
                class="form-control form-control-lg"
                placeholder="Ingrese una contraseña"
            >

        </div>

        <!-- CONFIRM PASSWORD -->
        <div class="mb-4">

            <label class="form-label fw-semibold">
                Confirmar contraseña
            </label>

            <input
                v-model="confirmPassword"
                type="password"
                class="form-control form-control-lg"
                placeholder="Repita la contraseña"
            >

        </div>

        <!-- BOTON -->
        <button
            @click="register"
            class="btn btn-primary btn-lg w-100 mb-3"
        >
            Crear cuenta
        </button>

        <!-- VOLVER -->
        <button
            @click="irLogin"
            class="btn btn-outline-secondary w-100"
        >
            Volver al login
        </button>

    </div>

</div>

</template>