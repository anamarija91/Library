import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserData } from 'src/models/userData';

import { AppConfigService } from '../app-config.service';

enum Routes {
  Users = '/Users',
}

@Injectable({
  providedIn: 'root',
})
export class LibraryRestService {
  private appUrl: string;

  constructor(private http: HttpClient, private environment: AppConfigService) {
    this.appUrl = this.environment.getConfig().app_url;
  }

  public createNewUser(data: UserData): Observable<string> {
    return this.http.post<string>(this.appUrl + Routes.Users, data);
  }
}
