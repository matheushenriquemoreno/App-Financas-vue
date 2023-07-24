<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-toolbar-title> App Finanças </q-toolbar-title>

        <q-btn-dropdown icon="person" class="white" rounded :label="email">
          <q-list>
            <q-item clickable v-close-popup @click="() => console.log('sair')">
              <q-item-section avatar>
                <q-avatar icon="logout" color="dark" text-color="white" />
              </q-item-section>
              <q-item-section>
                <q-item-label>Sair</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useUserStore } from 'src/stores/UserStore';
import { useRouter } from 'vue-router';
import { userLogado } from 'src/services/ServiceUSer';

export default defineComponent({
  name: 'MainLayout',
  setup() {
    const userStore = useUserStore();
    const router = useRouter();

    if (!userLogado()) {
      router.push('/login');
    }

    return { email: userStore.obterUsuario.email };
  },
});
</script>
