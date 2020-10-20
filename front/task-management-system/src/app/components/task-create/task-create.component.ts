import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';
import { Task } from '../../models/task';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.scss']
})
export class TaskCreateComponent implements OnInit {

  constructor(public dialog: MatDialog,) { }

  openDialog() {
    this.dialog.open(DialogElement);
  }
  ngOnInit(): void {
  }

}

@Component({
  selector: 'dialoge-create-task',
  templateUrl: 'dialoge-create-task.html',
  styleUrls: ['./dialoge-create-task.scss'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: {showError: true}
  }]
})
export class DialogElement implements OnInit {
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  task: Task = {
    id: 1,
    Description:  '',
    DeadLine:  new Date(),
    Date : new Date(),
    Title : '',
    CreatorId :  '',
    ExecutorId : ''
  };

constructor(private _formBuilder: FormBuilder) {}
  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
      secondCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
      fourthCtrl: ['', Validators.required]
    });
  }
  createTask() {
    if (this.firstFormGroup.valid && this.secondFormGroup.valid) {
      this.task.Title = this.firstFormGroup.value.firstCtrl;
      this.task.Description = this.firstFormGroup.value.secondCtrl;
      this.task.DeadLine = new Date(this.secondFormGroup.value.thirdCtrl);
      this.task.Date = new Date();
      console.log(this.task);
    }
    else {

    }
  }
}