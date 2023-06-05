<template>
  <q-page>
    <!-- <div class="q-pa-md">
      <div class="q-gutter-y-md">
        <q-tabs class="text-primary" align="left" dense :breakpoint="0">
          <q-btn-dropdown
            auto-close
            stretch
            flat
            icon="event"
            label="Ano - 2023 "
          >
            <q-list>
              <q-item clickable>
                <q-item-section>Ano - 2024</q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </q-tabs>
      </div>

      <div class="q-gutter-y-md mes">
        <q-tabs class="text-primary" dense align="left" :breakpoint="0">
          <q-btn-dropdown
            auto-close
            stretch
            flat
            icon="event"
            label="Mes - 06 - junho "
          >
            <q-list>
              <q-item clickable>
                <q-item-section>Mes - 07 - julho</q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </q-tabs>
      </div>
    </div> -->

    <div class="row col-12 justify-center q-gutter-xl shadow q-pa-md">
      <q-input
        class="col-xs-8 col-md-3 col-sm-3 col-xl-3 col"
        v-model="user.salario"
        label="Salario"
        type="number"
        step="0.01"
        min="0"
      >
      </q-input>

      <q-badge
        color="red"
        class="col-xs-8 col-md-3 col-sm-3 col-xl-3 justify-center gastos"
      >
        Despesa
        {{
          calcularValorDespesa.toLocaleString('pt-br', {
            style: 'currency',
            currency: 'BRL',
          })
        }}
        <q-icon name="warning" color="white" class="q-ml-xs" />
      </q-badge>

      <q-badge
        :color="calcularRestante < 0 ? 'orange' : 'teal'"
        class="col-xs-8 col-md-3 col-sm-3 col-xl-3 justify-center gastos"
      >
        Restante
        {{
          calcularRestante.toLocaleString('pt-br', {
            style: 'currency',
            currency: 'BRL',
          })
        }}
      </q-badge>
    </div>

    <div class="q-pa-md">
      <TableGerenciamentoMensal />
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, toRaw } from 'vue';
import IUser from 'src/interfaces/IUSer';
import { useQuasar } from 'quasar';
import { TipoDespesas } from 'src/interfaces/AtividadeMensal';
import TableGerenciamentoMensal from 'src/components/TableGerenciamentoMensal.vue';
import { useDespesaStore } from 'src/stores/despesasStore';

export default defineComponent({
  name: 'IndexPage',
  components: { TableGerenciamentoMensal },
  data() {
    return {
      user: {} as IUser,
      abriModal: false,
      filter: '',
    };
  },

  computed: {
    calcularValorDespesa() {
      let valor = 0;

      for (const item of this.despesas) {
        valor += item.valor;
      }
      return valor;
    },
    calcularRestante(): number {
      const salario = this.user.salario ?? 0;
      return salario - this.calcularValorDespesa;
    },
  },
  setup() {
    const quasar = useQuasar();
    const despesaStore = useDespesaStore();
    const despesas = ref(despesaStore.getDespesas);

    return { quasar, TipoDespesas, despesaStore, despesas };
  },
});
</script>


<style scoped>
.mes {
  margin-left: 30px;
}
.gastos {
  font-size: 16px;
}
</style>