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

    <div
      class="row justify-center q-gutter-x-xl q-gutter-y-sm shadow q-pa-md text-center"
    >
      <div class="col-md-3 col-sm-5">
        <q-badge color="green" class="gastos justify-center">
          Rendimento
          <ValorPadraoBR :valor="calcularRendimentos" />
        </q-badge>
      </div>
      <div class="col-md-3 col-sm-5">
        <q-badge color="red" class="gastos justify-center">
          Despesa
          <ValorPadraoBR :valor="calcularValorDespesa" />
          <q-icon name="warning" color="white" class="q-ml-xs" size="xs" />
        </q-badge>
      </div>
      <div class="col-md-3 col-sm-6">
        <q-badge
          :color="calcularRestante < 0 ? 'orange' : 'teal'"
          class="gastos justify-center"
        >
          Restante
          <ValorPadraoBR :valor="calcularRestante" />
          <q-icon
            v-if="calcularRestante < 0"
            name="warning"
            color="red"
            class="q-ml-xs"
            size="xs"
          />
        </q-badge>
      </div>
    </div>

    <div class="q-gutter-md">
      <q-tabs
        :v-model="''"
        dense
        align="center"
        class="text-dark"
        :breakpoint="0"
      >
        <q-route-tab name="Recebimentos" label="Recebimentos" to="/" />
        <q-route-tab name="Despesas" label="Despesas" to="/Despesa" />
      </q-tabs>
    </div>

    <router-view></router-view>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useQuasar } from 'quasar';
import { TipoDespesas } from 'src/interfaces/IDespesa';
import ValorPadraoBR from 'src/components/ValorPadraoBR.vue';

import { useDespesaStore } from 'src/stores/gerenciamentoMensalStore';

export default defineComponent({
  name: 'IndexPage',
  components: {
    ValorPadraoBR,
  },
  data() {
    return {
      abriModal: false,
      filter: '',
    };
  },

  computed: {
    calcularValorDespesa(): number {
      let valor = 0;

      for (const item of this.despesas) {
        valor += item.valor;
      }
      return valor;
    },
    calcularRestante(): number {
      const salario = this.calcularRendimentos ?? 0;
      return salario - this.calcularValorDespesa;
    },
    calcularRendimentos(): number {
      let valor = 0;

      for (const item of this.rendimentos) {
        valor += item.valor;
      }
      return valor;
    },
  },
  setup() {
    const quasar = useQuasar();
    const despesaStore = useDespesaStore();
    const despesas = ref(despesaStore.getDespesas);
    const rendimentos = ref(despesaStore.getRendimentos);

    return { quasar, TipoDespesas, despesaStore, despesas, rendimentos };
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
