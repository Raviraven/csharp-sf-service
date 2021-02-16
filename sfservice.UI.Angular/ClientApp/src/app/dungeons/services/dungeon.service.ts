import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Dungeon } from 'src/app/models/dungeon';

@Injectable({
  providedIn: 'root'
})
export class DungeonService {

  private _dungeons: BehaviorSubject<Dungeon[]>;

  private dataStore: {
    dungeons: Dungeon[]
  }

  constructor(private http: HttpClient) { 
    this.dataStore = { dungeons:[] };
    this._dungeons = new BehaviorSubject<Dungeon[]>([]);
  }

  get dungeons(): Observable<Dungeon[]>{
    return this._dungeons.asObservable();
  }

  dungeonById(Id: number){
    return this.dataStore.dungeons.find(x=>x.number == Id);
  }

  loadAllDungeons(){
    console.log("ajsdhflkajshd");
    const dungeonsUrl = "http://localhost/sfservice.API/dungeons";

    return this.http.get<Dungeon[]>(dungeonsUrl)
      .subscribe(data => {
        this.dataStore.dungeons = data;
        this._dungeons.next(Object.assign({}, this.dataStore).dungeons);
      }, error => {
        console.log(`Failed to fetch dungeons`)
      });
  }

  
}
