<template>
  <q-select
    filled
    v-model="itemSelecionado"
    :options="opcoes"
    :label="label"
    lazy-rules
    :rules="[(val) => val || 'Campo obrigatorio']"
    emit-value
    map-options
    @update:model-value="(value) => $emit('aoSalvar', value)"
  />
</template>

<script lang="ts">
import { PropType } from 'vue';
import { TipoAtividade } from 'src/interfaces/AtividadeMensal';

export default {
  name: 'CampoSelect',
  emits: ['aoSalvar'],
  data() {
    return {
      itemSelecionado: '',
    };
  },
  props: {
    opcoes: {
      type: [] as PropType<Array<TipoAtividade>>,
      required: true,
    },
    label: {
      type: String,
      required: true,
    },
  },
};
</script>
