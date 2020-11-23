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
  page = { size: 20, number: 1 };
  taskCount: number = 0;
  totalTaskCount: number = 0;
  displayFiltersMenu: boolean = false;
  filters: any;  

  constructor(private taskService: TaskService,
     private accountService: AccountService,
     public dialog: MatDialog
     ) { }

  ngOnInit(): void {
    this.updatePage();
    this.setTotalTaskCount();
  }

  updatePage() {
    this.setTaskCount();
    this.setTasks();
  }

  onTaskUpdate() {
    this.setTasks();
  }

  setTotalTaskCount() {
    this.taskService.getTaskCount({})
      .subscribe((data: number) => {
        this.totalTaskCount = data;
    });
  }

  setTaskCount() {
    this.taskService.getTaskCount(this.filters)
      .subscribe((data: number) => {
        this.taskCount = data;
    });
  }

  setTasks() {
    this.taskService.getForPage(this.page, this.filters)
      .subscribe((data: ShowTaskShortInfo[]) => {
        this.pageTasks = data;
    });
  }

  onPageChange(pageNumber) {
    this.page.number = pageNumber;
    this.setTasks()
  }

  onPageSizeChange(pageSize) {
    this.page.number = 1;
    this.page.size = pageSize;
    this.setTasks();
  }

  onFilterApply(filters) {
    this.filters = filters;
    this.page.number = 1;
    this.setTaskCount();
    this.setTasks();
  }

  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
