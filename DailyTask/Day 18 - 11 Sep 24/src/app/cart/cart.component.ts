import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { dessert } from '../desserts/desserts.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  @Input() productsInCart: any[] = [];
  @Output() emptyCart : EventEmitter<dessert[]>= new EventEmitter();

  getTotalPrice(): number {
    return this.productsInCart.reduce((total, product) => {
      return total + (product.dessertPrice * product.count);
    }, 0);
  }

  buyNow() {
    alert("Proceeding to checkout...");
    this.emptyCart.emit([]);
  }

}
