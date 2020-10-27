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
  tasks: TaskShortInfo[] = [];

  ngOnInit(): void {
    this.taskService.getForPage(1, 1).subscribe((data: TaskShortInfo[]) => 
    {
      console.log(data);
    });
  }
}
