import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { MatDialog } from '@angular/material/dialog';
import { TaskEditingComponent } from '../task-editing/task-editing.component';
import { SelectUser } from 'src/app/models/selectUser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-task-menu',
  templateUrl: './task-menu.component.html',
  styleUrls: ['./task-menu.component.scss']
})
export class TaskMenuComponent implements OnInit {

  constructor(
    private taskService: TaskService,
    private accountService: AccountService,
    public dialog: MatDialog,
  ) {
   }
   @Output() onCloseDialogue = new EventEmitter<any>();
   @Output() onDeleteTask = new EventEmitter<any>();
   @Input() taskId: number;
   Executors: SelectUser[];
  ngOnInit(): void {
    this.accountService.getExecutorsForSelect().subscribe((data) => {
      this.Executors = data;
    })
  }
  openEditTaskDialogue() : void 
  {
    this.taskService.getTaskById(this.taskId).subscribe((task) => {
      const executor = this.Executors.find(Item => Item.id == task.executorId)
      const dialogRef = this.dialog.open(TaskEditingComponent, {
        data: {task, executor}
      })
      dialogRef.componentInstance.ontaskEdited.subscribe(() => {
        this.onCloseDialogue.emit()
      });
    })
  }
  deleteTask()
  {
    this.taskService.deleteTask(this.taskId).subscribe(() => {
      this.onDeleteTask.emit()
    })
  }
}
