import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
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
  selector: 'app-task-editing',
  templateUrl: './task-editing.component.html',
  styleUrls: ['./task-editing.component.scss'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true }
  }]
})

export class TaskEditingComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<TaskEditingComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
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
    isLinear = true;
    firstFormGroup: FormGroup;
    secondFormGroup: FormGroup;
    selectedExecutor: string;
    minDate: Date;
    ExecutorEmail: '';
    errorMessage: '';
    Executors: Executor[];
    isThirdStep: Boolean;
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
  ngOnInit(): void {
    this._userService.get().subscribe((data) => {
      this.Executors = data;
    })
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: [this.data.task.title, Validators.required],
      secondCtrl: [this.data.task.description, Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      thirdCtrl: new FormControl (new Date(this.data.task.deadline), [
        Validators.required
      ]),
      fourthCtrl: new FormControl (this.data.executor.id, [
        Validators.required
      ]
      )
    });
  }
  editTask()
  {
    if (this.firstFormGroup.valid && this.secondFormGroup.valid) {
      this.task.id = this.data.task.id;
      this.task.title = this.firstFormGroup.value.firstCtrl;
      this.task.description = this.firstFormGroup.value.secondCtrl;
      this.task.deadline = new Date(this.secondFormGroup.value.thirdCtrl);
      this.task.date = this.data.task.date;
      this.task.creatorId = this.data.task.creatorId;
      this.task.executorId = this.secondFormGroup.value.fourthCtrl;
      this._taskService.editTask(this.task).subscribe(() => {
        this.errorMessage = '';
        this.toastrService.success('Task has been successfuly edited.', '');
      }, error => {
        this.errorMessage = error.error.message;
      });
    }
  }
}
