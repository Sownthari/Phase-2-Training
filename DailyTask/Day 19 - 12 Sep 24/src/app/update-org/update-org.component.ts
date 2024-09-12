import { Component } from '@angular/core';
import { Company } from '../Company';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../api.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-org',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-org.component.html',
  styleUrl: './update-org.component.css'
})
export class UpdateOrgComponent {
  org: Company = {'id':0, name:''}
  constructor(private apiService: ApiService, private route: ActivatedRoute, private router:Router){}
  ngOnInit(){
    const id = +this.route.snapshot.params['id'];
    this.apiService.getById(id).subscribe(
      (response) => {
        this.org = response
      }
    );
  }
  onSubmit(){
    console.log(this.org.id, this.org.name);
    this.apiService.updateOrg(this.org.id,this.org).subscribe(
      (response) => {
        console.log("Organization updated successfully",response);
        this.router.navigate(['/']);
      }
    )
  }
}
