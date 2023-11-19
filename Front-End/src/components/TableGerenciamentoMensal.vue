<template>
  <ModalGerenciamento
    :value="abriModal"
    @AoSalvarModal="adicionarAtividade"
    @alterar="(value) => (abriModal = value)"
    :modalCadastroDepesa="tabelaDespesa"
  />

  <q-table
    flat
    bordered
    :rows="dados"
    :columns="columns"
    row-key="id"
    :pagination="initialPagination"
    :filter="filter"
  >
    <template v-slot:top-left>
      <section class="q-gutter-md">
        <q-btn
          class=""
          color="primary"
          :label="tabelaDespesa ? 'Adicionar Despesa' : 'Adicionar Rendimento'"
          @click="() => (abriModal = true)"
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
          <q-icon
            :name="tabelaDespesa ? 'arrow_downward' : 'arrow_upward'"
            :color="tabelaDespesa ? 'red' : 'green'"
            size="xs"
          />
          {{ obterTipoAtividade(props.row) }}

          <q-popup-edit
            v-model="props.row.idTipoDespesa"
            buttons
            label-set="Salvar"
            label-cancel="Fechar"
            :validate="(val) => val !== ''"
            v-slot="scope"
            @save="(opcao) => console.log(opcao)"
          >
            <q-select
              dense
              autofocus
              v-model="scope.value"
              :options="tabelaDespesa ? TipoDespesas : TipoRendimento"
              label="Standard"
              @keyup.enter="scope.set"
              :rules="[(val) => scope.validate(val) || 'Dite um valor valido!']"
              emit-value
              map-options
            />
          </q-popup-edit>
        </q-td>
        <q-td key="tipo" :props="props">
          {{ props.row.descricao }}

          <q-popup-edit
            v-model="props.row.descricao"
            buttons
            label-set="Salvar"
            label-cancel="Fechar"
            :validate="(val) => val !== ''"
            @save="(value) => alterarDescricao(props.row.id, value)"
            v-slot="scope"
          >
            <q-input
              v-model.trim="scope.value"
              dense
              autofocus
              :rules="[(val) => scope.validate(val) || 'Dite um valor valido!']"
              @keyup.enter="scope.set"
            />
          </q-popup-edit>
        </q-td>
        <q-td key="tipo" :props="props">
          <ValorPadraoBR :valor="props.row.valor" />

          <q-popup-edit
            v-model.number="props.row.valor"
            buttons
            label-set="Salvar"
            label-cancel="Fechar"
            @save="(value) => alterarValor(props.row.id, value)"
            :validate="(val) => val !== ''"
            v-slot="scope"
          >
            <q-input
              type="number"
              v-model.number="scope.value"
              step="0.01"
              dense
              autofocus
              :rules="[(val) => scope.validate(val) || 'Dite um valor valido!']"
              @keyup.enter="scope.set"
            />
          </q-popup-edit>
        </q-td>

        <q-td key="tipo" :props="props">
          <q-btn
            round
            color="deep-orange"
            icon="delete"
            size="sm"
            @click="() => excluirAtividade(props.row.id)"
          />
        </q-td>
      </q-tr>
    </template>
  </q-table>
</template>

<script lang="ts">
import { useDespesaStore } from 'src/stores/gerenciamentoMensalStore';
import IDespesa, { TipoDespesas } from 'src/interfaces/IDespesa';
import { ref } from 'vue';
import ModalGerenciamento from 'src/components/ModalCriacaoDespesaERendimento.vue';
import ValorPadraoBR from './ValorPadraoBR.vue';
import IRendimento, { TipoRendimento } from 'src/interfaces/IRendimento';

export type tipoTablea = 'IRendimento' | 'Despesa';

export default {
  name: 'TableGerenciamentoMensal ',
  components: { ModalGerenciamento, ValorPadraoBR },
  props: {
    itemName: {
      type: String,
      required: true,
    },
    TableTipe: {
      type: Object as () => tipoTablea,
    },
  },
  data() {
    return {
      filter: '',
      abriModal: false,
      abrirModalDepesa: true,
      valorEdit: 0,
    };
  },
  computed: {
    tabelaDespesa: function () {
      return this.TableTipe === 'Despesa';
    },
  },
  methods: {
    excluirAtividade: function (id: string) {
      if (this.TableTipe === 'Despesa') {
        this.despesaStore.removerDespesa(id);
      } else {
        this.despesaStore.removerRendimento(id);
      }
    },
    obterTipoAtividade: function (row: any) {
      if (this.TableTipe === 'Despesa') {
        return TipoDespesas.find((item) => item.id === row.idTipoDespesa)
          ?.label;
      } else {
        return TipoRendimento.find((item) => item.id === row.idTipoRendimento)
          ?.label;
      }
    },
    adicionarAtividade: function (item: IDespesa | IRendimento) {
      if (this.tabelaDespesa) {
        this.despesaStore.adicionarDespesa(item as IDespesa);
      } else {
        this.despesaStore.adicionarRendimento(item as IRendimento);
      }
      this.abriModal = false;
    },
    alterarValor: function (id: string, value: number) {
      this.despesaStore.atualizarValor(
        value,
        id,
        this.tabelaDespesa ? 'Despesa' : 'Rendimento'
      );
    },
    alterarDescricao: function (id: string, descricaoNova: string) {
      this.despesaStore.atualizarDescricao(
        id,
        descricaoNova,
        this.tabelaDespesa ? 'Despesa' : 'Rendimento'
      );
    },
  },
  setup(props) {
    const columns: any[] = [
      {
        name: 'tipo',
        required: true,
        label:
          props.TableTipe === 'Despesa'
            ? 'Tipo do Gasto'
            : 'Tipo do rendimento',
        align: 'left',
        field: (row: any) => row.idTipoDespesa,
        format: (val: any) => `${val}`,
        sortable: false,
      },
      {
        name: 'descricao',
        label: 'Descricão',
        field: 'descricao',
        align: 'left',
      },
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

    const dados = ref();
    const despesaStore = useDespesaStore();
    if (props.TableTipe === 'Despesa') {
      dados.value = despesaStore.getDespesas;
    } else {
      dados.value = despesaStore.getRendimentos;
    }

    return {
      despesaStore,
      columns,
      dados,
      TipoDespesas,
      TipoRendimento,
      initialPagination: {
        sortBy: 'desc',
        descending: false,
        rowsPerPage: 10,
      },
    };
  },
};
</script>
