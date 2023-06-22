import { defineStore } from 'pinia';
import IUser from 'src/interfaces/IUSer';
import { toRaw } from 'vue';


export const useUserStore = defineStore('userStore', {
  state: () => ({
    User: {} as IUser,
  }),
  getters: {
    obterUsuario: (state) => {
      state.User = obterUsuario()
      return toRaw(state.User);
    },
  },
  actions: {
    logarUsuario(userName: string, email: string) {

      this.User = obterUsuario(userName, email);

      localStorage.setItem('userName', userName);
      localStorage.setItem('email', email);
      localStorage.setItem('user', JSON.stringify(this.User));

      return true;
    }
  },
});


function obterUsuario(userName?: string, email?: string): IUser {
  const userPlataforma = localStorage.getItem('user');

  if (userPlataforma) {
    return JSON.parse(userPlataforma)
  }
  else {
    return {
      email: email!,
      name: userName!,
      idHash: new Date().toISOString(),
      listaDespesas: [],
      listaRendimentos: [],
    }

  }
}


