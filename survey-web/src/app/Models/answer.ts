import {Time} from '@angular/common';

export interface Answer {
  id: string;
  userId: string;
  surveyId: string;
  createdDateUtc: Time;
  questionAnswers: QuestionAnswer[];
}

export interface QuestionAnswer {
  answerId: string;
  questionId: string;
  optionId: string;
  textAnswer: string;
}
