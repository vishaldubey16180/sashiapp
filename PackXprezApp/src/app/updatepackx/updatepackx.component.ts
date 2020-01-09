import { Component, OnInit } from '@angular/core';
import { userservice } from '../services/userservice.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-updatepackx',
  templateUrl: './updatepackx.component.html',
  styleUrls: ['./updatepackx.component.css']
})
export class UpdatepackxComponent implements OnInit {
  CustName: string
  CustEmailId: string
  Password: string
  ContactNo: number
  BuildingNo: string
  StreetNo: string
  PinCode: number
  Locality: string

  constructor( private _userservice: userservice, private rt: Router) { }

  ngOnInit() {
    
    
  }
  UpdateProfile(form: NgForm) {
    console.log(form.value.name);
    console.log(form.value.emailId);
    console.log(form.value.password);
    console.log(form.value.contactno);
    this._userservice.updateprofile(form.value.name, form.value.emailId, form.value.password, form.value.contactno)
      .subscribe(
      x => {
          console.log(x)
          if (x) { alert("Updated Successfully") }
          else { alert("Error Occurred") }
          this.rt.navigate(['/login']);
        },
        err => { console.log(err) },
        () => console.log("UpdateCart executed successfully"));

  }

}
