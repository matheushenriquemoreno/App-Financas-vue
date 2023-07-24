<template>
  <q-select
    filled
    v-model="model"
    use-input
    input-debounce="0"
    :label="label"
    :options="options"
    :rules="[(val) => val || 'Campo obrigatorio']"
    emit-value
    map-options
    :multiple="selectMultiplo"
    options-selected-class="text-deep-orange"
    @filter="filtrarPesquisa"
    @update:model-value="(value) => $emit('aoSalvar', value)"
  >
    <template v-slot:option="{ itemProps, opt, selected, toggleOption }">
      <q-item v-if="selectMultiplo" v-bind="itemProps">
        <q-item-section>
          <q-item-label>{{ opt.label }} </q-item-label>
        </q-item-section>
        <q-item-section side>
          <q-toggle
            :model-value="selected"
            @update:model-value="toggleOption(opt)"
          />
        </q-item-section>
      </q-item>

      <q-item v-else v-bind="itemProps">
        <q-item-section avatar>
          <q-icon :name="selected ? 'check' : 'chevron_right'" />
        </q-item-section>
        <q-item-section>
          <q-item-label>{{ opt.label }}</q-item-label>
          <q-item-label caption>{{ opt.description }}</q-item-label>
        </q-item-section>
      </q-item>
    </template>
    <template v-slot:no-option>
      <q-item>
        <q-item-section class="text-grey">
          Nenhum resultado encontrado
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script lang="ts">
import { PropType, ref } from 'vue';
import { TipoAtividade } from 'src/interfaces/AtividadeMensal';

export default {
  name: 'CampoSelect',
  emits: ['aoSalvar'],
  props: {
    opcoes: {
      type: [] as PropType<Array<TipoAtividade>>,
      required: true,
    },
    label: {
      type: String,
      required: true,
    },
    modelValue: {
      type: Object as PropType<any>,
      required: true,
    },
    selectMultiplo: {
      type: Boolean,
      default: false,
      required: false,
    },
  },
  setup(props) {
    const model = ref(props.modelValue);
    const options = ref(props.opcoes);
    return {
      model,
      options,

      filtrarPesquisa(val: string, update: any) {
        update(() => {
          if (val === '') {
            options.value = props.opcoes;
          } else {
            const pesquisa = val.toLowerCase();
            options.value = props.opcoes.filter(
              (opcao) => opcao.label.toLowerCase().indexOf(pesquisa) > -1
            );
          }
        });
      },
    };
  },
};
</script>
