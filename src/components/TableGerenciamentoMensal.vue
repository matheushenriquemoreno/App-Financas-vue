<template>
  <ModalDespesa
    :value="abriModal"
    @AoSalvarModal="adicionarDespesa"
    @alterar="(value) => (abriModal = value)"
    :modalCadastroDepesa="abrirModalDepesa"
  />

  <q-table
    flat
    bordered
    title="Treats"
    :rows="despesas"
    :columns="columns"
    row-key="id"
    :filter="filter"
  >
    <template v-slot:top-left>
      <section class="q-gutter-md">
        <q-btn
          class=""
          color="primary"
          label="Adicionar Rendimento"
          @click="() => abrirModal(false)"
        />
        <q-btn
          class=""
          color="primary"
          label="Adicionar Despesa"
          @click="() => abrirModal()"
        />
      </section>
    </template>

    <template v-slot:top-right>
      <q-input
        borderless
        dense
        debounce="300"
        label="Filtro"
        color="primary"
        v-model="filter"
      >
        <template v-slot:append>
          <q-icon name="search" />
        </template>
      </q-input>
    </template>

    <template v-slot:body="props">
      <q-tr :props="props.cols" :key="props.row.id">
        <q-td key="tipo" :props="props">
          <q-icon name="arrow_downward" color="red" size="xs" />
          {{ obterNomeDespesa(props.row.idTipoDespesa) }}
        </q-td>
        <q-td key="tipo" :props="props">
          {{ props.row.descricao }}
        </q-td>
        <q-td key="tipo" :props="props">
          {{
            props.row.valor.toLocaleString('pt-br', {
              style: 'currency',
              currency: 'BRL',
            })
          }}
        </q-td>

        <q-td key="tipo" :props="props">
          <q-btn
            round
            color="deep-orange"
            icon="delete"
            size="sm"
            @click="() => excluirDespesa(props.row.id)"
          />
        </q-td>
      </q-tr>
    </template>
  </q-table>
</template>

<script lang="ts">
import { useDespesaStore } from 'src/stores/despesasStore';
import IDespesa, { TipoDespesas } from 'src/interfaces/AtividadeMensal';
import { ref } from 'vue';
import ModalDespesa from 'src/components/ModalCriacaoDespesa.vue';

const columns: any[] = [
  {
    name: 'tipo',
    required: true,
    label: 'Tipo do Gasto',
    align: 'left',
    field: (row: any) => row.idTipoDespesa,
    format: (val: any) => `${val}`,
    sortable: false,
  },
  { name: 'descricao', label: 'Descricão', field: 'descricao', align: 'left' },
  {
    name: 'valor',
    label: 'Valor',
    field: 'valor',
    align: 'left',
    sortable: true,
    sort: (a: any, b: any) => parseInt(a, 10) - parseInt(b, 10),
  },
  { name: '', label: '', field: '', align: 'left' },
];

export default {
  name: 'TableGerenciamentoMensal ',
  components: { ModalDespesa },
  data() {
    return {
      filter: '',
      abriModal: false,
      abrirModalDepesa: true,
    };
  },
  methods: {
    excluirDespesa: function (id: number) {
      this.despesaStore.removerDespesa(id);
    },
    obterNomeDespesa: function (id: number) {
      return TipoDespesas.find((item) => item.id === id)?.label;
    },
    adicionarDespesa: function (despesa: IDespesa) {
      this.despesaStore.adicionarDespesa(despesa);
      this.abriModal = false;
    },
    abrirModal: function (ModalDespesa = true) {
      this.abriModal = true;
      this.abrirModalDepesa = ModalDespesa;
    },
  },
  setup() {
    const despesaStore = useDespesaStore();
    const despesas = ref(despesaStore.getDespesas);

    return { despesaStore, columns, despesas };
  },
};
</script>