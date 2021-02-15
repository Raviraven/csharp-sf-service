import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Dungeon } from '../models/dungeon';
import { DungeonService } from './services/dungeon.service';

@Component({
  selector: 'app-dungeons-app',
  template: `
    <p>
      dungeons-app works!
    </p>
  `,
  styles: [
  ]
})
export class DungeonsAppComponent implements OnInit {

  dungeonsList: Observable<Dungeon[]>;

  constructor(private route: ActivatedRoute, 
    private dungeonService: DungeonService) { }

  ngOnInit(): void {
    this.dungeonsList = this.dungeonService.dungeons;
  }

}
