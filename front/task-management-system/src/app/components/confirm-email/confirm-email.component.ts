import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private accountService: AccountService) { }

  result: string = "Confirming...";

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.confirmEmail(params['userId'], encodeURIComponent(params['code']));
    });
  }

  confirmEmail(userId: string, code: string) {
    this.accountService.confirmEmail(userId, code).subscribe(() => {
      this.result = 'Your email address has been successfuly confirmed.';
    }, (error) => {
      this.result = error.error[0];
    });
  } 
}
