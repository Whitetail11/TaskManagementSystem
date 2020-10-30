import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {

  constructor() { }

  @Input() collectionSize: number;
  @Input() pageSize: number;
  @Input() pageNumber: number;
  @Output() pageSizeChange = new EventEmitter<number>(); 
  @Output() pageChange = new EventEmitter<number>(); 

  ngOnInit(): void {
  }

  onPageSizeChange() {
    this.pageSizeChange.emit(this.pageSize);
  }

  onPageChange() {
    this.pageChange.emit(this.pageNumber)
  }
}
