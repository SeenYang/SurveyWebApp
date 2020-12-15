import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {MessagesComponent} from './Components/messages/messages.component';
import {ENV, getEnv} from '../environments/environment.provider';
import {SurveyDetailComponent} from './Components/survey-detail/survey-detail.component';
import {SurveyDashboardComponent} from './Components/survey-dashboard/survey-dashboard.component';
import {AnswerDetailComponent} from './Components/answer-detail/answer-detail.component';
import {QuestionComponent} from './components/question/question.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyDetailComponent,
    MessagesComponent,
    SurveyDashboardComponent,
    AnswerDetailComponent,
    QuestionComponent],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [{provide: ENV, useFactory: getEnv}],

  bootstrap: [AppComponent]
})
export class AppModule {
}
