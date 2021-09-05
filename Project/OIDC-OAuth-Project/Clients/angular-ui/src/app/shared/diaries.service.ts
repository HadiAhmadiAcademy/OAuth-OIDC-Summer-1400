import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DiaryEntry } from './diary-entry.model';
import { AuthService } from '../auth/auth.service';

@Injectable({ providedIn: 'root' })
export class DiariesService {
    constructor(private httpClient: HttpClient, private authService: AuthService) { }

    public getAllDiaries(): Observable<Array<DiaryEntry>> {

        //TODO: Use interceptor for a real-world app
        var user = this.authService.getUser();

        var httpOptions = {
            headers: new HttpHeaders({ Authorization: `${user.token_type} ${user.access_token}` })
        };
        debugger;
        return this.httpClient
            .get<Array<DiaryEntry>>("https://localhost:5005/api/diaries/all", httpOptions);
    }

}