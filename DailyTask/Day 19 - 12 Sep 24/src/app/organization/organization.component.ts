import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrganizationDetailsComponent } from "../organization-details/organization-details.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-organization',
  standalone: true,
  imports: [CommonModule, FormsModule, OrganizationDetailsComponent],
  templateUrl: './organization.component.html',
  styleUrl: './organization.component.css'
})
export class OrganizationComponent {
  data : any
  constructor(private apiService: ApiService,private router:Router){}

  ngOnInit():void{
    this.loadCompanies();
  }
  viewDetails(id: number): void {
    this.router.navigate(['/organization', id]);
  }

  updateCompany(id: number): void {
    this.router.navigate(['/update', id]);
  }
  addCompany(): void {
    this.router.navigate(['/add']);
  }
  deleteCompany(id: number): void {
    if (confirm('Are you sure you want to delete this company?')) {
      this.apiService.deleteOrg(id).subscribe(
        () => {
          console.log('Organization deleted successfully');
          this.loadCompanies(); 
        },
        (error) => console.error('Error deleting organization', error)
      );
    }
  }
  loadCompanies() {
    this.apiService.get().subscribe(
      (response) => {
        this.data = response;
      }
    );
  }
}
