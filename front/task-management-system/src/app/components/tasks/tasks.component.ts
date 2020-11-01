import { Component, OnInit } from '@angular/core';
import { TaskShortInfo } from 'src/app/models/taskShortInfo';
import { AccountService } from 'src/app/services/account.service';
import { TaskService } from 'src/app/services/task.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { TaskEditingComponent } from 'src/app/components/task-editing/task-editing.component'
import { Task } from 'src/app/models/task';
import { UserService } from 'src/app/services/user.service';
import { Executor } from 'src/app/models/Executor';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  constructor(private taskService: TaskService,
     private accountService: AccountService,
     public dialog: MatDialog,
     private _userService: UserService
     ) { }
  
  pageTasks: TaskShortInfo[] = [];
  taskCount: number;
  pageSize: number = 20;
  pageNumber: number = 1;
  Executors: Executor[];

  ngOnInit(): void {
    this.setTaskCount();
    this.setTasks();
    this._userService.get().subscribe((data) => {
      this.Executors = data;
    })
  }
  openEditTaskDialogue(taskId: number) : void 
  {
    this.taskService.getTaskById(taskId).subscribe((task) => {
      const executor = this.Executors.find(Item => Item.id == task.executorId)
      const dialogRef = this.dialog.open(TaskEditingComponent, {
        data: {task, executor}
      })
      dialogRef.afterClosed().subscribe(result => {
        this.setTaskCount();
        this.setTasks();
      });
    })
  }
  deleteTask(id: number)
  {
    this.taskService.deleteTask(id).subscribe(() => {
      this.setTaskCount();
      this.setTasks();
    })
  }
  setTaskCount() {
    this.taskService.getTaskCount().subscribe((data: number) => {
      this.taskCount = data;
    });
  }

  setTasks() {
    this.taskService.getForPage(this.pageNumber, this.pageSize).subscribe((data: TaskShortInfo[]) => {
      this.pageTasks = data;
    });
  }

  onPageChange() {
    this.setTasks()
  }

  onPageSizeChange() {
    this.pageNumber = 1;
    this.setTasks();
  }
  
  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
