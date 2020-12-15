import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SurveyDetailComponent} from './Components/survey-detail/survey-detail.component';
import {SurveyDashboardComponent} from './Components/survey-dashboard/survey-dashboard.component';
import {AnswerDetailComponent} from './Components/answer-detail/answer-detail.component';

const routes: Routes = [
  {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
  {path: 'dashboard', component: SurveyDashboardComponent},
  {path: 'survey/:id', component: SurveyDetailComponent},
  {path: 'answer/:id', component: AnswerDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
