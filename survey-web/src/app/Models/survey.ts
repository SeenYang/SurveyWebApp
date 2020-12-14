import {Option} from './option';
import {Time} from '@angular/common';
import {Question} from './question';

export interface Survey {
    id: string;
    name: string;
    description: string;
    type: string;
    userId: string;
    userName: string;
    createdDateUtc: Time;
    updatedDateUtc: Time;
    questions: Question[];
}
