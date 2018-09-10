import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { Filme, FilmesCopaID } from '../../../filme';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../../../dataservice';

@Component({
  selector: 'app-selecionar-filmes',
  templateUrl: './selecionar-filmes.component.html',
  styleUrls: ['../../app.component.css']
})

export class SelecionarFilmesComponent implements OnInit, OnDestroy {
  public filmes: Filme[] = [];
  public contador: number = 0;
  public bloqBtnGerar: boolean = true;
  private filmesSelecaoID: FilmesCopaID[] = [];
  private objFilmeID: FilmesCopaID;

  constructor(private router: Router, http: HttpClient, private dataservice: DataService, @Inject('BASE_URL_API') baseUrl: string) {
    let url = baseUrl + 'api/CopaFilmes/filmes-disponiveis';

    http.get<Filme[]>(url).subscribe(result => {
      this.filmes = result;
    }, error => console.error(error));
  }

  public checkedState(event, filme) {
    if (event.target.checked === true) {
      if (this.contador < 8) {
        this.contador++;

        this.objFilmeID = new FilmesCopaID();
        this.objFilmeID.id = filme.id;
        this.filmesSelecaoID.push(this.objFilmeID);

      } else {
        event.target.checked = false;
      }
    } else if (this.contador > 0) {
      this.contador--;

      this.objFilmeID = new FilmesCopaID();
      this.objFilmeID = this.filmesSelecaoID.filter(f => f.id == filme.id)[0];
      const indiceFilmeID = this.filmesSelecaoID.indexOf(this.objFilmeID);
      if (indiceFilmeID !== -1)
        this.filmesSelecaoID.splice(indiceFilmeID, 1);
    }

    this.bloqBtnGerar = (this.contador < 8);

    console.log(this.filmesSelecaoID);
  }

  chamaResultadoFinal() {
    this.router.navigate(['resultado-final']);
  }

  ngOnInit() { }

  ngOnDestroy() {
    this.dataservice.filmesCopaID = this.filmesSelecaoID;
    console.log(this.dataservice.filmes.length);
  }
}
