import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
	registerMode = false;
	users: any;

	constructor(
		private http: HttpClient,
		private accountService: AccountService
	) {}
	ngOnInit(): void {
		this.getUsers();
	}

	getUsers() {
		this.http.get('http://localhost:5001/api/users').subscribe({
			next: (res) => (this.users = res),
			error: (err) => console.log(err),
			complete: () => console.log('Request as completed'),
		});
	}

	registerToggle() {
		this.registerMode = !this.registerMode;
	}

	cancelRegisterMode(event: boolean) {
		this.registerMode = event;
	}
}
