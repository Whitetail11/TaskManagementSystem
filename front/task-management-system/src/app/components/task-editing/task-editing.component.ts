import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Task } from '../../models/task';
import { AccountService } from '../../services/account.service';
import { TaskService } from 'src/app/services/task.service';
import {FormControl, FormGroupDirective, NgForm} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxFileDropEntry, FileSystemFileEntry, FileSystemDirectoryEntry } from 'ngx-file-drop';
import { SelectUser } from 'src/app/models/selectUser';

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
    private toastrService: ToastrService
    ) {
      const currentYear = new Date().getFullYear();
      const currentDay = new Date().getDate();
      const currentMonth = new Date().getMonth();
      this.minDate = new Date(currentYear, currentMonth, currentDay);
      this.isThirdStep = false;
      this.files = this.data.task.files;
      this.deletedFiles = []
     }
    isLinear = true;
    firstFormGroup: FormGroup;
    secondFormGroup: FormGroup;
    selectedExecutor: string;
    minDate: Date;
    ExecutorEmail: '';
    errorMessage: '';
    Executors: SelectUser[]
    isThirdStep: Boolean;
    files: any;
    deletedFiles: number[];
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
    this._accountService.getExecutorsForSelect().subscribe((data) => {
      this.Executors = data;
    })
    console.log(this.data);
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
  public Newfiles: NgxFileDropEntry[] = [];

  public dropped(files: NgxFileDropEntry[]) {
    let res = true
    this.Newfiles.forEach(currentFile => {
      files.forEach(inputFile => {
        if(currentFile.relativePath === inputFile.relativePath) { res = false;}
      });
    });
    if(res)
    {
      this.Newfiles = this.Newfiles.concat(files);
    }
    console.log(this.Newfiles)
  }
  deleteNewFile(name: string) {
    this.Newfiles = this.Newfiles.filter((item) => item.relativePath !== name);
  }
  thirdstep()
  {
    if(this.secondFormGroup.valid)
      this.isThirdStep = true;
  }
  deleteFileFromForm(id: number) {
    this.deletedFiles.push(id);
    this.files = this.files.filter((item) => item.id !== id)
  }
  deleteFile(id: number) {
    this._taskService.deletefile(id).subscribe(() => {
    })
  }
  public fileOver(event) {
    console.log(event);
  }

  public fileLeave(event) {
    console.log(event);
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
      this._taskService.editTask(this.task).subscribe((id) => {
        if(this.deletedFiles.length) 
        {
          this.deletedFiles.forEach(fileId => {
            this.deleteFile(fileId)
          });
        }
        if(this.Newfiles.length)
        {
          for (const droppedFile of this.Newfiles) {
            if (droppedFile.fileEntry.isFile) {
              const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
              const formData = new FormData()
              fileEntry.file((file: File) => {
                formData.append('Data', file, droppedFile.relativePath)
                this._taskService.postFile(formData, id).subscribe(() => {
                })
              });
            } else {
              // It was a directory (empty directories are added, otherwise only files)
              const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
            }
          }
          this.toastrService.success('Task has been successfuly updated.', '');
        } else {
          this.toastrService.success('Task has been successfuly updated.', '');
        }
      }, error => {
        this.errorMessage = error.error.message;
      });
    }
  }
}
