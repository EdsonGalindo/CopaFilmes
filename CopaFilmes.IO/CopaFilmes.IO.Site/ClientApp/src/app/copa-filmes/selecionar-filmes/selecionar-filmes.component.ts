import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Filme } from '../../../filme';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../../../dataservice';

@Component({
  selector: 'app-selecionar-filmes',
  templateUrl: './selecionar-filmes.component.html',
  styleUrls: ['../../app.component.css'],
  providers: [DataService]
})

export class SelecionarFilmesComponent implements OnInit, OnDestroy {
  public filmes: Filme[];
  public counter: number = 0;

  constructor(http: HttpClient, public dataservice: DataService, @Inject('BASE_URL_API') baseUrl: string) {
    let url = baseUrl + 'api/CopaFilmes/filmes-disponiveis';

    http.get<Filme[]>(url).subscribe(result => {
      this.filmes = result;
    }, error => console.error(error));
  }

  public checkedState(event, checkBox) {
    if (event.target.checked === true) {
      if (this.counter < 8) {
        this.counter++
      } else {
        event.target.checked = false;
      }
    } else if (this.counter > 0) {
      this.counter--;
    }
  }

  ngOnInit() { }

  ngOnDestroy() {
    this.dataservice.filmesId = this.filmes.filter(f => f.selecionado);
  }
}
