import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SurveyService} from '../../Services/survey.service';
import {Survey} from '../../Models/survey';
import {Customise} from '../../Models/customise';

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

    characters: Survey[];
    customises: Customise[];


    ngOnInit(): void {
        this.getAllSurveys();
        // this.getAnswers();
    }

    getAllSurveys() {
        this.surveyService.getSurveys()
            .subscribe((result) => {
                this.characters = result;
            });
    }

    getAnswers() {
        this.surveyService.getAllAnswers()
            .subscribe((result) => {
                this.customises = result;
            });
    }
}
