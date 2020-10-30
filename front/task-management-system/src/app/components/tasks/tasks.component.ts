import { Component, OnInit } from '@angular/core';
import { TaskShortInfo } from 'src/app/models/taskShortInfo';
import { AccountService } from 'src/app/services/account.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  constructor(private taskService: TaskService, private accountService: AccountService) { }
  
  pageTasks: TaskShortInfo[] = [];
  taskCount: number;
  pageSize: number = 20;
  pageNumber: number = 1;

  ngOnInit(): void {
    this.setTaskCount();
    this.setTasks();
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

  onPageChange(pageNumber) {
    this.pageNumber = pageNumber;
    this.setTasks()
  }

  onPageSizeChange(pageSize) {
    this.pageNumber = 1;
    this.pageSize = pageSize;
    this.setTasks();
  }
  
  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
