import { Component, OnInit } from '@angular/core';
import { IScheduledPickup } from '../packXprez-interfaces/ScheduledPickup';
import { userservice } from '../services/userservice.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-branch-receive-package',
  templateUrl: './branch-receive-package.component.html',
  styleUrls: ['./branch-receive-package.component.css']
})
export class BranchReceivePackageComponent implements OnInit {

  errMsg: string;
  scheduledPickup: IScheduledPickup[];
  scheduledPickup1: IScheduledPickup[];
  city: string;
  name: string="Madhura";
  status1: boolean = false;
 // errMsg: string;
  constructor(private _ps: userservice) { }

  ngOnInit() {
    this.city = sessionStorage.getItem('Location');
    //this.name = this.aroute.snapshot.params('')
    
    console.log(this.city);
    this.getDetails();
  }
  
  getDetails() {
    this._ps.getSchedulingDetails(this.city).subscribe(
      x => {
        console.log("ajdaks");
        this.scheduledPickup = x;
        console.log(this.scheduledPickup);
      },
      y => {
        this.errMsg = y;
        console.log(this.errMsg);
      },
      ()=> console.log("Method executed successfully")
    )
  }

  pickup( emailId: string, city: string, deliveryAddress: string) {
    console.log("Into pickup");
    this.insertintoReceivePackage(this.name, emailId, city, deliveryAddress);
    this.updateScheduledPickup(emailId);
    
  }


  insertintoReceivePackage(name:string,emailId:string,city:string,deliveryAddress:string) {
   // console.log(name);
    console.log("Into receive package");
    console.log(name);
    console.log(emailId);
    console.log(city);
    console.log(deliveryAddress);
    this._ps.validateReceivePackage(name, city, deliveryAddress).subscribe(
      x => {
        this.status1 = x;
      },
      y => {
        this.errMsg = y;
        console.log(this.errMsg);
      },
      ()=> console.log("Insert into package method executed successfully")
    )

  }

  updateScheduledPickup(emailId: string) {


  }
}
