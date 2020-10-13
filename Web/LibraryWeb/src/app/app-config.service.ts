import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ConfigurationData } from 'src/models/general/configurationData';

@Injectable({
  providedIn: 'root',
})
export class AppConfigService {
  private static appConfig: ConfigurationData;

  constructor() {}

  public static async loadAppConfig(): Promise<void> {
    const config = environment.production ? './assets/config/config.json' : './assets/config/config-local.json';
    try {
      const response = await fetch(config, { method: 'GET' });
      AppConfigService.appConfig = await response.json();
    } catch (error) {
      console.log(error);
    }
  }

  public getConfig(): ConfigurationData {
    return AppConfigService.appConfig;
  }
}
