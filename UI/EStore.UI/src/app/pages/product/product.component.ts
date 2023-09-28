import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/common/ProductDto';
import { CouponDto } from '../coupon/model/coupondto';
import { ProductService } from './service/product.service';
import { ResponseDto } from 'src/app/common/responsedto';
import { BaseComponent } from '../base/base/base.component';
import { AuthService } from 'src/app/service/auth.service';
import { CategoryService } from 'src/app/service/category.service';
import { Category } from 'src/app/common/Category';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent extends BaseComponent {
  products: ProductDto[] = [];
  categories: Category[] = [];
  showCreate: boolean = false;
  showEdit: boolean = false;
  showDetails: boolean = false;
  categoryId: number;
  selectedProduct: ProductDto = new ProductDto();
  selectedCategory: Category = new Category();
  newProduct: ProductDto = new ProductDto();
  editingProduct: ProductDto = new ProductDto();
  isCategoryOpen = true;
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
    private categoryService: CategoryService,
    protected override authService: AuthService,
    private route: ActivatedRoute) {
    super(authService);
  }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
        console.log(params); // { orderby: "price" }
        this.categoryId = params["categoryId"];
        console.log(this.categoryId); // price
        this.getCategoryMap();
        let productObjects: any;
        let productString = '{"isSuccess":true,"result":[{"productId":1,"name":"Samosa","price":15,"description":"Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.","categoryName":null,"imageUrl":"assets/images/14.jpg","quantity":10,"categoryId":7,"category":null},{"productId":2,"name":"Paneer Tikka","price":13.99,"description":"Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.","categoryName":null,"imageUrl":"assets/images/12.jpg","quantity":10,"categoryId":7,"category":null},{"productId":3,"name":"Sweet Pie","price":10.99,"description":"Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.","categoryName":null,"imageUrl":"assets/images/11.jpg","quantity":10,"categoryId":6,"category":null},{"productId":4,"name":"Pav Bhaji","price":15,"description":"Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.","categoryName":null,"imageUrl":"assets/images/13.jpg","quantity":10,"categoryId":3,"category":null},{"productId":5,"name":"Solo Bambu Katkılı 40lı Tuvalet Kağıdı","price":19,"description":"Çevre dostu tuvalet kağıdı.","categoryName":null,"imageUrl":"assets/images/Tuvalet Kağıdı.webp","quantity":10,"categoryId":8,"category":null},{"productId":6,"name":"Xiaomi Redmi Note 12 Pro","price":1900,"description":"Xiaomi Redmi Note 12 Pro 8 GB 256 GB(Xiaomi Türkiye Garantili)","categoryName":null,"imageUrl":"assets/images/Redmi Note 12 Pro.webp","quantity":10,"categoryId":5,"category":null},{"productId":7,"name":"Samsung Galaxy A04E","price":1700,"description":"Samsung Galaxy A04E 4 GB 128 GB(Samsung Türkiye Garantili)","categoryName":null,"imageUrl":"assets/images/Samsung Galaxy A04E.jpg","quantity":10,"categoryId":5,"category":null}],"displayMessage":"","errorMessage":null}';
        productObjects = JSON.parse(productString);
        console.log(productObjects);
        this.products = productObjects.result;
      }
      );

    // this.getProducts();
  }
  findSubcategoryIds(baseId: number, data: Category[]): number[] {
    const result: number[] = [];
    result.push(data.find(i => i.Id == baseId).Id);
    for (const category of data.filter(i => i.BaseCategoryId == baseId)) {
      result.push(...this.findSubcategoryIds(category.Id, data.filter(i => i.BaseCategoryId == baseId)));
    }

    return result;
  }
  getCategories(): void {
    this.categoryService.getCategories().subscribe(
      (data: Category[]) => {
        // Initialize a map to store categories by their BaseCategoryId
        const categoryMap = new Map<number, Category[]>();

        // Group categories by BaseCategoryId
        data.forEach(category => {
          if (category.BaseCategoryId == null)
            category.BaseCategoryId = 0;
          if (!categoryMap.has(category.BaseCategoryId)) {
            categoryMap.set(0, []);
          }
          categoryMap.get(category.BaseCategoryId)?.push(category);
        });

        // Populate subcategories based on BaseCategoryId
        data.forEach(category => {
          if (categoryMap.has(category.Id)) {
            category.subCategories = categoryMap.get(category.Id) || [];
          }
        });

        // Filter top-level categories (categories with BaseCategoryId 0)
        this.categories = categoryMap.get(0) || [];
      },
      (error: any) => {

      });

    debugger;
  }
  getCategoryMap() {
    var categoryString = '[{"Id":1,"CategoryName":"Yemek","BaseCategoryId":null,"subCategories":[{"Id":3,"CategoryName":"Ana Yemek","BaseCategoryId":1},{"Id":6,"CategoryName":"Tatlı","BaseCategoryId":1},{"Id":7,"CategoryName":"Aparatif","BaseCategoryId":1}]},{"Id":2,"CategoryName":"Elektronik","BaseCategoryId":null,"subCategories":[{"Id":4,"CategoryName":"Telefon","BaseCategoryId":2,"subCategories":[{"Id":5,"CategoryName":"Android Telefon","BaseCategoryId":4}]}]},{"Id":3,"CategoryName":"Ana Yemek","BaseCategoryId":1},{"Id":4,"CategoryName":"Telefon","BaseCategoryId":2,"subCategories":[{"Id":5,"CategoryName":"Android Telefon","BaseCategoryId":4}]},{"Id":5,"CategoryName":"Android Telefon","BaseCategoryId":4},{"Id":6,"CategoryName":"Tatlı","BaseCategoryId":1,"subCategories":[{"Id":5,"CategoryName":"Android Telefon","BaseCategoryId":4,"subCategories":[{"Id":5,"CategoryName":"Android Telefon","BaseCategoryId":4}]}]},{"Id":7,"CategoryName":"Aparatif","BaseCategoryId":1},{"Id":8,"CategoryName":"Temizlik","BaseCategoryId":null}]';
    let data: Category[] = [];

    data = JSON.parse(categoryString);
    let categoryIds = this.findSubcategoryIds(this.categoryId, data);
    console.log(categoryIds);
    // Initialize a map to store categories by their BaseCategoryId
    const categoryMap = new Map<number, Category[]>();

    // Group categories by BaseCategoryId
    data.forEach(category => {
      if (category.BaseCategoryId == null)
        category.BaseCategoryId = 0;
      if (!categoryMap.has(category.BaseCategoryId)) {
        categoryMap.set(category.BaseCategoryId, []);
      }
      categoryMap.get(category.BaseCategoryId)?.push(category);
    });

    // Populate subcategories based on BaseCategoryId
    data.forEach(category => {
      if (categoryMap.has(category.Id)) {
        category.subCategories = categoryMap.get(category.Id) || [];
      }
    });

    // Filter top-level categories (categories with BaseCategoryId 0)
    this.categories = categoryMap.get(0) || [];
    console.log(JSON.stringify(this.categories));
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
            rating: 0,
            category: null,
            categoryId: 0
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
      rating: 0,
      category: null,
      categoryId: 0
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
      rating: 0,
      category: null,
      categoryId: 0
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
  toggleCategory(): void {
    this.isCategoryOpen = !this.isCategoryOpen;
  }

  onSortOptionChange(event: any) {
    this.selectedSortOption = event.target.value;
    // Burada sıralama seçeneği değiştiğinde yapılacak işlemleri ekleyebilirsiniz.
  }


}



