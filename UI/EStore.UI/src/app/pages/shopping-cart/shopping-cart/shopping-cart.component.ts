import { Component } from '@angular/core';
import { ShoppingCartService } from '../service/shopping-cart.service';
import { ProductDto } from 'src/app/common/ProductDto';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent {
  products: ProductDto[]=[];
 
  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    // Sepet verilerini alma örneği
    this.products = [
      {
        productId: 0,
        name: "Solo Bambu Katkılı 40'lı Tuvalet Kağıdı",
        description: "Çevre dostu tuvalet kağıdı.",
        categoryName: "Temizlik",
        imageUrl: "https://n11scdn.akamaized.net/a1/80/03/98/33/22/IMG-3428722542198131134.jpg",
        price: 190.00,
        quantity: 1,
        rating:3.0,
        category:null,
        categoryId: 0
      }
    ];
    this.shoppingCartService.getCart('12345').subscribe((data) => {
      // API'den gelen sepet verilerini kullanın
      this.products = [
        {
          productId: 0,
          name: "Solo Bambu Katkılı 40'lı Tuvalet Kağıdı",
          description: "Çevre dostu tuvalet kağıdı.",
          categoryName: "Yemek",
          imageUrl: "https://n11scdn.akamaized.net/a1/80/03/98/33/22/IMG-3428722542198131134.jpg",
          price: 190.00,
          quantity: 1,
          rating:3.0,
          category:null,
          categoryId : 0
        }
      ];
      console.log(data);
    });
  }


  removeFromCart(item: any) {
    // Sepetten ürünü kaldırma işlemi
    const index = this.products.indexOf(item);
    if (index !== -1) {
      this.products.splice(index, 1);
    }
  }

  clearCart() {
    // Sepeti temizleme işlemi
    this.products = [];
  }

  // Sepetten ürünü kaldır
  removeProduct(product: any) {
    const index = this.products.indexOf(product);
    if (index !== -1) {
      this.products.splice(index, 1);
    }
  }

  // Toplam tutarı hesapla
  getTotalPrice(): string {
    const total = this.products.reduce((sum, product) => sum + product.price * product.quantity, 0);
    return total.toFixed(2);
  }

  increaseQuantity(product: any) {
    product.quantity += 1;
  }
  decreaseQuantity(product: any) {
    if (product.quantity == 1)
      this.removeProduct(product)
    product.quantity -= 1;
  }
  getTotalQuantity(): number {
    let totalQuantity = 0;

    for (const product of this.products) {
      totalQuantity += product.quantity;
    }

    return totalQuantity;
  }
}
