import { defineStore } from 'pinia';
import IDespesa from 'src/interfaces/AtividadeMensal';


export const useDespesaStore = defineStore('despesaStore', {
  state: () => ({
    despesas: [] as Array<IDespesa>,
  }),
  getters: {
    getDespesas: (state) => state.despesas,
  },
  actions: {
    adicionarDespesa(despesa: IDespesa) {
      this.despesas.push(despesa);
    },
    removerDespesa(id: number){
      const index = this.despesas.findIndex(depesa => id === depesa.id);
      console.log(index)
      this.despesas.splice(index, 1);
    }
  },
});
