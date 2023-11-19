import { defineStore } from 'pinia';
import IDespesa from 'src/interfaces/IDespesa';
import IRendimento from 'src/interfaces/IRendimento';
import UserService, { userLogado } from 'src/services/ServiceUSer';

function serviceUser() {
  return new UserService();
}

export const useDespesaStore = defineStore('despesaStore', {
  state: () => ({
    despesas: [] as Array<IDespesa>,
    rendimentos: [] as Array<IRendimento>,
  }),
  getters: {
    getDespesas: (state) => {
      if (userLogado()) {
        console.log(serviceUser().usuario)

        state.despesas = serviceUser().usuario.listaDespesas
      }
      return state.despesas
    },
    getRendimentos: (state) => {
      if (userLogado()) {
        state.rendimentos = serviceUser().usuario.listaRendimentos
      }
      return state.rendimentos
    }
  },
  actions: {
    adicionarDespesa(despesa: IDespesa) {
      this.despesas.push(despesa);
      serviceUser().adicionarDespesaUsuario(despesa);
    },
    removerDespesa(id: string) {
      const index = this.despesas.findIndex(depesa => id === depesa.id);
      this.despesas.splice(index, 1);
      serviceUser().atualizarDepesas(this.despesas);
    },
    adicionarRendimento(rendimento: IRendimento) {
      this.rendimentos.push(rendimento);
      console.log(this.rendimentos);
      serviceUser().adicionarRendimentoUsuario(rendimento);
    },
    removerRendimento(id: string) {
      const index = this.rendimentos.findIndex(rendimento => id === rendimento.id);
      this.rendimentos.splice(index, 1);
      serviceUser().atualizarRendimento(this.rendimentos);
    },
    atualizarValor(valor: number, id: string, type: 'Despesa' | 'Rendimento') {
      if (valor === undefined || valor === null) {
        valor = 0;
      }

      if (type === 'Despesa') {
        const index = this.despesas.findIndex(despesa => id === despesa.id);
        this.despesas[index].valor = valor;
        serviceUser().atualizarDepesas(this.despesas);
      }
      else {
        const index = this.rendimentos.findIndex(rendimento => id === rendimento.id);
        this.rendimentos[index].valor = valor;
        serviceUser().atualizarRendimento(this.rendimentos);
      }
    },
    atualizarDescricao(id: string, descricao: string, type: 'Despesa' | 'Rendimento') {

      if (type === 'Despesa') {
        const index = this.despesas.findIndex(despesa => id === despesa.id);
        this.despesas[index].descricao = descricao;
        serviceUser().atualizarDepesas(this.despesas);
      }
      else {
        const index = this.rendimentos.findIndex(rendimento => id === rendimento.id);
        this.rendimentos[index].descricao = descricao;
        serviceUser().atualizarRendimento(this.rendimentos);
      }
    }



  },
});
