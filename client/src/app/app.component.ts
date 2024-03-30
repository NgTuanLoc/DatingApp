import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
	title = 'client';
	users: any;
	constructor(private http: HttpClient) {}

	ngOnInit(): void {
		this.http.get('http://localhost:5001/api/users').subscribe({
			next: (res) => {
				this.users = res;
			},
			error: (err) => {
				console.log(err);
			},
			complete: () => {
				console.log('Request as completed');
			},
		});
	}
}
