<template>
  <div class="wrapper">
    <nav id="sidebar" class="sidebar">
      <div class="sidebar-header">
        <h3>ETZ Lending</h3>
      </div>

      <ul class="list-unstyled components">
        <li class="sidebar-item active">
          <router-link class="sidebar-link" :to="{ name: 'product-index' }">
            Producten
          </router-link>
        </li>
        <li class="sidebar-item">
          <a class="sidebar-link">Klanten</a>
        </li>
        <li class="sidebar-item">
          <a class="sidebar-link">Abonnementen</a>
        </li>
        <li class="sidebar-item">
          <a class="sidebar-link">Reparaties</a>
        </li>
        <li class="sidebar-item">
          <a class="sidebar-link">Uitloggen</a>
        </li>
      </ul>
    </nav>

    <div class="container">
      <main role="main" class="content">
        <slot />
      </main>
    </div>
  </div>
</template>
