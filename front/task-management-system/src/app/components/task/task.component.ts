import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    private toastrService: ToastrService, private router: Router) { 
    }

  task: ShowTask;
  replyCommentId: number;
  showRepliesOfComments: number[] = [];
  files: any
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.setTask(+params['id']);
      this.taskService.downloadFiles(+params['id']).subscribe((data) => {
        this.files = data.body;
        console.log(this.files);
      }, error => {
        console.log(error)
        this.files = null;
      })
    });
  }
  downLoadFile() {
    if(this.files !== null)
    {
      let blob = new Blob([this.files], { type: "application/zip"});
      let url = window.URL.createObjectURL(blob);
      let pwa = window.open(url);
      if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
          alert( 'Please disable your Pop-up blocker and try again.');
      }
    }
    else {
      this.toastrService.error('This task has no files.', '', {
        timeOut: 5000
      });
    }
  }
  statusChange() {
    console.log('status changed')
  }
  setTask(id: number) {
    this.taskService.getForShowig(id).subscribe(
      (data: ShowTask) => {
        this.task = data;
        console.log(this.task);
      }, (error) => {
        if (error.status == 404) {
          this.router.navigate(['not-found']);
        }
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
  updateTasks() {
    console.log('task updated')
  }
  deleteTask() {
    console.log('task deleted')
  }
  public get userId() {
    return this.accountService.getUserId();
  }
  public get isExecutor(): boolean {
    return this.accountService.isExecutor();
  }
}
