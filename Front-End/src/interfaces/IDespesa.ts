import { AtividadeMensal, TipoAtividade } from './AtividadeMensal';

export const TipoDespesas: TipoAtividade[] = [
    new TipoAtividade(1 ,'Alimentação'),
    new TipoAtividade(2 ,'Gasolina'),
    new TipoAtividade(3 ,'Cartão de Crédito'),
    new TipoAtividade(4 ,'Livros'),
    new TipoAtividade(5 ,'Estudos')
]

export default interface IDespesa extends AtividadeMensal{
    idTipoDespesa: number;
}


