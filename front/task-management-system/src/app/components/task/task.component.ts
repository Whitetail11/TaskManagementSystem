import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShowTask } from 'src/app/models/showTask';
import { CommentService } from 'src/app/services/comment.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  constructor(private route: ActivatedRoute, private taskService: TaskService,
    private commentService: CommentService) { 
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
    this.taskService.getComments(this.task.id).subscribe((data) => {
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

  onCommentCreate() {
    this.setComments();
    this.replyCommentId = null;
  }

  onCommentCancel() {
    this.replyCommentId = null;
  }
}
