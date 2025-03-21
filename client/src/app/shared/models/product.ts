import { ImageUrl } from "./image";
import { Option } from "./option";

export type Product = {
    id: number;
    name: string;
    price: number;
    quantity: number;
    description: string;
    productCategoryId:number;
    productItemImgs: ImageUrl[];
    variationOpts: Option[];
}