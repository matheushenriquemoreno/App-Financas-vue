
// export enum TipoDespesa{
//     Alimentacao = 1,
//     Gasolina = 2,
//     CartaoDeCredito = 3
// }

//convertEnumToArray(TipoDespesa);

// function convertEnumToArray(prop: object){
//     const arrayObjects = []            

//       for (const [propertyKey, propertyValue] of Object.entries(prop)) { 

//        if (!Number.isNaN(Number(propertyKey))) {  
//          continue;  
//        }  
 
//        arrayObjects.push({ id: propertyValue, label: propertyKey });  
//      }        
 
//    console.log(arrayObjects); 
//    return arrayObjects;
// }

// export const obterTipoDespesaSelect = () => { 
//     return convertEnumToArray(TipoDespesa); 
// }


class TipoAtividade{
    id: number;
    label: string;
    value: number;
    constructor(id: number, label: string){
        this.id = id;
        this.label = label; 
        this.value = id;
    }
}

export const TipoDespesas: TipoAtividade[] = [
    new TipoAtividade(1 ,'Alimentação'),
    new TipoAtividade(2 ,'Gasolina'),
    new TipoAtividade(3 ,'Cartão de Crédito'),
    new TipoAtividade(4 ,'Livros'),
    new TipoAtividade(5 ,'Estudos')
]


export const TipoRendimento: TipoAtividade[] = [
    new TipoAtividade(1 ,'Salario'),
    new TipoAtividade(1 ,'Renda extra'),
]

export interface AtividadeMensal{
    id: number;
    descricao: string;
    valor: number;
}

export default interface IDespesa extends AtividadeMensal{
    idTipoDespesa: number;
}

export interface IRendimento extends AtividadeMensal{
    idTipoRendimento: number;
}