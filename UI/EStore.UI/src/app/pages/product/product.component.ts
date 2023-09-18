import { Component } from '@angular/core';
import { ProductDto } from 'src/app/common/ProductDto';
import { CouponDto } from '../coupon/model/coupondto';
import { ProductService } from './service/product.service';
import { ResponseDto } from 'src/app/common/responsedto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  products: ProductDto[] = [];
  showCreate: boolean = false;
  showEdit: boolean = false;
  showDetails: boolean = false;
  selectedProduct: ProductDto = new ProductDto();
  newProduct: ProductDto = new ProductDto();
  editingProduct: ProductDto = new ProductDto();

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getProducts();
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
            imageUrl: ''
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
      imageUrl: ''
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
      imageUrl: ''
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
}



