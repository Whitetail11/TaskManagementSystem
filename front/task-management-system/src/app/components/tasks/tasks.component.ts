import { Component, OnInit } from '@angular/core';
import { TaskShortInfo } from 'src/app/models/taskShortInfo';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  constructor(private taskService: TaskService) { }
  
  pageTasks: TaskShortInfo[] = [];
  taskCount: number;
  pageSize: number = 20;
  pageNumber: number = 1;

  ngOnInit(): void {
    this.setTaskCount();
    this.setTasks();
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
}
