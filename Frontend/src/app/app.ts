import {Component} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {MatIcon} from '@angular/material/icon';

@Component({
	selector: 'app-root',
	imports: [RouterOutlet, RouterLink, MatIcon],
	templateUrl: './app.html',
	styleUrl: './app.css'
})
export class App {
}
