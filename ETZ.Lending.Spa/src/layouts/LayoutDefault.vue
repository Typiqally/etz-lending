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
          <router-link class="sidebar-link" :to="{ name: 'lent-product-index' }">
            Uitgeleend
          </router-link>
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
