import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfirmEmail } from 'src/app/models/confirmEmail';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  result: string;
  succeeded: boolean = false;

  constructor(private route: ActivatedRoute, private accountService: AccountService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.confirmEmail(params['userId'], params['code']);
    });
  }

  confirmEmail(userId: string, code: string) {
    const confirmEmail: ConfirmEmail = { userId: userId, code: code };
    this.accountService.confirmEmail(confirmEmail).subscribe(() => {
      this.result = 'Your email address has been successfuly confirmed.';
      this.succeeded = true;
    }, (error) => {
      this.result = error.error[0];
    });
  } 
}
