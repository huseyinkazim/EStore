export class Category {
    Id: number;
    CategoryName: string;
    BaseCategoryId: number;
    link: string;
    subCategories: Category[];
}