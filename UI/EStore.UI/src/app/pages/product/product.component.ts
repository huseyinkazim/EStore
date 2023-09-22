import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/common/ProductDto';
import { CouponDto } from '../coupon/model/coupondto';
import { ProductService } from './service/product.service';
import { ResponseDto } from 'src/app/common/responsedto';
import { BaseComponent } from '../base/base/base.component';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent extends BaseComponent {
  products: ProductDto[] = [];
  showCreate: boolean = false;
  showEdit: boolean = false;
  showDetails: boolean = false;
  selectedProduct: ProductDto = new ProductDto();
  newProduct: ProductDto = new ProductDto();
  editingProduct: ProductDto = new ProductDto();
  selectedSortOption = 'NUMARA11'; // Varsayılan sıralama seçeneği
  sortOptions = [
    { value: 'NUMARA11', label: 'Akıllı Sıralama' },
    { value: 'PRICE_LOW', label: 'Artan fiyat' },
    { value: 'PRICE_HIGH', label: 'Azalan fiyat' },
    { value: 'SALES_VOLUME', label: 'Satış miktarı' },
    { value: 'REVIEWS', label: 'Yorum sayısı' },
    { value: 'NEWEST', label: 'Yeni ürün' },
    { value: 'REVIEW_RATE', label: 'Ürün Notu' },
    { value: 'SELLER_GRADE', label: 'Mağaza Puanı' }
  ];
  constructor(private productService: ProductService,
    protected override authService: AuthService) {
    super(authService);
  }

  ngOnInit(): void {

    this.products = [
      {
        productId: 0,
        name: "Solo Bambu Katkılı 40'lı Tuvalet Kağıdı",
        description: "Çevre dostu tuvalet kağıdı.",
        categoryName: "Temizlik",
        imageUrl: "https://n11scdn.akamaized.net/a1/602_857/03/98/33/22/IMG-3428722542198131134.jpg",
        price: 190.00,
        quantity: 1,
        rating: 3.0
      },
      {
        productId: 0,
        name: "Xiaomi Redmi Note 12 Pro",
        description: "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/08/84/05/19/IMG-7386303242057276048.jpg",
        price: 11899.00,
        quantity: 3,
        rating: 4.0
      },
      {
        productId: 0,
        name: "Xiaomi Redmi Note 12 Pro",
        description: "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/08/84/05/19/IMG-7386303242057276048.jpg",
        price: 11899.00,
        quantity: 3,
        rating: 5.0
      },
      {
        productId: 0,
        name: "Xiaomi Redmi Note 12 Pro",
        description: "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/08/84/05/19/IMG-7386303242057276048.jpg",
        price: 11899.00,
        quantity: 3,
        rating: 3.5
      },
      {
        productId: 0,
        name: "Xiaomi Redmi Note 12 Pro",
        description: "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/08/84/05/19/IMG-7386303242057276048.jpg",
        price: 11899.00,
        quantity: 3,
        rating: 2.8

      },
      {
        productId: 0,
        name: "Samsung Galaxy A04E",
        description: "Samsung Galaxy A04E 4 GB 128 GB (Samsung Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/01/40/24/33/IMG-1826629388239884520.jpg",
        price: 4999,
        quantity: 3,
        rating: 3.0
      },
      {
        productId: 0,
        name: "Reeder S19 Max Pro",
        description: "Reeder S19 Max Pro 6 GB 256 GB (Reeder Türkiye Garantili)",
        categoryName: "Telefon",
        imageUrl: "https://n11scdn.akamaized.net/a1/226_339/01/81/34/55/IMG-2812823934010575074.jpg",
        price: 4041.00 ,
        quantity: 3,
        rating: 1.0
      }
    ];

    // this.getProducts();
  }

  getProducts(): void {
    this.productService.getProducts().subscribe(
      (data: ResponseDto) => {
        if (data.isSuccess) {
          this.products = data.result;
        }
      },
      (error: any) => {

      });
  }

  createProduct(): void {
    this.productService.createProduct(this.newProduct).subscribe(
      (data: ResponseDto) => {
        if (data.isSuccess) {
          this.products.push(data.result);
          this.clearShow();
          this.showCreate = false;
          this.newProduct = {
            productId: 0,
            name: '',
            price: 0,
            description: '',
            categoryName: '',
            imageUrl: '',
            quantity: 0,
            rating: 0
          };
        }
      },
      (error: any) => {

      });
  }

  editProduct(product: ProductDto): void {
    this.editingProduct = { ...product };
    this.clearShow();
    this.showEdit = true;
  }

  updateProduct(updatedProduct: ProductDto): void {
    this.productService.updateProduct(updatedProduct).subscribe(
      () => {
        const index = this.products.findIndex(p => p.productId === updatedProduct.productId);
        if (index !== -1) {
          this.products[index] = updatedProduct;
          this.clearShow();
          this.showEdit = false;
        }
      });
  }

  deleteProduct(productId: number): void {
    this.productService.deleteProduct(productId).subscribe(() => {
      this.products = this.products.filter(p => p.productId !== productId);
    });
  }

  showCreateForm(): void {
    this.clearShow();
    this.showCreate = true;
  }

  cancelEdit(): void {
    this.clearShow();
    this.editingProduct = {
      productId: 0,
      name: '',
      price: 0,
      description: '',
      categoryName: '',
      imageUrl: '',
      quantity: 0,
      rating:0
    };
  }

  cancelCreate(): void {
    this.clearShow();
    this.newProduct = {
      productId: 0,
      name: '',
      price: 0,
      description: '',
      categoryName: '',
      imageUrl: '',
      quantity: 0,
      rating:0
    };
  }

  selectProduct(product: ProductDto): void {
    this.selectedProduct = product;
    this.clearShow();
    this.showDetails = true;
  }
  clearShow() {
    this.showCreate = false;
    this.showEdit = false;
    this.showDetails = false;

  }


  onSortOptionChange(event: any) {
    this.selectedSortOption = event.target.value;
    // Burada sıralama seçeneği değiştiğinde yapılacak işlemleri ekleyebilirsiniz.
  }
}



