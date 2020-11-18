import { Component, OnInit } from '@angular/core';
import { ShowTaskShortInfo } from 'src/app/models/showTaskShortInfo';
import { AccountService } from 'src/app/services/account.service';
import { TaskService } from 'src/app/services/task.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  pageTasks: ShowTaskShortInfo[] = [];
  taskPage = { pageSize: 20, pageNumber: 1 };
  taskCount: number;
  pageSize: number = 20;
  pageNumber: number = 1;
  displayFiltersMenu: boolean = false;
  filters: any;

  constructor(private taskService: TaskService,
     private accountService: AccountService,
     public dialog: MatDialog
     ) { }

  ngOnInit(): void {
    this.updatePage();
  }

  updatePage() {
    this.setTaskCount();
    this.setTasks();
  }

  onTaskUpdate() {
    this.setTasks();
  }
  
  onTaskDelete()
  {
    this.taskService.getTaskCount(this.filters).subscribe((data: number) => {
      this.taskCount = data;
      if (this.pageCount < this.pageNumber) {
        this.pageNumber = 1;
      }
      this.setTasks();
    });
  }

  setTaskCount() {
    this.taskService.getTaskCount(this.filters)
      .subscribe((data: number) => {
        this.taskCount = data;
    });
  }

  setTasks() {
    this.taskService.getForPage(this.taskPage, this.filters)
      .subscribe((data: ShowTaskShortInfo[]) => {
        this.pageTasks = data;
    });
  }

  onPageChange(pageNumber) {
    this.taskPage.pageNumber = pageNumber;
    this.setTasks()
  }

  onPageSizeChange(pageSize) {
    this.taskPage.pageNumber = 1;
    this.taskPage.pageSize = pageSize;
    this.setTasks();
  }

  onFilterApply(filters) {
    this.filters = filters;
    this.taskPage.pageNumber = 1;
    this.setTaskCount();
    this.setTasks();
  }

  public get pageCount() {
    return (this.taskCount + this.pageSize - 1) / this.pageSize;
  }

  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
