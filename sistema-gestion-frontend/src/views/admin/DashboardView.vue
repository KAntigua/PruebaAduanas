<script setup>

import { useRouter } from 'vue-router'

const router = useRouter()

const logout = () => {

    localStorage.removeItem('token')

    router.push('/')

}

</script>

<template>

<div class="container-fluid bg-light min-vh-100 p-0">

    <nav class="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm px-4 py-3">

        <div class="container-fluid">

            <a class="navbar-brand fw-bold fs-3" href="#">
                Sistema de Gestion
            </a>

            <button
                class="navbar-toggler"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#navbarNav"
            >
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarNav">

                <ul class="navbar-nav ms-auto align-items-lg-center gap-2">

                    <li class="nav-item">
                        <router-link
                            to="/clientes"
                            class="btn btn-outline-light"
                        >
                            Clientes
                        </router-link>
                    </li>

                    <li class="nav-item">
                        <router-link
                            to="/productos"
                            class="btn btn-outline-light"
                        >
                            Productos
                        </router-link>
                    </li>

                    <li class="nav-item">
                        <router-link
                            to="/ventas"
                            class="btn btn-outline-light"
                        >
                            Ventas
                        </router-link>
                    </li>

                    <li class="nav-item">
                        <button
                            @click="logout"
                            class="btn btn-danger"
                        >
                            Cerrar sesión
                        </button>
                    </li>

                </ul>

            </div>

        </div>

    </nav>

    <div class="container py-5">

        <div class="text-center mb-5">

            <h1 class="fw-bold display-4 text-primary">
                Panel Principal
            </h1>

            <p class="text-muted fs-5">
                Administra clientes, productos y ventas desde un solo lugar.
            </p>

        </div>

        <div class="row g-4">

            <div class="col-12 col-md-6 col-xl-4">

                <div class="card border-0 shadow-lg h-100 p-4 rounded-4">

                    <div class="card-body text-center">

                        <h2 class="fw-bold text-primary mb-3">
                            Clientes
                        </h2>

                        <p class="text-muted mb-4 fs-5">
                            Gestiona y administra los clientes registrados.
                        </p>

                        <router-link
                            to="/clientes"
                            class="btn btn-primary btn-lg w-100"
                        >
                            Ir a Clientes
                        </router-link>

                    </div>

                </div>

            </div>

            <div class="col-12 col-md-6 col-xl-4">

                <div class="card border-0 shadow-lg h-100 p-4 rounded-4">

                    <div class="card-body text-center">

                        <h2 class="fw-bold text-success mb-3">
                            Productos
                        </h2>

                        <p class="text-muted mb-4 fs-5">
                            Administra el inventario y los productos disponibles.
                        </p>

                        <router-link
                            to="/productos"
                            class="btn btn-success btn-lg w-100"
                        >
                            Ir a Productos
                        </router-link>

                    </div>

                </div>

            </div>

            <div class="col-12 col-md-6 col-xl-4 mx-md-auto mx-xl-0">

                <div class="card border-0 shadow-lg h-100 p-4 rounded-4">

                    <div class="card-body text-center">

                        <h2 class="fw-bold text-danger mb-3">
                            Ventas
                        </h2>

                        <p class="text-muted mb-4 fs-5">
                            Consulta y registra las ventas realizadas.
                        </p>

                        <router-link
                            to="/ventas"
                            class="btn btn-danger btn-lg w-100"
                        >
                            Ir a Ventas
                        </router-link>

                    </div>

                </div>

            </div>

        </div>

    </div>

</div>

</template>

