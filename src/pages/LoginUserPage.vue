<!-- eslint-disable vue/valid-template-root -->
<template>
  <div class="row justify-center">
    <div class="form-container">
      <p class="title">Login</p>

      <q-form class="q-gutter-y-lg" @submit.prevent="login">
        <q-input
          outlined
          v-model="username"
          label-color="white"
          label="Nome de usuario"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Campo obrigatorio']"
        />

        <q-input
          filled
          v-model="email"
          type="email"
          label-color="white"
          label="E-mail"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Campo obrigatorio']"
        />

        <q-btn label="Entrar" type="submit" class="sign" />
      </q-form>
    </div>
  </div>
</template>
    
    
  <script lang="ts">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from 'src/stores/UserStore';

export default defineComponent({
  name: 'LoginPage',
  data() {
    return {
      username: '',
      email: '',
    };
  },
  methods: {
    login: function () {
      if (this.userStore.logarUsuario(this.username, this.email)) {
        this.router.push({ name: 'Home' });
      }
    },
  },
  setup() {
    const router = useRouter();
    const userStore = useUserStore();
    return { userStore, router };
  },
});
</script>
    
    
    <style scoped>
.form-container {
  margin-top: 100px;
  width: 400px;
  border-radius: 0.75rem;
  background-color: grey;
  padding: 2rem;
  color: rgba(243, 244, 246, 1);
}

.title {
  text-align: center;
  font-size: 1.5rem;
  line-height: 2rem;
  font-weight: 700;
  margin-bottom: 2rem;
}

.signup a {
  color: rgb(199, 202, 207);
  text-decoration: none;
  font-size: 14px;
}

.forgot a:hover,
.signup a:hover {
  text-decoration: underline rgba(167, 139, 250, 1);
}

.sign {
  display: block;
  width: 100%;
  background-color: #29272e;
  padding: 0.75rem;
  text-align: center;
  color: rgb(208, 210, 212);
  border: none;
  border-radius: 0.375rem;
  font-weight: 600;
}
</style>