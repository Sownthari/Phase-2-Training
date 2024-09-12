import { Routes } from '@angular/router';
import { OrganizationComponent } from './organization/organization.component';
import { OrganizationDetailsComponent } from './organization-details/organization-details.component';
import { AddOrgComponent } from './add-org/add-org.component';
import { DeleteOrgComponent } from './delete-org/delete-org.component';
import { UpdateOrgComponent } from './update-org/update-org.component';

export const routes: Routes = [
    {path: '', component: OrganizationComponent},
    {path: 'organization/:id', component:OrganizationDetailsComponent},
    {path: 'add',component:AddOrgComponent},
    {path: 'update/:id',component:UpdateOrgComponent},
    {path: 'delete/:id',component:DeleteOrgComponent}
];
