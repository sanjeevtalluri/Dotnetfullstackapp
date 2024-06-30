import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if(!basket) return;
    this.createOrder(basket).subscribe({
      next:order=>{
        this.toastr.success('Order successfully created');
        this.basketService.deleteLocalBasket();
        const navigationExtras: NavigationExtras = {state:order}
        this.router.navigate(['checkout/success'],navigationExtras);
        console.log(order);
      }
    })
  }

  private createOrder(basket: IBasket) {
    const orderToCreate = this.getOrderToCreate(basket);
    return this.checkoutService.createOrder(orderToCreate);
  }

  private getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value,
      shipToAddress: this.checkoutForm?.get('addressForm')?.value
    };
  }
}
