import { Component, OnInit } from '@angular/core';
import { userservice } from '../services/userservice.service';
import { NgForm } from '@angular/forms';
import { IAddress } from '../packXprez-interfaces/Address';

@Component({
  selector: 'app-deleteuser',
  templateUrl: './deleteuser.component.html',
  styleUrls: ['./deleteuser.component.css']
})
export class DeleteuserComponent implements OnInit {
  status: boolean = false
  errorMsg:string

  constructor(private _userservice: userservice) {
    
  }

  ngOnInit() {
  }
  //removeFromuser(address:string) {
  //  this._userservice.deleteuser(address).subscribe(
  //    responseRemoveCartProductStatus => {
  //      this.status = responseRemoveCartProductStatus;
  //      if (this.status) {
  //        alert("Product deleted successfully.");
  //        this.ngOnInit();
  //      }
  //      else {
  //        alert("Product could not be deleted. Please try after sometime.");
  //      }
  //    },
  //    responseRemoveCartProductError => {
  //      this.errorMsg = responseRemoveCartProductError;
  //      alert("Something went wrong. Please try after sometime.");
  //    },
  //    () => console.log("RemoveProductFromCart method executed successfully")
  //  );
  //}

}
