export interface MomoCreatePaymentResponseModel {
    requestId: string | null;
    errorCode: number | null;
    orderId: string | null;
    message: string | null;
    localMessage: string | null;
    requestType: string | null;
    payUrl: string | null;
    signature: string | null;
    qrCodeUrl: string | null;
    deeplink: string | null;
    deeplinkWebInApp: string | null;
}