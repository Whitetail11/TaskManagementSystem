import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Status } from 'src/app/models/status';
import { StatusService } from 'src/app/services/status.service';

@Component({
  selector: 'app-status-menu',
  templateUrl: './status-menu.component.html',
  styleUrls: ['./status-menu.component.scss']
})
export class StatusMenuComponent implements OnInit {

  constructor(
    private statusService: StatusService,
  ) { }
  @Input() statusId: number;
  @Input() taskId: number;
  @Output() onStatusChange = new EventEmitter<any>()
  Statuses: Status[]
  CurrentStatus: Status

  ngOnInit(): void {
    this.statusService.get().subscribe((data) => {
      this.Statuses = data;
      this.setCurrStatus();
    })
  }

  setCurrStatus() {
    this.Statuses.forEach(status => {
      if(status.id === this.statusId) {
        this.CurrentStatus = status;
      }
    });
  }

  changeStatus(statusId: number) {
    this.statusId = statusId;
    this.setCurrStatus();
    this.statusService.changeStatus(this.taskId, statusId).subscribe(() => {
      this.onStatusChange.emit();
    })
  }

}
