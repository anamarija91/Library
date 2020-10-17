import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AngularMaterialModule } from './material.module';

import { TopBarComponent } from './components/top-bar/top-bar.component';
import { UserEditorComponent } from './components/user-editor/user-editor.component';
import { InputFieldComponent } from './components/input-field/input-field.component';
import { DatepickerFieldComponent } from './components/datepicker-field/datepicker-field.component';
import { DatePipe } from '@angular/common';
import { MultipleValuesFieldComponent } from './components/multiple-values-field/multiple-values-field.component';

import { AppConfigService } from './app-config.service';
import { ToastrModule } from 'ngx-toastr';

const appInitializerFn = (): any => {
  return (): Promise<void> => {
    return AppConfigService.loadAppConfig();
  };
};

@NgModule({
  declarations: [
    AppComponent,
    TopBarComponent,
    UserEditorComponent,
    InputFieldComponent,
    DatepickerFieldComponent,
    MultipleValuesFieldComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFn,
      multi: true,
      deps: [AppConfigService],
    },
    DatePipe
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
