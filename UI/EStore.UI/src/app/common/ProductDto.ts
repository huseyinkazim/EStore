import { Category } from "./Category";

export class ProductDto {
    productId: number;
    name: string;
    price: number;
    description: string;
    categoryName: string;
    imageUrl: string;
    quantity: number;
    rating: number;
    categoryId: number;
    category: Category;
}