import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import {Option} from '../../Models/option';
import {v4 as guid} from 'uuid';
import {MessageService} from '../../Services/message.service';
import {SurveyService} from '../../Services/survey.service';
import {Survey} from '../../Models/survey';
import {Customise} from '../../Models/customise';
import {Question} from '../../Models/question';

@Component({
    selector: 'app-character-detail',
    templateUrl: './survey-detail.component.html',
    styleUrls: ['./survey-detail.component.css']
})
export class SurveyDetailComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private surveyService: SurveyService,
        private messageService: MessageService,
        private location: Location
    ) {
    }

    surveyId: string;
    survey: Survey;

    ngOnInit(): void {
        this.surveyId = this.route.snapshot.paramMap.get('id');
        this.getCharacter(this.surveyId);
    }

    getCharacter(id: string): void {
        this.surveyService.getSurveyById(id)
            .subscribe(c => {
                this.survey = c;
            });
    }

    goBack(): void {
        this.location.back();
    }

    save(): void {
        // const msgPrefix = `${new Date().toLocaleString('en-AU', {timeZone: 'UTC'})} -> `;
        // this.customiseCharacter.name = this.customiseName;
        // this.surveyService.addAnswer(this.customiseCharacter)
        //     .subscribe((result) => {
        //         this.messageService.add(msgPrefix + `New character "${result.name}" created. id: ${result.id}.`);
        //         this.back();
        //     });
    }

    selecteAnswer(questionId: string, optionId: string){

    }

    selectOption(option: Option) {
        // // Reset Suboption container if none of Option selected.
        // if (this.selectedOption === option) {
        //     this.selectedOption = null;
        //     this.options = null;
        // } else {
        //     this.selectedOption = option;
        //     // this.suboptions = option.;
        // }
    }

    selectSuboption(option: Option) {
        // this.selectedSubOption = option;
        // if (this.customiseCharacter.selectedOptions.indexOf(option.id) !== -1) {
        //     this.customiseCharacter.selectedOptions = this.customiseCharacter.selectedOptions.filter(c => c !== option.id);
        //     this.messageService.add(`Un-Selected Option ${option.text}`);
        // } else {
        //     this.customiseCharacter.selectedOptions.push(option.id);
        //     this.messageService.add(`Selected Option ${option.text}`);
        // }
    }

    reset() {
        // this.selectedOption = null;
        // this.selectedSubOption = null;
        // this.options = null;
        // this.customiseCharacter.selectedOptions = [];
        // this.messageService.add(`un-select all.`);
    }

    back() {
        this.location.back();
    }

    disableSave(): boolean {
        // return !this.customiseName && this.customiseName.trim() !== '';
        return true;
    }
}
