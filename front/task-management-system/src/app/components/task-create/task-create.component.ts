import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

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
  styleUrls: ['./dialoge-create-task.scss']
})
export class DialogElement implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;

constructor(private _formBuilder: FormBuilder) {}
  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }
}