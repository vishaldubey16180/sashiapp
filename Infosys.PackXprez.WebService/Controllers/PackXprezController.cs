using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.PackXprez.DataAccessLayer;
using Infosys.PackXprez.DataAccessLayer.Models;

namespace Infosys.PackXprez.WebService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackXprezController : Controller
    {
        PackXprezRepository rep = new DataAccessLayer.PackXprezRepository();
        [HttpPost]
        public bool AddCustomer(Customer cObj)
        {
            bool status = false;
            bool addStatus = false;
            try
            {
                Customer cust = new Customer();
                if (ModelState.IsValid)
                {
                    cust.CustName = cObj.CustName;
                    cust.CustEmailId = cObj.CustEmailId;
                    cust.Password = cObj.Password;
                    cust.ContactNo = cObj.ContactNo;
                    cust.BuildingNo = cObj.BuildingNo;
                    cust.StreetNo = cObj.StreetNo;
                    cust.Locality = cObj.Locality;
                    cust.PinCode = cObj.PinCode;
                }

                status = rep.RegisterCustomer(cust);
                if (status)
                {
                    addStatus = AddAddress(cust.CustEmailId, cust.BuildingNo, cust.StreetNo, cust.Locality, cust.PinCode);
                    if (addStatus == false)
                    {
                        status = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        [HttpPost]
        public bool AddAddress(string emailId, string bNo, string sNo, string locality, decimal pcode)
        {
            bool status = false;
            try
            {
                Address addobj = new Address();
                addobj.EmailId = emailId;
                addobj.Address1 = bNo + "," + sNo + "," + locality + "," + Convert.ToString(pcode);

                //status = rep.AddAddressindb(addobj);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpGet]
        public JsonResult CustomerLogin(string emailId, string Password)
        {
            try
            {
                var obj = rep.CustomerLogin(emailId, Password);
                Console.WriteLine(obj);
                return Json(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }


        [HttpGet]
        public bool ServiceAvailability(int pincode)
        {
            //List<AvailableDeliveryPickup> lstAvailableDeliveryPickup = new List<AvailableDeliveryPickup>();
            bool status = false;
            try
            {
                status = rep.ServiceAvailability(pincode);
            }
            catch (Exception ex)
            {
                status = false;
                Console.WriteLine(ex.Message);
            }
            return status;
        }

        [HttpPost]
        public bool AddFeedback(FeedBack fObj)
        {
            bool status = false;

            try
            {
                FeedBack fb = new FeedBack();
                fb.EmailId = fObj.EmailId;
                fb.FeedbackType = fObj.FeedbackType;
                fb.Comments = fObj.Comments;

                status = rep.AddFeedback(fb);
                Console.WriteLine(status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }
            return status;

        }

        [HttpPut]
        public bool EditCustomerDetails(Customer cobj)
        {

            bool status = false;

            try
            {
                status = rep.UpdateCustomerDetails(cobj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        [HttpDelete]
        public bool DeleteAddress(string address)
        {
            bool status = false;
            try
            {
                status = rep.DeleteAddressindb(address);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        [HttpGet]
        public JsonResult BranchOfficerLogin(string emailId, string Password)
        {
            try
            {
                var obj = rep.BranchOfficerLogin(emailId, Password);
                Console.WriteLine(obj);
                return Json(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetPackageDetailsByLocation(string city)
        {
            List<ScheduledPickUp> list1 = null;
            try
            {
                list1 = rep.GetPackageDetailsByLocation(city);
            }
            catch (Exception)
            {

                list1 = null;
            }
            return Json(list1);
        }

        [HttpPost]
        public bool AddReceivePackage(ReceivePackage robj)
        {
            bool status = false;
            try
            {
                status = rep.InsertDataIntoReceivePackage(robj);
            }
            catch (Exception)
            {

                status = false;
            }
            return status;
        }

        [HttpPut]
        public bool UpdateScheduledPickup(string emailId)
        {
            bool status = false;
            try
            {
                status = rep.UpdateScheduledPickup(emailId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }
            return status;
        }

        [HttpGet]
        public JsonResult GetAddress(string emailId)
        {
            try
            {
                var lstadd = rep.FetchAddressByEmailId(emailId);
                return Json(lstadd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        [HttpGet]
        public JsonResult GetCardDetailsfromSavedcards(string emailId)
        {
            try
            {
                var cardinfo = rep.displayCardDetails(emailId);
                return Json(cardinfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        [HttpPost]
        public bool SchedulePickUp(WebService.Models.SchedulePickUpCardDetails spcdobj)
        {
            bool status = false;
            try
            {
                ScheduledPickUp spobj = new ScheduledPickUp();
                CardDetails cdobj = new CardDetails();
                spobj.EmailId = spcdobj.EmailId;
                spobj.PickupPinCode = spcdobj.PickupPinCode;
                spobj.DeliveryPinCode = spcdobj.DeliveryPinCode;
                spobj.ShipmentType = spcdobj.ShipmentType;
                spobj.PackageLen = spcdobj.PackageLen;
                spobj.PackageBreadth = spcdobj.PackageBreadth;
                spobj.PackageHeight = spcdobj.PackageHeight;
                spobj.PackageWeight = spcdobj.PackageWeight;
                spobj.PackingReq = spcdobj.PackingReq;
                spobj.DeliveryOpt = spcdobj.DeliveryOpt;
                spobj.TimeSlot = spcdobj.TimeSlot;
                spobj.PickupAddress = spcdobj.PickupAddress;
                spobj.DeliveryAddress = spcdobj.DeliveryAddress;
                //spobj.Insurance = spcdobj.Insurance;
                //spobj.Amount = spcdobj.Amount;
                spobj.Payment = spcdobj.Payment;

                if (spcdobj.CardNumber != 0)
                {
                    cdobj.CardNumber = spcdobj.CardNumber;
                    cdobj.NameOnCard = spcdobj.NameOnCard;
                    cdobj.CardType = spcdobj.CardType;
                    cdobj.Cvvnumber = spcdobj.Cvvnumber;
                    cdobj.ExpiryDate = spcdobj.ExpiryDate;
                    cdobj.Balance = spcdobj.Balance;
                }

                status = rep.ScheduleAPick_Up(spobj, cdobj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        [HttpGet]
        public string TrackShipmentStatus(int AWBNo)
        {
            try
            {
                var status = rep.TrackShipment(AWBNo);
                return status;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpGet]
        public JsonResult FetchOrderHistory(string emailId)
        {
            try
            {
                var temp = rep.OrderHistory(emailId);
                return Json(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}