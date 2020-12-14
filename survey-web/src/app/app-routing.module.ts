import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SurveyDetailComponent} from './Components/survey-detail/survey-detail.component';
import {SurveyDashboardComponent} from './Components/survey-dashboard/survey-dashboard.component';
import {CustomiseDetailComponent} from './Components/customise-detail/customise-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: SurveyDashboardComponent },
  { path: 'character/:id', component: SurveyDetailComponent },
  { path: 'customise/:id', component: CustomiseDetailComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
