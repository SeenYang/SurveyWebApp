import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MessageService} from './message.service';
import {EMPTY, Observable, of} from 'rxjs';
import {catchError, mapTo, tap} from 'rxjs/operators';
import {Survey} from '../Models/survey';
import {environment as testEnv} from '../../environments/environment';
import {Answer} from '../Models/answer';

@Injectable({providedIn: 'root'})
export class SurveyService {
  private surveyUrl = `${testEnv.apiPath}/api/Surveys`;  // URL to web api
  private answerUrl = `${testEnv.apiPath}/api/Answers`;  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService) {
  }

  getSurveys(): Observable<Survey[]> {
    return this.http.get<Survey[]>(`${this.surveyUrl}/GetAllSurveys/`)
      .pipe(
        tap(_ => this.log('Fetch Survey')),
        catchError(this.handleError<Survey[]>('GetAllSurveys', []))
      );
  }


  getSurveyById(surveyId: string): Observable<Survey> {
    return this.http.get<Survey>(`${this.surveyUrl}/GetSurveyById/${surveyId}`).pipe(
      tap((x) => {
        this.log(`found survey matching "${surveyId}"`);
      }),
      catchError((error) => {
        this.handleError<Survey>('getSurveyById', {} as Survey);
        return EMPTY;
      })
    );
  }

  getAllAnswers(): Observable<Answer[]> {
    return this.http.get<Answer[]>(`${this.answerUrl}/GetAllAnswers/`)
      .pipe(
        tap(_ => this.log('Fetch Answers')),
        catchError(this.handleError<Answer[]>('GetAllAnswers', []))
      );
  }

  getAnswerById(answerId: string): Observable<Answer> {
    return this.http.get<Answer>(`${this.answerUrl}/GetAnswerById/${answerId}`).pipe(
      tap((x) => {
        this.log(`found Answer matching "${answerId}"`);
      }),
      catchError((error) => {
        this.handleError<Answer>('GetAnswerById', {} as Answer);
        return EMPTY;
      })
    );
  }

  addAnswer(answer: Answer): Observable<Answer> {
    return this.http.post<Answer>(`${this.answerUrl}/AddAnswer/`, answer).pipe(
      tap((newAnswer: Answer) => {
        console.log(`added new Answer. w/ id=${newAnswer.id}`);
        return newAnswer;
      }),
      catchError((error) => {
        this.log('Fail to add answer. ' + error.message);
        return EMPTY;
      })
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation: string = 'operation', result?: T): (error: any) => Observable<T> {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      this.log(`${operation} failed: ${error.message}`);
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a SurveyService message with the MessageService */
  private log(message: string): void {
    this.messageService.add(`SurveyService: ${message}`);
  }
}
