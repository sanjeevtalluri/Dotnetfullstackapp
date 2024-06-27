import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketTotals } from '../models/basket';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent {
  basketTotal$?: Observable<IBasketTotals | null>;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basketTotal$ = this.basketService.basketTotalSource$;
  }
}
