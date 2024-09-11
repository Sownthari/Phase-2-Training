import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddformComponent } from "../addform/addform.component";
import { CartComponent } from '../cart/cart.component';

@Component({
  selector: 'app-desserts',
  standalone: true,
  imports: [CommonModule, FormsModule, AddformComponent, CartComponent],
  templateUrl: './desserts.component.html',
  styleUrl: './desserts.component.css'
})
export class DessertsComponent {

  isLogged:boolean = false
  isAdmin:boolean = false
  DessertList: dessert[] = [
    { dessertId: 1, dessertName: 'Chocolate Cake', dessertPrice: 1200, type: "cake", image: 'assets/cake.webp', rating: 4.5, stock: 0, reviews: ['Amazing!', 'Delicious!'], isAvailable:true},
    { dessertId: 2, dessertName: 'Chocolate Ice Cream', dessertPrice: 100, type: "icecream", image: 'assets/icecream.webp', rating: 3.5, stock: 5, reviews: ['Great!', 'Perfect for summer!'], isAvailable:true},
    { dessertId: 3, dessertName: 'Chocolate Cupcake', dessertPrice: 60, type: "cupcake", image: 'assets/cupcake.jpeg', rating: 2, stock: 15, reviews: ['Lovely!', 'So sweet!'], isAvailable:false}
  ];

  uniqueTypes = [...new Set(this.DessertList.map(product => product.type)),"all"];
  selectedType:string = "all"


  cartItems: any[] = [];

  display(dessert: dessert) {
    
    const selectedDessert = this.DessertList.find(d => d.dessertId === dessert.dessertId);
    if (selectedDessert && selectedDessert.stock && selectedDessert.stock > 0) {
      selectedDessert.stock--;  

      if (selectedDessert.stock < 1) {
        selectedDessert.isAvailable = false;
      }
    }
    this.showAlert(dessert.dessertName!); 
    const existingProduct = this.cartItems.find(item => item.dessertId === dessert.dessertId);

    if (existingProduct) {
      
      existingProduct.count += 1;
    } else {
      this.cartItems.push({ ...dessert, count: 1 });
    }
  }

  loginUser(){
    this.isLogged = true
    alert("Logged in successfully");
    console.log(this.DessertList)
  }

  logoutUser(){
    this.isLogged = false
    this.isAdmin = false
    alert("Logged out successfully");
  }
  loginAdmin(){
    this.isAdmin = true
    this.isLogged = true
  }

  generateStars(rating: number | undefined): string {
    if (!rating) return '';

    const fullStar = '<span class="fa fa-star checked"></span>';
    const halfStar = '<span class="fa fa-star-half-alt checked"></span>';
    const emptyStar = '<span class="fa-regular fa-star"></span>';

    let stars = '';
    const totalStars = 5;
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 >= 0.5;
    const remaining = totalStars - fullStars - (hasHalfStar ? 1 : 0)

    for (let i = 0; i < fullStars; i++) {
      stars += fullStar;
    }

    if (hasHalfStar) {
      stars += halfStar;
    }

    for (let i = 0; i<remaining; i++) {
      stars += emptyStar;
    }
    return stars;
  }

  showAlert(dessertName: string) {
    alert(`${dessertName} has been added to your cart!`);
  }

  addDessert(d : dessert){
    this.DessertList.push(d)
  }

  emptyCart(cart : dessert[]){
    this.cartItems = cart
  }

}
export class dessert{
  dessertId?:number
  dessertName?:string
  dessertPrice?:number
  type?:string
  image?:string
  rating?:number
  stock: number = 0
  reviews?:string[]
  isAvailable:boolean = true
}

