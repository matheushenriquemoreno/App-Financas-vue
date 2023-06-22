<template>
  <div class="q-pa-md">
    <TableGerenciamentoMensal itemName="Despesa" TableTipe="Despesa" />
  </div>
</template>


<script lang="ts">
import TableGerenciamentoMensal from 'src/components/TableGerenciamentoMensal.vue';
export default {
  name: 'DespesaView',
  components: { TableGerenciamentoMensal },
};
</script>