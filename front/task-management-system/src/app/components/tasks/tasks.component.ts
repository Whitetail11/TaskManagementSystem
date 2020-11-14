import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { SelectUser } from 'src/app/models/selectUser';
import { Status } from 'src/app/models/status';
import { ShowTaskShortInfo } from 'src/app/models/showTaskShortInfo';
import { AccountService } from 'src/app/services/account.service';
import { TaskService } from 'src/app/services/task.service';
import { MatDialog } from '@angular/material/dialog';
import { TaskEditingComponent } from 'src/app/components/task-editing/task-editing.component'
import { UserService } from 'src/app/services/user.service';
import { StatusService } from 'src/app/services/status.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  constructor(private taskService: TaskService,
     private statusService: StatusService,
     private accountService: AccountService,
     public dialog: MatDialog,
     private _userService: UserService
     ) { }
  
  pageTasks: ShowTaskShortInfo[] = [];
  statuses: Status[] = [];
  executors: SelectUser[] = [];
  taskPage = { pageSize: 20, pageNumber: 1 };
  taskCount: number;
  pageSize: number = 20;
  pageNumber: number = 1;
  Executors: SelectUser[];
  filtersForm: FormGroup;
  displayFiltersMenu: boolean = false;

  ngOnInit(): void {
    this.filtersForm = new FormGroup({
      title: new FormControl(),
      statusIds: new FormControl(),
      executorId: new FormControl(),
      deadline: new FormControl(null)
    });

    this.setTaskCount();
    this.setTasks();
    this.setStatuses();

    if (!this.isExecutor) {
      this.setExecutors();
    }
    this.accountService.getExecutorsForSelect().subscribe((data) => {
      this.Executors = data;
    })
  }
  onCloseDialogue() {
    console.log('dialog has been closed!');
  }
  updateTasks() {
    this.setTasks();
  }
  
  deleteTask()
  {
    this.taskService.getTaskCount(this.getFilters()).subscribe((data: number) => {
      this.taskCount = data;
      if (this.pageCount < this.pageNumber) {
        this.pageNumber = 1;
      }
      this.setTasks();
    });
  }

  setTaskCount() {
    this.taskService.getTaskCount(this.getFilters())
      .subscribe((data: number) => {
        this.taskCount = data;
    });
  }

  setTasks() {
    this.taskService.getForPage(this.taskPage, this.getFilters())
      .subscribe((data: ShowTaskShortInfo[]) => {
        this.pageTasks = data;
    });
  }

  setStatuses() {
    this.statusService.get().subscribe((data: Status[]) => {
      this.statuses = data;
    });
  }

  setExecutors() {
    this.accountService.getExecutorsForSelect().subscribe((data: SelectUser[]) => {
      this.executors = data;
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

  onFilterApply() {
    this.taskPage.pageNumber = 1;
    this.setTaskCount();
    this.setTasks();
  }

  toggleFiltersMenu() {
    this.displayFiltersMenu = !this.displayFiltersMenu;

    if (!this.displayFiltersMenu && this.filtersForm.get('deadline').invalid) {
      this.filtersForm.get('deadline').reset();
    }
  }

  getFilters() {
    let filters = Object.assign({}, this.filtersForm.value);
    if (filters.deadline != null) {
      let deadline = new Date(filters.deadline);
      filters.deadline = `${ deadline.getMonth() + 1 }.${ deadline.getDate() }.${ deadline.getFullYear() }`
    }

    for(let [key, value] of Object.entries(filters)) {
      if (value === null) {
        delete filters[key];
      }
    }
    return filters;
  }

  public get pageCount() {
    return (this.taskCount + this.pageSize - 1) / this.pageSize;
  }

  public get filterApplied(): boolean {
    return this.appliedFilterCount != 0;
  }
 
  public get appliedFilterCount(): number {
    let count = 0;
    for(let [key, value] of Object.entries(this.filtersForm.value)) {
      if (key !== 'title' && key !== 'statusIds' && value !== null) {
        count++;
      }
      else if (key === 'statusIds' && value !== null && (value as any[]).length !== 0) {
        count++;
      }
    }
    return count;
  }

  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
