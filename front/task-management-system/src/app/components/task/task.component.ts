import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ShowTask } from 'src/app/models/showTask';
import { AccountService } from 'src/app/services/account.service';
import { CommentService } from 'src/app/services/comment.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  constructor(private route: ActivatedRoute, private taskService: TaskService,
    private commentService: CommentService, private accountService: AccountService,
    private toastrService: ToastrService) { 
      this.route.params.subscribe(params => {
        this.id = +params['id'];
      });
    }

  id: number;
  task: ShowTask;
  replyCommentId: number;
  showRepliesOfComments: number[] = [];

  ngOnInit(): void {
    this.setTask(this.id);
  }

  setTask(id: number) {
    this.taskService.getForShowig(id).subscribe((data: ShowTask) => {
      this.task = data;
    });
  }

  setComments() {
    this.commentService.getForTask(this.task.id).subscribe((data) => {
      this.task.comments = data;
    });
  }

  replyToComment(commentId) {
    this.replyCommentId = commentId;
  }

  showReplies(commentId: number) {
    this.showRepliesOfComments.push(commentId);
  }

  hideReplies(commentId: number) {
    this.showRepliesOfComments = this.showRepliesOfComments.filter((e) => e != commentId);
  }

  deleteComment(commentId) {
    this.commentService.delete(commentId, this.task.id).subscribe(() => {
      this.setComments();
      this.toastrService.success('Comment has been successfuly deleted.', '', {
        timeOut: 5000
      });
    }, () => {
      this.toastrService.error('Comment has not been deleted.', '', {
        timeOut: 5000
      });
    });
  }

  onCommentCreate() {
    this.setComments();
    this.replyCommentId = null;
  }

  onCommentCancel() {
    this.replyCommentId = null;
  }

  public get userId() {
    return this.accountService.getUserId();
  }
}
