import { TipoAtividade, AtividadeMensal } from './AtividadeMensal';

export const TipoRendimento: TipoAtividade[] = [
  new TipoAtividade(1, 'Salario'),
  new TipoAtividade(2, 'Renda extra'),
  new TipoAtividade(3, 'Dividendos'),
  new TipoAtividade(4, 'Beneficio'),
  new TipoAtividade(5, 'cashback'),
  new TipoAtividade(6, 'Recebimentos'),
]

export default interface IRendimento extends AtividadeMensal {
  idTipoRendimento: number;
}
