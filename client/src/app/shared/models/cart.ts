import { nanoid } from "nanoid";

export type CartType = {
    id: string;
    items: CartItem[];
}

export type CartItem = {
    itemId: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    size: string;
    color: string;
    productId: number;
}

export class Cart implements CartType {
    id = nanoid();
    items: CartItem[] = [];
    deliveryMethodId?: number;
    orderId?: string
}