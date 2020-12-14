import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MessageService} from './message.service';
import {EMPTY, Observable, of} from 'rxjs';
import {catchError, mapTo, tap} from 'rxjs/operators';
import {Survey} from '../Models/survey';
import {environment as testEnv} from '../../environments/environment';
import {Customise} from '../Models/customise';

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

    getAllAnswers(): Observable<Customise[]> {
        return this.http.get<Customise[]>(`${this.answerUrl}/GetAllCustomise/`)
            .pipe(
                tap(_ => this.log('Fetch Characters')),
                catchError(this.handleError<Customise[]>('getAllCustomise', []))
            );
    }

    getAnswerById(customiseId: string): Observable<Customise> {
        return this.http.get<Customise>(`${this.answerUrl}/GetCustomiseById/${customiseId}`).pipe(
            tap((x) => {
                this.log(`found Customise matching "${customiseId}"`);
            }),
            catchError((error) => {
                this.handleError<Customise>('getCustomiseById', {} as Customise);
                return EMPTY;
            })
        );
    }

    addAnswer(customise: Customise): Observable<Survey> {
        return this.http.post<Survey>(`${this.answerUrl}/CreateCustomerCharacter/`, customise).pipe(
            tap((newCharacter: Survey) => {
                console.log(`added new Customise Character. w/ id=${newCharacter.id}`);
                return newCharacter;
            }),
            catchError((error) => {
                this.log('Fail to add customise. ' + error.message);
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
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log a CharacterService message with the MessageService */
    private log(message: string) {
        this.messageService.add(`CharacterServices: ${message}`);
    }
}
