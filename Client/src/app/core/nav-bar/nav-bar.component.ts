import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  constructor(public basketService:BasketService, public accountService:AccountService){}

  getCount(items: IBasketItem[]){
    return items.reduce((sum,item)=>{
      return sum+item.quantity;
    },0)
  }

  logout(){
    this.accountService.logout();
  }
}
