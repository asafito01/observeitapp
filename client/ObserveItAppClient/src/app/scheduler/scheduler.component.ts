import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SchedulerService } from './scheduler.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ISchedulerRow } from './scheduler.model';

@Component({
	selector: 'scheduler',
	templateUrl: './scheduler.component.html'
})

export class SchedulerComponent implements OnInit {
	schedulerRows: ISchedulerRow[];
	error: string;

	constructor(private route: ActivatedRoute, private schedulerService: SchedulerService) { }

	ngOnInit() {
		this.getSchedulerRows();
	}

	getSchedulerRows() {
		setInterval(() =>
			this.schedulerService.getSchedulerRows().subscribe(
				data => {
					this.schedulerRows = data
				},
				error => {
					this.error = error.error.ExceptionMessage;
					console.log(error)
				}
			),
			1000);
	}
}