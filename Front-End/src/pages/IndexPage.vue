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
        <q-route-tab name="Graficos" label="Graficos" to="/Acompanhamento" />
      </q-tabs>
    </div>

    <router-view></router-view>
  </q-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useQuasar } from 'quasar';
import IDespesa, { TipoDespesas } from 'src/interfaces/IDespesa';
import ValorPadraoBR from 'src/components/ValorPadraoBR.vue';
import { useDespesaStore } from 'src/stores/gerenciamentoMensalStore';
import IRendimento from 'src/interfaces/IRendimento';

export default defineComponent({
  name: 'IndexPage',
  components: {
    ValorPadraoBR,
  },
  data() {
    return {
      abriModal: false,
      filter: '',
      despesas: [] as Array<IDespesa>,
      rendimentos: [] as Array<IRendimento>,
    };
  },
  computed: {
    calcularValorDespesa(): number {
      return this.despesas
        .map((x) => x.valor)
        .reduce((acumulador, valorAtual) => {
          return acumulador + valorAtual;
        }, 0);
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
  async created() {
    console.log('');
    this.despesas = this.despesaStore.getDespesas;
    this.rendimentos = this.despesaStore.getRendimentos;
  },
  setup() {
    const quasar = useQuasar();
    const despesaStore = useDespesaStore();

    return { quasar, TipoDespesas, despesaStore };
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
