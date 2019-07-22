import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SchedulerService } from '../scheduler/scheduler.service';
import { FormGroup, FormControl } from '@angular/forms';
import { IAppointment } from './appointment.model';
import { IUser } from './user.model';

@Component({
	selector: 'appointmentadd',
	templateUrl: './appointmentadd.component.html'
})

export class AppointmentAddComponent implements OnInit {
	error: string;
	foundError: boolean;
	insertForm: FormGroup;
	startHour: number;
	endHour: number;
	startHourString: string;
	endHourString: string;

	constructor(private route: ActivatedRoute, private router: Router, private schedulerService: SchedulerService) { }

	ngOnInit() {
		this.foundError = false;
		this.startHour = 8;
		this.endHour = 9;
		this.startHourString = "08:00";
		this.endHourString = "09:00";

		// Init insert form
		this.insertForm = new FormGroup({
			Name: new FormControl(''),
			StartHour: new FormControl(this.startHour),
			EndHour: new FormControl(this.endHour)
		});
	}

	startHourChanged(event) {
		this.startHour=parseInt(event.target.value);
		this.startHourString=event.target.options[event.target.selectedIndex].innerHTML;
		this.validateHours();
	}

	endHourChanged(event) {
		this.endHour=parseInt(event.target.value);
		this.endHourString=event.target.options[event.target.selectedIndex].innerHTML;
		this.validateHours();
	}

	// Validation - Doesn't work
	validateHours()
	{
		this.foundError = (parseInt(this.startHour) >= parseInt(this.endHour));
		console.log(this.startHour, this.endHour, (this.startHour >= this.endHour), this.foundError)
	}

	// Inserts a new appointment with the insert form values
	addAppointmentSubmit() {
		// Validate data
		if (this.insertForm.valid) {
			let appointment: IAppointment = {
				User: { Name: this.insertForm.value.Name },
				StartHour: this.startHourString,
				EndHour: this.endHourString
			}

			this.addAppointment(appointment);
		}
	}

	addAppointment(appointment: IAppointment) {
		this.schedulerService.addAppointment(appointment).subscribe(
			data => {
				this.router.navigateByUrl('/scheduler');
			},
			error => {
				this.error = error.error.ExceptionMessage;
				console.log("insert error", error)
			}
		);
	}

}