import { Injectable } from '@angular/core';
import { Settings } from '../model/settings'

@Injectable()
export class SettingsService {

  private _settings: Settings;

  get settings():Settings {
    return this._settings;
  }
  set settings(settings:Settings) {
      this._settings = settings;
  }
 

}
