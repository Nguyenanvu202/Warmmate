export interface Order {
    id: number
    orderDate: string
    buyerEmail: string
    shippingAddress: ShippingAddress
    deliveryMethod: string
    shippingPrice: number
    orderItems: OrderItem[]
    subtotal: number
    status: string
    total: number
  }
  
  export interface ShippingAddress {
    name: string
    line1: string
    line2: any
    city: string
    huyen: string
    quan: string
  }
  
  export interface OrderItem {
    productId: number
    productName: string
    pictureUrl: string
    price: number
    quantity: number
  }

  export interface OrderToCreate{
    cartId: string;
    deliveryMethodId: number;
    shippingAddress: ShippingAddress;
  }