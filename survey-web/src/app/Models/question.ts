import {Time} from '@angular/common';
import {Option} from './option';

export interface Question {
    id: string;
    order: number;
    createdDateUtc: Time;
    title: string;
    subTitle: string;
    questionType: string;
    surveyId: string;
    options: Option[];
}
