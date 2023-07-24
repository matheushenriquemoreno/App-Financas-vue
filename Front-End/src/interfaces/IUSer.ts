import IDespesa from './IDespesa';
import IRendimento from './IRendimento';

export default interface IUser{
    id?: number;
    idHash: string;
    name: string;
    email: string;
    salario?:number;
    listaDespesas: IDespesa[];
    listaRendimentos: IRendimento[];
}