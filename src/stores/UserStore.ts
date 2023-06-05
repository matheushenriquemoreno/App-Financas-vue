import { defineStore } from 'pinia';
import IUser from 'src/interfaces/IUSer';


export const useUserStore = defineStore('userStore', {
  state: () => ({
    User: {} as IUser,
  }),
  getters: {
     obterUsuario: (state) => {
        if(state.User == null || state.User == undefined || state.User.email === '') {
            const userName = localStorage.getItem('userName')
            const email = localStorage.getItem('email')
    
            state.User = {
                email: email ?? '',
                id: 1,
                listaDespesas: [],
                name: userName ?? '',
            }
        }

        return state.User;
    },
  },
  actions: {
    userLogado() {
       const userName = localStorage.getItem('userName')
       const email = localStorage.getItem('email')

       if(userName && email){
        return true;
       }
      return false;
    },
    logarUsuario(userName: string, email:string) {

        console.log(userName, email);

        localStorage.setItem('userName', userName);
        localStorage.setItem('email', email);
        
        this.User = {
            email: email,
            id: 1,
            listaDespesas: [],
            name: userName,
        }
        return true;
    }
  },
});
