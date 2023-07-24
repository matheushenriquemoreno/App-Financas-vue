<template>
  {{
    valor.toLocaleString('pt-br', {
      style: 'currency',
      currency: 'BRL',
    })
  }}
</template>

<script lang="ts">
export default {
  name: 'ValorPadraoBR',
  props: {
    valor: {
      type: Number,
      required: true,
    },
  },
};
</script>