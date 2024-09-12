import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnimalsComponent } from './animals/animals.component';
import { CommonModule } from '@angular/common';
import { DessertsComponent } from './desserts/desserts.component';
import { DirectivesComponent } from './directives/directives.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AnimalsComponent, CommonModule, DessertsComponent, DirectivesComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Display';
}
