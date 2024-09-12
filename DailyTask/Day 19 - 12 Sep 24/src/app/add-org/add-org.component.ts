import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Company } from '../Company';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-org',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './add-org.component.html',
  styleUrl: './add-org.component.css'
})
export class AddOrgComponent {
  org : Company = {id : 0, name : ""}

  constructor(private apiService: ApiService, private router:Router){}

  onSubmit():void{
    this.apiService.addOrg(this.org).subscribe(
      (response) => {
        console.log("Organization added successfully");
        this.router.navigate(['/']);
      }
    );
  }
}
