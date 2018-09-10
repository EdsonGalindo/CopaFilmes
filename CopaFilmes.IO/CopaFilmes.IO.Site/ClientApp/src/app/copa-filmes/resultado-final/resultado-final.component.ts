import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { Filme, FilmesCopaID } from '../../../filme';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { DataService } from '../../../dataservice';

@Component({
  selector: 'app-resultado-final',
  templateUrl: './resultado-final.component.html',
  styleUrls: ['../../app.component.css']
})

export class ResultadoFinalComponent implements OnInit, OnDestroy {
  public filmes: Filme[];
  private filmesCopaID: FilmesCopaID[] = [];
  private idFilmes: String = '';
  public filmeCampeao: Filme;
  public filmeViceCampeao: Filme;

  constructor(private router: Router, public httpClient: HttpClient, private dataservice: DataService, @Inject('BASE_URL_API') public baseUrl: string) {

  }

  chamarSelecaoFilmes() {
    this.router.navigate(['selecionar-filmes']);
  }

  private obterResultadoCopa() {
    let url = this.baseUrl + 'api/CopaFilmes/filmes-selecionados';

    for (var prop of this.filmesCopaID) {
      this.idFilmes += (this.idFilmes.length > 1 ? ', ' : '') + prop.id;
    }
    
    console.log(this.idFilmes.toString());

    this.httpClient.get<Filme[]>(url + '?FilmesCopa=' + this.idFilmes).subscribe(result => {
      this.filmes = result;
      if (this.filmes.length > 1) {
        this.filmeCampeao = result[0];
        this.filmeViceCampeao = result[1];
      }
    }, error => console.error(error));

  }

  ngOnInit() {
    this.filmesCopaID = this.dataservice.filmesCopaID;
    console.log(this.filmesCopaID.length);
    this.obterResultadoCopa();
  }

  ngOnDestroy() {
  }

}

//interface Filme {
//  id: string;
//  titulo: string;
//  ano: number;
//  nota: number;
//}
