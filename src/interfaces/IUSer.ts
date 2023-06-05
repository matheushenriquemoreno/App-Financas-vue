import IDespesa from './AtividadeMensal';

export default interface IUser{
    id: number;
    name: string;
    email: string;
    salario?:number;
    listaDespesas: IDespesa[];
}