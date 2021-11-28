import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-diaries-index',
  templateUrl: './diaries-index.component.html',
  styleUrls: ['./diaries-index.component.scss']
})
export class DiariesIndexComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  signout() {
    this.authService.signout();
  }
}
