import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SurveyService} from '../../Services/survey.service';
import {MessageService} from '../../Services/message.service';
import {Location} from '@angular/common';
import {Customise} from '../../Models/customise';

@Component({
  selector: 'app-customise-detail',
  templateUrl: './customise-detail.component.html',
  styleUrls: ['./customise-detail.component.css']
})
export class CustomiseDetailComponent implements OnInit {

  constructor(
      private route: ActivatedRoute,
      private characterService: SurveyService,
      private messageService: MessageService,
      private location: Location
  ) { }

  answerId: string;
  // customise: Customise;


  ngOnInit(): void {
    this.answerId = this.route.snapshot.paramMap.get('id');
    // this.getCustomise(this.customiseId);
  }

  getCustomise(id: string): void {
    // this.characterService.getSurveyById(id)
    //     .subscribe(c => {
    //       this.customise = c;
    //     });
  }

  back(): void {
    this.location.back();
  }

}
