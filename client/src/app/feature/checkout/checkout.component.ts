import { Component, inject, OnInit, signal } from '@angular/core';
import { OrderSummaryComponent } from '../../shared/components/order-summary/order-summary.component';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { SnackbarService } from '../../core/services/snackbar.service';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AddressService } from '../../core/services/address.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListOption } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatOption } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { AccountService } from '../../core/services/account.service';
import { Address, User } from '../../shared/models/user';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { firstValueFrom } from 'rxjs';
import { CheckoutDeliveryComponent } from "./checkout-delivery/checkout-delivery.component";
import { CheckoutReviewComponent } from "./checkout-review/checkout-review.component";
import { CartService } from '../../core/services/cart.service';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    OrderSummaryComponent,
    MatStepperModule,
    MatButton,
    RouterLink,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    FormsModule,
    CommonModule,
    MatLabel,
    MatFormFieldModule,
    MatInputModule,
    MatExpansionModule,
    MatOption,
    MatSelectModule,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent
],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
})
export class CheckoutComponent implements OnInit {
  provinces: any[] = [];
  districts: any[] = [];
  wards: any[] = [];
  private snackbar = inject(SnackbarService);
  private accountService = inject(AccountService);
  selectedProvince = '0';
  selectedDistrict = '0';
  selectedWard? = '0';
  //Value of user input
  fullName?: string;
  addressUser: Address = {
    line1: '',
    line2: '',
    city: '',
    quan: '',
    huyen: '',
  };
  user?: User;
  private router = inject(Router);
  panelOpenState = false;
  cartService = inject(CartService)
  saveAddress= false;
  completionStatus = signal<{address: boolean, delivery: boolean}>(
    {address: false, delivery: false}
  )
  constructor(private addressService: AddressService) {}

   ngOnInit() {
    this.loadProvinces();
    this.getCurrentUser();
    this.checkAddressCompletion();
  }
checkAddressCompletion() {
  const isComplete = Boolean(
    this.fullName?.trim() &&
    this.addressUser.line1.trim() && // Now safe to access directly
    this.selectedProvince &&
    this.selectedDistrict &&
    this.selectedWard
  );
  
  this.completionStatus.update(status => ({
    ...status,
    address: isComplete
  }));
  console.log("state: ", this.completionStatus)
}
handleAddressChange = (event: { complete: boolean }) => {
  this.completionStatus.update(state => {
    const isAddressComplete = Boolean(
      this.addressUser && 
      this.fullName && 
      this.addressUser.line1 && 
      this.user && 
      this.addressUser.city && 
      this.addressUser.huyen && 
      this.addressUser.quan
    );
    
    state.address = isAddressComplete;
    return state;
  });
}

  handleDeliveryChange(event: boolean){
    this.completionStatus.update(state => {
      state.delivery = event;
      console.log("state: ", state)
      return state;
    })
  }
  loadProvinces() {
    this.addressService.getProvinces().subscribe((response: any) => {
      if (response.error === 0) {
        this.provinces = response.data;
        this.selectedProvince = this.provinces.find((x) => x.full_name === this.user?.address.city)?.id;
        console.log("province: ", this.selectedProvince);
        
        if (this.selectedProvince) {
          this.addressService.getDistricts(+this.selectedProvince).subscribe((districtResponse: any) => {
            if (districtResponse.error === 0) {
              this.districts = districtResponse.data;
              console.log("district: ", this.districts);
              this.selectedDistrict = this.districts.find((x) => x.full_name === this.user?.address.huyen)?.id;
              
              if (this.selectedDistrict) {
                this.addressService.getWards(+this.selectedDistrict).subscribe((wardResponse: any) => {
                  if (wardResponse.error === 0) {
                    this.wards = wardResponse.data;
                    this.selectedWard = this.wards.find((x) => x.full_name === this.user?.address.quan)?.id;
                  }
                });
              }
            }
          });
        }
      }
    });
  }

  getCurrentUser() {
    const currentUser = this.accountService.currentUser();
    if (currentUser) {
      this.user = currentUser;
      console.log("user: ", currentUser)
    }
    this.fullName = this.user?.firstName + ' ' + this.user?.lastName;
    this.addressUser.line1 = this.user?.address.line1 ?? '';
    this.addressUser.line2 = this.user?.address.line2 ?? '';
    this.addressUser.city = this.user?.address.city?? '';
    this.addressUser.huyen = this.user?.address.huyen?? '';
    this.addressUser.quan = this.user?.address.quan?? '';
  }

  onProvinceChange() {
    this.districts = [];
    this.wards = [];
    this.selectedDistrict = '0';
    this.selectedWard = '0';

    if (this.selectedProvince !== '0') {
      this.addressService
        .getDistricts(+this.selectedProvince!)
        .subscribe((data: any) => {
          if (data.error === 0) {
            this.districts = data.data;
            this.addressUser.city = data.data_name;
          }
        });
    }
  }

  onDistrictChange() {
    this.wards = [];
    this.selectedWard = '0';

    if (this.selectedDistrict !== '0') {
      this.addressService
        .getWards(+this.selectedDistrict!)
        .subscribe((data: any) => {
          if (data.error === 0) {
            this.wards = data.data;
            this.addressUser.huyen = data.data_name;
          }
        });
    }
  }
  onWardChange(){
    this.addressUser.quan = this.wards.find((x) => x.id == this.selectedWard).full_name;
  }
  onSaveAddressCheckboxChange(event: MatCheckboxChange){
    this.saveAddress = event.checked;
  }

  async onStepChange(event: StepperSelectionEvent){
    if(event.selectedIndex===1){
      if(this.saveAddress){
        const address = await this.createAddress();
        
        address && firstValueFrom(this.accountService.updateAddress(address));
      }
    }
    if(event.selectedIndex === 2){
     
    }
  }
createAddress(): Address {
  return {
    line1: this.addressUser?.line1 ?? '',       
    line2: this.addressUser?.line2 ?? '',       
    city: this.addressUser?.city ?? '',    
    huyen: this.addressUser?.huyen ?? '',   
    quan: this.addressUser?.quan ?? ''         
  };
}

async confirmPayment(stepper: MatStepper){
  this.cartService.deleteCart();
  this.cartService.selectedDelivery.set(null);
  this.router.navigateByUrl('checkout/success');
}
}
