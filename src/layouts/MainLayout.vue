<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-toolbar-title> App Finanças </q-toolbar-title>
        <q-chip>
          <q-avatar icon="person" />
          {{ email }}
        </q-chip>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useUserStore } from 'src/stores/UserStore';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'MainLayout',
  setup() {
    const userStore = useUserStore();
    const router = useRouter();

    if (!userStore.userLogado()) {
      router.push('/login');
    }

    return { email: userStore.obterUsuario.email };
  },
});
</script>
