import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { OrganizationComponent } from './organization/organization.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, OrganizationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EmployeeApplication';
}
