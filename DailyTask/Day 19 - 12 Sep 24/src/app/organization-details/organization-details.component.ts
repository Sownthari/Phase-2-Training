import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../Company';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-organization-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './organization-details.component.html',
  styleUrl: './organization-details.component.css'
})
export class OrganizationDetailsComponent {
  organization: Company | undefined
  constructor(private apiService: ApiService, private route:ActivatedRoute){}
  ngOnInit():void{
    const id = +this.route.snapshot.params['id'];
    this.apiService.getById(id).subscribe(
      (response) => {
        this.organization = response
      }
    );
  }
}
