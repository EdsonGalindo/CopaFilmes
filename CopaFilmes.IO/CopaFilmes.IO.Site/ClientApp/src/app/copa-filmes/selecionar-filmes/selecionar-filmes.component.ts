import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-selecionar-filmes',
  templateUrl: './selecionar-filmes.component.html'
})
export class SelecionarFilmesComponent {
  public filmes: Filme[];

  constructor(http: HttpClient, @Inject('BASE_URL_API') baseUrl: string) {
    http.get<Filme[]>(baseUrl + 'api/CopaFilmes/filmes-disponiveis').subscribe(result => {
      this.filmes = result;
    }, error => console.error(error));
  }
}

interface Filme {
  id: string;
  titulo: string;
  ano: number;
  nota: number;
}
