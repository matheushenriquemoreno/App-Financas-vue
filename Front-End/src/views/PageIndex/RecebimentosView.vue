<template>
  <div class="q-pa-md">
    <TableGerenciamentoMensal itemName="IRendimento" TableTipe="IRendimento" />
  </div>
</template>


  <script lang="ts">
import TableGerenciamentoMensal from 'src/components/TableGerenciamentoMensal.vue';

export default {
  name: 'RecebimentosView',
  components: { TableGerenciamentoMensal },
};
</script>
