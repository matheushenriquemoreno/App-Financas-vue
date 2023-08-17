<template>
  <div class="row justify-center q-mt-lg q-gutter-sm">
    <q-card class="q-pa-xl">
      <span>Rendimentos</span>
      <apexchart
        type="bar"
        class="grafico"
        :options="options"
        :series="series"
      ></apexchart>
    </q-card>

    <q-card class="q-pa-xl">
      <span>Despesas</span>
      <apexchart
        type="bar"
        class="grafico"
        :options="options"
        :series="series"
      ></apexchart>
    </q-card>
  </div>
</template>


<script lang="ts">
interface CategoriaOrdenavel {
  nome: string;
  id: number;
  dados: any;
}

// retornar as options, que seria as config, e os valores
function montarGraficoDespesa(categorias: Array<string>) {
  const categoriasOrdenavel: CategoriaOrdenavel[] = categorias.map(
    (categoria, index) => {
      return {
        nome: categoria,
        id: index + 1,
        dados: null,
      };
    }
  );

  console.log('');
}

export default {
  name: 'AcompanhamentoView',
  data: function () {
    return {
      options: {
        chart: {
          id: 'vuechart-example',
        },
        xaxis: {
          categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998],
        },
      },
      series: [
        {
          name: 'series-1',
          data: [30, 40, 45, 50, 49, 60, 70, 91],
        },
      ],
    };
  },
};
</script>


<style>
.grafico {
  width: 500px;
}

@media (max-width: 520px) {
  .grafico {
    width: 320px;
  }
}
</style>