import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { QuoteComponent } from './components/quote/quote.component';
import { SettingsComponent } from './components/settings/settings.component';

import { SettingsService } from './services/settings.service';
import { QuoteService } from './services/quote.service';
import { BookService } from './services/book.service';


const appRoutes: Routes = [
  { path: 'settings', component: SettingsComponent },
  { path: 'next', component: QuoteComponent },
  { path: 'random', component: QuoteComponent },
  { path: '', component: QuoteComponent },
  // { path: 'hero/:id',      component: HeroDetailComponent },
  // {
  //   path: 'heroes',
  //   component: HeroListComponent,
  //   data: { title: 'Heroes List' }
  // },
  // { path: '',
  //   redirectTo: '/heroes',
  //   pathMatch: 'full'
  // },
  //{ path: '**', component: PageNotFoundComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    QuoteComponent,
    SettingsComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [QuoteService, BookService, SettingsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
