import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Filme, FilmeId } from '../../../filme';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { DataService } from '../../../dataservice';
import { Http, RequestOptions, URLSearchParams } from '@angular/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './resultado-final.component.html',
  styleUrls: ['../../app.component.css'],
  providers: [DataService]
})

export class ResultadoFinalComponent implements OnInit, OnDestroy {
  public filmes: Filme[];
  public filmesId: FilmeId[];

  constructor(public httpClient: HttpClient, public dataservice: DataService, @Inject('BASE_URL_API') public baseUrl: string) {
    let url = this.baseUrl + 'api/CopaFilmes/filmes-selecionados';
    let params = { 'FilmesCopaID' : '1' };//JSON.parse(JSON.stringify(this.filmesId));

    httpClient.get<Filme[]>(url, { params: params }).subscribe(result => {
      this.filmes = result;
    }, error => console.error(error));
  }

  public obterResultadoCopa() {
    let url = this.baseUrl + 'api/CopaFilmes/filmes-selecionados';
    let params = JSON.parse(JSON.stringify(this.filmesId));

    this.httpClient.get<Filme[]>(url, { params: params }).subscribe(result => {
      this.filmes = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    this.filmesId = this.dataservice.filmesId;
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
