import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import {v4 as guid} from 'uuid';
import {MessageService} from '../../Services/message.service';
import {SurveyService} from '../../Services/survey.service';
import {Survey} from '../../Models/survey';
import {Answer, QuestionAnswer} from '../../Models/answer';

@Component({
  selector: 'app-survey-detail',
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
  selectedAnswers: Map<string, string>;

  ngOnInit(): void {
    this.surveyId = this.route.snapshot.paramMap.get('id');
    this.getSurvey(this.surveyId);
    this.selectedAnswers = new Map<string, string>();
  }

  getSurvey(id: string): void {
    this.surveyService.getSurveyById(id)
      .subscribe(c => {
        this.survey = c;
      });
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    const newAnswer = {
      userId: guid(), // for now randomly generate user Id.
      surveyId: this.survey.id,
      createdDateUtc: null, // leave BE generate the time stamp.
      questionAnswers: []
    } as Answer;

    this.selectedAnswers.forEach((value, key, map) => {
      const a = {
        questionId: key,
        optionId: value
      } as QuestionAnswer;
      newAnswer.questionAnswers.push(a);
    });

    if (newAnswer.questionAnswers.length === 0) {
      this.messageService.add('Can not save current answer due to no option selected');
      return;
    }

    const msgPrefix = `${new Date().toLocaleString('en-AU', {timeZone: 'UTC'})} -> `;
    this.surveyService.addAnswer(newAnswer)
      .subscribe((result) => {
        this.messageService.add(msgPrefix + `New answer "${result.id}" created.`);
        this.back();
      });
  }

  selectAnswer(questionId: string, optionId: string): void {
    if (this.selectedAnswers.get(questionId) === optionId) {
      // remove selection
      this.selectedAnswers.delete(questionId);
      this.messageService.add(`un-selected question: ${questionId}, option: ${optionId}`);
    } else {
      this.selectedAnswers.set(questionId, optionId);
      this.messageService.add(`selected question: ${questionId}, option: ${optionId}`);
    }
  }

  reset(): void {
    this.selectedAnswers = new Map<string, string>();
    this.messageService.add('reset selections');
  }

  back(): void {
    this.location.back();
  }

  disableSave(): boolean {
    // return !this.customiseName && this.customiseName.trim() !== '';
    return true;
  }
}
