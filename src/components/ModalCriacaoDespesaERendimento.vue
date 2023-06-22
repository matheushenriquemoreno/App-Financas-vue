<template>
  <q-dialog v-model="abriModal" persistent>
    <q-card style="width: 700px; max-width: 80vw">
      <q-card-section>
        <div class="text-h6">
          {{
            modalCadastroDepesa ? 'Adicionar Despesa' : 'Adicionar Rendimento'
          }}
        </div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        <div class="q-gutter-sm">
          <q-form @submit.prevent="adicionarDespesa">
            <q-input
              rounded
              filled
              v-model="descricao"
              label="Descricao"
              maxlength="100"
              lazy-rules
              :rules="[(val) => (val && val.length > 0) || 'Campo obrigatorio']"
            />

            <q-input
              rounded
              filled
              type="number"
              min="0"
              step="0.01"
              v-model="valor"
              label="Valor"
              lazy-rules
              :rules="[(val) => (val && val.length > 0) || 'Campo obrigatorio']"
            />

            <CampoSelect
              :label="
                modalCadastroDepesa ? 'Tipo da Despesa' : 'tipo do Rendimento'
              "
              :opcoes="modalCadastroDepesa ? TipoDespesas : TipoRendimento"
              @ao-salvar="
                (value) => {
                  tipoSelecionado = value;
                }
              "
            />
            <q-card-actions class="text-primary">
              <q-btn flat label="Cancelar" v-close-popup />
              <q-btn flat label="Adicionar" type="submit" />
            </q-card-actions>
          </q-form>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

  <script lang="ts">
import { defineComponent } from 'vue';
import IDespesa, { TipoDespesas } from 'src/interfaces/IDespesa';
import IRendimento, { TipoRendimento } from 'src/interfaces/IRendimento';
import { AtividadeMensal } from 'src/interfaces/AtividadeMensal';
import CampoSelect from './CampoSelect.vue';

export default defineComponent({
  name: 'ModalGerenciamento',
  emits: ['AoSalvarModal', 'alterar'],
  components: { CampoSelect },
  props: {
    value: {
      type: Boolean,
      default: false,
    },
    modalCadastroDepesa: {
      type: Boolean,
      default: true,
    },
  },
  computed: {
    abriModal: {
      get(): boolean {
        return this.value;
      },
      set(value: boolean) {
        this.$emit('alterar', value);
      },
    },
  },
  methods: {
    adicionarDespesa: function () {
      let atividadeMensal: AtividadeMensal = {
        id: new Date().toISOString(),
        idHashUser: '',
        descricao: this.descricao,
        valor: parseFloat(this.valor),
      };

      if (this.modalCadastroDepesa) {
        const depesa: IDespesa = {
          ...atividadeMensal,
          idTipoDespesa: parseInt(this.tipoSelecionado),
        };

        this.$emit('AoSalvarModal', depesa);
      } else {
        const rendimento: IRendimento = {
          ...atividadeMensal,
          idTipoRendimento: parseInt(this.tipoSelecionado),
        };
        this.$emit('AoSalvarModal', rendimento);
      }
      this.descricao = '';
      this.valor = '';
      this.tipoSelecionado = '';
    },
  },
  data() {
    return {
      descricao: '',
      valor: '',
      tipoSelecionado: '',
    };
  },
  setup() {
    return { TipoDespesas, TipoRendimento };
  },
});
</script>
