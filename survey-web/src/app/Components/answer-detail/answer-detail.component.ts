import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SurveyService} from '../../Services/survey.service';
import {MessageService} from '../../Services/message.service';
import {Location} from '@angular/common';
import {Answer} from '../../Models/answer';

@Component({
  selector: 'app-customise-detail',
  templateUrl: './answer-detail.component.html',
  styleUrls: ['./answer-detail.component.css']
})
export class AnswerDetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private surveyService: SurveyService,
    private messageService: MessageService,
    private location: Location
  ) {
  }

  answerId: string;
  answer: Answer;

  ngOnInit(): void {
    this.answerId = this.route.snapshot.paramMap.get('id');
    this.getAnswer(this.answerId);
  }

  getAnswer(answerId: string): void {
    this.surveyService.getAnswerById(answerId)
      .subscribe(c => {
        this.answer = c;
      });
  }

  back(): void {
    this.location.back();
  }

}
