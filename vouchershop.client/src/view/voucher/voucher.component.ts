import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ElementRef } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

interface voucher {
  id: string;
  name: string;
  amount;
}

@Component({
  selector: 'voucher-root',
  templateUrl: './voucher.component.html',
  styleUrl: './voucher.component.css'
})
export class AppComponent implements OnInit {
  public vouchers: voucher[] = [];
  name = 'Angular';
  cartId = -1;

  constructor(private http: HttpClient, private currencyPipe: CurrencyPipe) {
  }

  ngOnInit() {
    this.getVouchers();
  }

  getVouchers() {
    this.http.get<voucher[]>('/voucher/listvouchers').subscribe(
      (result) => {
        this.vouchers = result;
        for (let i = 0; i < this.vouchers.length; i++) {
          this.vouchers[i].amount = this.currencyPipe.transform(this.vouchers[i].amount, '£');
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  transformAmount(element, voucher: voucher) {
    if (element.target.value.startsWith("£")) {
      element.target.value = element.target.value.substring(1);
    }
    element.target.value = this.currencyPipe.transform(element.target.value, '£');
    console.log(voucher.amount);
    voucher.amount = element.target.value;
    console.log(voucher.amount);
    //element.target.value = this.formattedAmount;
  }

  addToCart(event, voucher: voucher) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
   
    const newCartVoucher =
    {
      cartID: this.cartId,
      voucher: voucher,
    };

    var data = JSON.stringify(newCartVoucher);
    data = data.replace('£', '');
    data = data.replace(/,(?!["{}[\]])/g, "");
    console.log(data);
    this.http.post('/voucher/AddToCart', data, { headers })
      .pipe(catchError((err, caught) => caught))
      .subscribe(response => {
        console.log('Success:', response); // Debugging log
        this.cartId = Number(response);
        console.log(this.cartId); // Debugging log
      },
      error => {
        //console.error('Error:', error); // Debugging log
      });;
    console.log(data);
  }

  checkout(event) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const newCartVoucher =
    {
      cartID: this.cartId,
      voucher: { id: "", name: "", amount: 0},
    };
    var data = JSON.stringify(newCartVoucher);
    console.log(data);
    this.http.post('/voucher/Checkout', data, { headers })
      .pipe(catchError((err, caught) => caught))
      .subscribe(response => {
        console.log('Success:', response); // Debugging log
        this.cartId = -1;
      },
      error => {
        console.error('Error:', error); // Debugging log
      });
  }

  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error.message); // Debugging log
    return throwError('Something bad happened; please try again later.');
  }

  title = 'vouchershop.client';
}
