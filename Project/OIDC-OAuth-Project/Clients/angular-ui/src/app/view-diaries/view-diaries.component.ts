import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DiaryEntry } from '../shared/diary-entry.model';
import { DiariesService } from '../shared/diaries.service';
import { AuthService } from '../auth/auth.service';
import { User } from 'oidc-client';

@Component({
  selector: 'app-view-diaries',
  templateUrl: './view-diaries.component.html',
  styleUrls: ['./view-diaries.component.scss']
})
export class ViewDiariesComponent implements OnInit {

  public diaries: Observable<Array<DiaryEntry>>;
  public user: User;

  constructor(private service: DiariesService, private authService: AuthService) { }

  ngOnInit(): void {
    this.user = this.authService.getUser();  
    this.diaries = this.service.getAllDiaries();
  }

}
