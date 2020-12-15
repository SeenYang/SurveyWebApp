import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SurveyService} from '../../Services/survey.service';
import {Survey} from '../../Models/survey';
import {Answer} from '../../Models/answer';

@Component({
  selector: 'app-survey-dashboard',
  templateUrl: './survey-dashboard.component.html',
  styleUrls: ['./survey-dashboard.component.css']
})
export class SurveyDashboardComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private surveyService: SurveyService,
  ) {
  }

  surveys: Survey[];
  answers: Answer[];


  ngOnInit(): void {
    this.getAllSurveys();
    this.getAnswers();
  }

  getAllSurveys(): void {
    this.surveyService.getSurveys()
      .subscribe((result) => {
        this.surveys = result;
      });
  }

  getAnswers(): void {
    this.surveyService.getAllAnswers()
      .subscribe((result) => {
        this.answers = result;
      });
  }
}
