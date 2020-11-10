import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Task } from '../../models/task';
import { AccountService } from '../../services/account.service';
import { TaskService } from 'src/app/services/task.service';
import {FormControl, FormGroupDirective, NgForm} from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { Executor } from 'src/app/models/Executor';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.scss']
})
export class TaskCreateComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  openDialog() {
    const dialogRef = this.dialog.open(DialogElement);
    dialogRef.afterClosed().subscribe(() => {
      console.log("task created!");
    })
  }
  ngOnInit(): void {
  }

}

@Component({
  selector: 'dialoge-create-task',
  templateUrl: 'dialoge-create-task.html',
  styleUrls: ['./dialoge-create-task.scss'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true }
  }]
})
export class DialogElement implements OnInit {
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  selectedExecutor: string;
  task: Task = {
    id: 0,
    description: '',
    deadline: new Date(),
    date: new Date(),
    title: '',
    creatorId: '',
    executorId: '',
    statusId: 0
  };
  minDate: Date;
  ExecutorEmail: '';
  errorMessage: '';
  Executors: Executor[];
  isThirdStep: Boolean;
  constructor(
    private _formBuilder: FormBuilder,
    private _accountService: AccountService,
    private _taskService: TaskService,
    private _userService: UserService,
    private toastrService: ToastrService
  ) {
    const currentYear = new Date().getFullYear();
    const currentDay = new Date().getDate();
    const currentMonth = new Date().getMonth();
    this.minDate = new Date(currentYear, currentMonth, currentDay);
    this.isThirdStep = false;
   }
  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
      secondCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
      fourthCtrl: new FormControl ('', [
        Validators.required
      ]
      )
    });
    this._userService.get().subscribe((data) => {
      this.Executors = data;
    })
  }
  createTask() {
    if (this.firstFormGroup.valid && this.secondFormGroup.valid) {
      this.task.title = this.firstFormGroup.value.firstCtrl;
      this.task.description = this.firstFormGroup.value.secondCtrl;
      this.task.deadline = new Date(this.secondFormGroup.value.thirdCtrl);
      this.task.date = new Date();
      this.task.creatorId = this._accountService.getUserId();
      this.task.executorId = this.secondFormGroup.value.fourthCtrl;
      console.log(this.task);
      this._taskService.post(this.task).subscribe(() => {    
        this.errorMessage = '';
        this.toastrService.success('Task has been successfuly created.', '');
      }, error => {
        this.errorMessage = error.error.message;
      })
    }
  }
  
  showToastr() {
    this.toastrService.success('Task has been successfuly created.', '', {
      timeOut: 30000,
    });
  }
}