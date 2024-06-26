import { Component, OnInit } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { Type } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  shopParams = new ShopParams();
  totalCount = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'High to Low', value: 'priceDesc' },
    { name: 'Low to High', value: 'priceAsc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService
      .getProducts(this.shopParams)
      .subscribe(
        (response) => {
          this.products = response.data;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalCount = response.count;
        },
        (err) => {
          console.log(err);
        }
      );
  }
  getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      },
      (err) => {
        console.log(err);
      }
    );
  }
  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      (err) => {
        console.log(err);
      }
    );
  }
  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }
  onSortSelected(event:any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
  onPageChanged(event:any){
    if(this.shopParams.pageNumber != event.page){
      this.shopParams.pageNumber = event.page;
      this.getProducts();
    }
  }
}
