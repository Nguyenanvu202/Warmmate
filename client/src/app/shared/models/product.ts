import { ImageUrl } from "./image";

export type Product = {
    id: number;
    name: string;
    price: number;
    quantity: number;
    description: string;
    productCategoryId:number;
    productItemImgs: any[];
    variationOpts:any[];
}