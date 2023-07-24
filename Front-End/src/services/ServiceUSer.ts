import IDespesa from 'src/interfaces/IDespesa';
import IUSer from 'src/interfaces/IUSer';
import IRendimento from 'src/interfaces/IRendimento';
import { AtividadeMensal } from 'src/interfaces/AtividadeMensal';


export function userLogado() {
  const userName = localStorage.getItem('userName')
  const email = localStorage.getItem('email')

  if (userName && email) {
    return true;
  }
  return false;
}

export default class UserService {
  usuario: IUSer;

  constructor() {
    const userPlataforma = localStorage.getItem('user');
    this.usuario = JSON.parse(userPlataforma!);
  }

  adicionarDespesaUsuario(depesas: IDespesa) {
    this.usuario.listaDespesas.push(this.adicionarIdHash(depesas));
    this.salvar()
  }

  adicionarRendimentoUsuario(rendimentos: IRendimento) {
    this.usuario.listaRendimentos.push(this.adicionarIdHash(rendimentos));
    this.salvar()
  }

  atualizarDepesas(depesas: IDespesa[]) {
    this.usuario.listaDespesas = depesas;
    this.salvar()
  }

  atualizarRendimento(rendimentos: IRendimento[]) {
    this.usuario.listaRendimentos = rendimentos;
    this.salvar()
  }

  salvar() {
    localStorage.setItem('user', JSON.stringify(this.usuario))
  }

  adicionarIdHash<T extends AtividadeMensal>(atividade: T) {
    atividade.idHashUser = this.usuario.idHash
    return atividade;
  }

}
