import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SelectUser } from 'src/app/models/selectUser';
import { Status } from 'src/app/models/status';
import { AccountService } from 'src/app/services/account.service';
import { StatusService } from 'src/app/services/status.service';

@Component({
  selector: 'app-task-filter',
  templateUrl: './task-filter.component.html',
  styleUrls: ['./task-filter.component.scss']
})
export class TaskFilterComponent implements OnInit {

  filtersForm: FormGroup;
  executors: SelectUser[] = [];
  statuses: Status[] = [];
  displayFiltersMenu: boolean = false;
  @Output() filterApply = new EventEmitter<any>();

  constructor(private accountService: AccountService,
     private statusService: StatusService) { }

  ngOnInit(): void {
    this.filtersForm = new FormGroup({
      title: new FormControl(''),
      statusIds: new FormControl(),
      executorId: new FormControl(),
      deadline: new FormControl(null)
    });

    this.setStatuses();

    if (!this.isExecutor) {
      this.setExecutors;
    }
  }

  setExecutors() {
    this.accountService.getExecutorsForSelect().subscribe((data: SelectUser[]) => {
      this.executors = data;
    });
  }

  setStatuses() {
    this.statusService.get().subscribe((data: Status[]) => {
      this.statuses = data;
    })
  }

  onFilterApply() {
    this.filterApply.emit(this.getFilters());
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

  toggleFiltersMenu() {
    this.displayFiltersMenu = !this.displayFiltersMenu;

    if (!this.displayFiltersMenu && this.filtersForm.get('deadline').invalid) {
      this.filtersForm.get('deadline').reset();
    }
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
