export interface AtividadeMensal {
  id: string;
  descricao: string;
  valor: number;
  idHashUser: string;
}


export class TipoAtividade {
  id: number;
  label: string;
  value: number;
  constructor(id: number, label: string) {
    this.id = id;
    this.label = label;
    this.value = id;
  }
}
































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
