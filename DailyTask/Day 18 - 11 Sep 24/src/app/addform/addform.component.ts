import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { dessert } from '../desserts/desserts.component';

@Component({
  selector: 'app-addform',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './addform.component.html',
  styleUrl: './addform.component.css'
})
export class AddformComponent {
  @Output() add :EventEmitter<dessert>= new EventEmitter();

  addDessert(addform:any){
    console.log(addform.value);
    this.add.emit(addform.value);
    addform.reset()
  }

}
