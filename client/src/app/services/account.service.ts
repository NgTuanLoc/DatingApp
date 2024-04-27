import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';

@Injectable({
	providedIn: 'root',
})
export class AccountService {
	baseUrl = environment.apiUrl;
	private currentUserSource = new BehaviorSubject<User | null>(null);
	currentUserSource$ = this.currentUserSource.asObservable();

	constructor(private http: HttpClient) {}

	login(model: any) {
		return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
			map((response) => {
				const user = response;
				if (!user) return;
				localStorage.setItem('user', JSON.stringify(user));
				this.currentUserSource.next(user);
			})
		);
	}

	register(model: any) {
		return this.http
			.post<User>(this.baseUrl + 'account/register', model)
			.pipe(
				map((user) => {
					if (!user) return;

					localStorage.setItem('user', JSON.stringify(user));
					this.currentUserSource.next(user);
					return user;
				})
			);
	}

	setCurrentUser(user: User) {
		this.currentUserSource.next(user);
	}

	logout() {
		this.currentUserSource.next(null);
		localStorage.removeItem('user');
	}
}
