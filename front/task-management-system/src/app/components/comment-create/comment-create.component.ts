import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CreateComment } from 'src/app/models/createComment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment-create',
  templateUrl: './comment-create.component.html',
  styleUrls: ['./comment-create.component.scss']
})
export class CommentCreateComponent implements OnInit {

  constructor(private commentService: CommentService, 
    private toastrService: ToastrService, private ref: ChangeDetectorRef) { }

  @Input() taskId: number;
  @Input() replyCommentId: number;
  @Output() commentCreate = new EventEmitter();
  @Output() commentCancel = new EventEmitter();
  @ViewChild('commentText') commentText: ElementRef;
  commentForm: FormGroup;
  textareaFocused: boolean = false;

  ngOnInit(): void {
    this.commentForm = new FormGroup({
      commentText: new FormControl('')
    });
  }

  ngAfterViewInit() {
    console.log();
    if (this.replyCommentId) {
      this.commentText.nativeElement.focus();
      this.ref.detectChanges();
    }
  }

  public get width(): Number {
    return this.replyCommentId ? 40 : 50;
  } 

  add() {
    const comment: CreateComment = { 
      text: this.commentForm.get('commentText').value,  
      taskId: this.taskId,
      replyCommentId: this.replyCommentId
    };

    this.commentService.post(comment).subscribe(() => {
      this.showToastr();
      this.commentCreate.emit();
    });

    this.commentForm.reset();
  }

  cancel() {
    this.commentForm.reset();
    this.textareaFocused = false;
    this.commentCancel.emit();
  }

  focus() {
    this.textareaFocused = true;
  }

  blur() {
    setTimeout(() => {
      this.textareaFocused = false;
    }, 150);
  }

  showToastr() {
    this.toastrService.success('Comment has been successfuly added.', '', {
      timeOut: 5000,
    });
  }
}
