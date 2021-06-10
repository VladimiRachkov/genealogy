import { CreateMessage, GetUser } from '@actions';
import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BusinessObjectOutDto, UserFilter } from '@models';
import { Store } from '@ngxs/store';
import { UserState } from '@states';
import { METATYPE_ID } from 'app/enums/metatype';
import { ModalComponent } from '../modal/modal.component';
import { isNil } from 'lodash';
import { AuthenticationService } from '@core';
import { NotifierService } from 'angular-notifier';
import { NOTIFICATIONS } from '@enums';

const metatypeId = METATYPE_ID.MESSAGE;

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss'],
})
export class FeedbackComponent implements OnInit {
  @ViewChild(ModalComponent, { static: false }) feedbackModal: ModalComponent;

  feedbackForm: FormGroup;

  submitted = false;

  error: string;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store,
    private authService: AuthenticationService,
    private notifierService: NotifierService
  ) {}

  ngOnInit() {
    this.feedbackForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      title: [''],
      message: ['', Validators.required],
    });

    const id = this.authService.getUserId();

    if (!isNil(id)) {
      const userFilter: UserFilter = { id };
      this.store.dispatch(new GetUser(userFilter)).subscribe(() => {
        const user = this.store.selectSnapshot(UserState.user);

        if (!isNil(user)) {
          const username = `${user.lastName} ${user.firstName}`;
          this.feedbackForm.patchValue({ username, email: user.email });
        }
      });
    }
  }

  open(title: string = 'Без темы') {
    this.feedbackForm.patchValue({ title });
    this.feedbackModal.open();
  }

  onSubmit() {
    this.submitted = true;

    if (this.feedbackForm.invalid) {
      this.notifierService.notify('error', NOTIFICATIONS.INVALID_FORM, 'MAIL');
      return;
    }

    const { username, email, message, title } = this.feedbackForm.value;

    const data = JSON.stringify({ username, email, message });

    const body: BusinessObjectOutDto = { title, metatypeId, data };
    this.store.dispatch(new CreateMessage(body)).subscribe(data => {
      this.feedbackModal.close();
      this.notifierService.notify('success', NOTIFICATIONS.MESSAGE_SENT, 'MAIL');
    });
  }
}
