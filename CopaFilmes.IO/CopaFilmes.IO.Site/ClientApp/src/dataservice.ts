import { Injectable } from '@angular/core';
import { Filme, FilmesCopaID } from './filme';

@Injectable()

export class DataService {
  public filmes: Filme[] = [];
  public filmesCopaID: FilmesCopaID[] = [];
}
