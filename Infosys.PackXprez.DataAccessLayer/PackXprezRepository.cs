using Infosys.PackXprez.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infosys.PackXprez.DataAccessLayer
{
    public class PackXprezRepository
    {
        public PackXprezDBContext Context { get; set; }

        public PackXprezRepository()
        {
            Context = new PackXprezDBContext();
        }

        //Customer registration DAL
        public bool RegisterCustomer(Customer custobj)
        {
            bool status = false;
            try
            {
                Context.Customer.Add(custobj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }        

        public Customer CustomerLogin(string emailId, string password)
        {
            Customer obj1 = new Customer();
            try
            {
                obj1 = (from b in Context.Customer
                        where b.CustEmailId == emailId && b.Password == password
                        select b).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj1 = null;
            }
            return obj1;
        }


        public bool ServiceAvailability(int pincode)
        {
            bool status = false;
            List<AvailableDeliveryPickup> lstAvailableDeliveryPickup = new List<AvailableDeliveryPickup>();
            try
            {
                lstAvailableDeliveryPickup = (from t1 in Context.AvailableDeliveryPickup where t1.Pincode == pincode select t1).ToList();
                if (lstAvailableDeliveryPickup.Count() == 1)
                {
                    Console.WriteLine(lstAvailableDeliveryPickup.Count());
                    status = true;
                    // return status;
                }
                else
                {
                    status = false;
                    // return status;
                }
            }
            catch (Exception ex)
            {
                status = false;
                lstAvailableDeliveryPickup = null;
                Console.WriteLine(ex.Message);
            }
            return status;
        }

        public bool AddFeedback(FeedBack fObj)
        {
            bool status = false;
            FeedBack fb = new FeedBack();
            fb.EmailId = fObj.EmailId;
            fb.FeedbackType = fObj.FeedbackType;
            fb.Comments = fObj.Comments;
            try
            {
                Context.FeedBack.Add(fb);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                Console.WriteLine(ex.Message);
            }
            return status;
        }

        
        //Update Customer Details DAL
        public bool UpdateCustomerDetails(Customer cobj)
        {
            Customer tempobj = new Customer();
            bool status = false;
            try
            {
                tempobj = (from c in Context.Customer
                           where cobj.CustEmailId == c.CustEmailId
                           select c).FirstOrDefault();
                if (tempobj != null)
                {
                    //Console.WriteLine("Not null");
                    tempobj.CustName = cobj.CustName;
                    tempobj.Password = cobj.Password;
                    tempobj.ContactNo = cobj.ContactNo;

                    Context.Customer.Update(tempobj);
                    Context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }
        //Add address in DB DAL
        public bool AddAddressindb(Address aobj)
        {
            bool status = false;
            try
            {
                Context.Address.Add(aobj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }
        //Delete Address from DB DAL
        public bool DeleteAddressindb(string address)
        {
            bool status = false;
            try
            {
                var tempaddobj = (from a in Context.Address
                                  where address == a.Address1
                                  select a).FirstOrDefault();
                Context.Address.Remove(tempaddobj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        //BranchOfficer Officer Login
        public BranchOfficer BranchOfficerLogin(string emailId, string password)
        {
            BranchOfficer obj1 = new BranchOfficer();
            try
            {
                obj1 = (from b in Context.BranchOfficer
                        where b.BoemailId == emailId && b.Password == password
                        select b).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj1 = null;
            }
            return obj1;
        }
        
        public bool ScheduleAPick_Up(ScheduledPickUp obj,CardDetails cdobj )
        {
            ScheduledPickUp temp = new ScheduledPickUp();
            Orders orderobj = new Orders();
            bool status = false;
            try
            {
                Console.WriteLine("hi");
                if (ServiceAvailability(Convert.ToInt32(obj.DeliveryPinCode)) && ServiceAvailability(Convert.ToInt32(obj.PickupPinCode)))
                {
                    Console.WriteLine("Hello");
                    int dist = 5;
                    int deloptchrge=0;
                    int insurancechrg=0;
                    int packingServicesCharge=0;
                    int perKmPrice = 7;                   
                    int pickUpChrg = 200;

                    int weightCharges=0;
                    int volumeCharges=0;
                    decimal amount;

                    decimal volume = (obj.PackageLen * obj.PackageBreadth * obj.PackageHeight);

                    int insuranceCol;
                    decimal finalamount;
                    if (obj.Payment.ToLower() == "card" && cdobj != null)
                    {
                        addCardDetails(cdobj);
                        addSavedCards(obj.EmailId, cdobj.CardNumber);
                    }

                    if (obj.PackingReq == 1)
                    {
                        packingServicesCharge = 500;
                    }
                    if (obj.DeliveryOpt == "Overnight")
                    {
                        deloptchrge = 500;
                    }
                    if (obj.DeliveryOpt == "Express")
                    {
                        deloptchrge = 100;
                    }
                    if (obj.DeliveryOpt == "Standard")
                    {
                        deloptchrge = 0;
                    }

                    if (obj.PackageWeight > 5)
                    {
                        weightCharges = 500;
                    }
                    
                    if(volume>100)
                    {
                        volumeCharges = 50;
                    }

                    amount = pickUpChrg + deloptchrge + (dist * perKmPrice) + packingServicesCharge + (obj.PackageWeight * weightCharges) + (volume * volumeCharges);

                    if (amount > 1000)
                    {
                        insuranceCol = 1;
                        insurancechrg = 1000;
                    }
                    else
                    {                        
                        insuranceCol = 1;
                        insurancechrg = 1000;                    
                    }

                    finalamount = amount + insurancechrg;
                    
                    temp.EmailId = obj.EmailId;
                    temp.PickupPinCode = Convert.ToDecimal(obj.PickupPinCode);
                    temp.DeliveryPinCode = Convert.ToDecimal(obj.DeliveryPinCode);
                    temp.ShipmentType = obj.ShipmentType;
                    temp.PackageLen = Convert.ToDecimal(obj.PackageLen);
                    temp.PackageBreadth = Convert.ToDecimal(obj.PackageBreadth);
                    temp.PackageHeight = Convert.ToDecimal(obj.PackageHeight);
                    temp.PackageWeight = Convert.ToDecimal(obj.PackageWeight);
                    temp.PackingReq = Convert.ToByte(obj.PackingReq);
                    temp.DeliveryOpt = obj.DeliveryOpt;
                    temp.TimeSlot = Convert.ToDateTime(obj.TimeSlot);
                    temp.PickupAddress = obj.PickupAddress;
                    temp.DeliveryAddress = obj.DeliveryAddress;
                    temp.Insurance = Convert.ToByte(insuranceCol);
                    temp.Amount = Convert.ToDecimal( finalamount);
                    temp.Payment = obj.Payment;

                    orderobj.DeliveryAddress = temp.DeliveryAddress;
                    orderobj.EmailId = temp.EmailId;
                    
                    Console.WriteLine(finalamount);
                    //int retval = (int)Context.Database.ExecuteSqlCommand($"exec [dbo].[schedule_pickup] {obj.EmailId},{Convert.ToInt32(obj.PickupPinCode)},{Convert.ToInt32(obj.DeliveryPinCode)}," +
                    //    $"{obj.ShipmentType},{Convert.ToDecimal(obj.PackageLen)},{Convert.ToDecimal(obj.PackageBreadth)},{Convert.ToDecimal(obj.PackageHeight)},{Convert.ToDecimal(obj.PackageWeight)}," +
                    //    $"{obj.PackingReq},{obj.DeliveryOpt},{obj.TimeSlot},{obj.PickupAddress},{obj.DeliveryAddress},{dist},{obj.Payment}");
                    Context.ScheduledPickUp.Add(temp);
                    Context.Orders.Add(orderobj);
                    //Console.WriteLine("xyz");
                    Context.SaveChanges();
                    
                    status = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        public List<Address> FetchAddressByEmailId(string emailId)
        {
            List<Address> lstofAddressobjects =new List<Address>();
            try
            {
                lstofAddressobjects = (from a in Context.Address
                                where a.EmailId == emailId
                                select a).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lstofAddressobjects;
        }

        public bool addCardDetails( CardDetails cdobj)
        {
            bool status = false;
            try
            {
                Context.CardDetails.Add(cdobj);
                Context.SaveChanges();
                status = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        public bool addSavedCards(string emailId,decimal cardNo)
        {
            bool status = false;
            SavedCards sobj = new SavedCards();
            try
            {
                sobj.EmailId = emailId;
                sobj.CardNumber = cardNo;
                Context.SavedCards.Add(sobj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        public List<CardDetails> displayCardDetails(string emailId)
        {
            List<CardDetails> lstcdobj = new List<CardDetails>();
            try
            {
                var lstcardNos = (from cd in Context.SavedCards
                            where cd.EmailId == emailId
                            select cd.CardNumber).ToList();
                foreach(var card in lstcardNos)
                {
                     var temp= (from cd in Context.CardDetails
                           where cd.CardNumber == card
                           select cd).FirstOrDefault();
                    lstcdobj.Add(temp);
                }
                //Console.WriteLine("success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                lstcdobj=null;
            }
            return lstcdobj;
        }

        public string TrackShipment(int AWNno)
        {
            string status=null;
            try
            {
                //status = Context.Database.FromSql("select * from dbo.ufn_TrackShipment(@AWNNo)",AWNno).FirstOrDefault();
                status = (from x in Context.Orders
                          where x.AwbNumber == AWNno
                          select x.Status).FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                status = null;
            }
            return status;
        }

        public bool InsertDataIntoReceivePackage(ReceivePackage robj)

        {
            ReceivePackage fb = new ReceivePackage();

            //fb.AwbNumber = robj.AwbNumber;
            fb.CustomerName = robj.CustomerName;
            fb.FromLocation = robj.FromLocation;
            fb.ToAddress = robj.ToAddress;
            fb.UpdatedStatus = robj.UpdatedStatus;
            bool status = false;
            try
            {
                Context.ReceivePackage.Add(fb);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        public List<Orders> OrderHistory(string emailId)
        {
            List<Orders> ordlst = new List<Orders>();
            try
            {
                 ordlst = (from o in Context.Orders
                            where o.EmailId == emailId
                            select o).ToList();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                ordlst = null;
            }
            return ordlst;
        }
       
        public List<ScheduledPickUp> GetPackageDetailsByLocation(string city)
        {
            List<ScheduledPickUp> pickupList = null;
            try
            {
                pickupList = (from ps in Context.ScheduledPickUp
                              join adp in Context.AvailableDeliveryPickup on ps.PickupPinCode equals
                                adp.Pincode
                              where adp.City == city
                              select ps).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                pickupList = null;

            }
            return pickupList;
        }
        
        public bool UpdateScheduledPickup(string emailId)
        {
            ScheduledPickUp tempobj = new ScheduledPickUp();
            bool status = false;
            long num;
            try
            {
                tempobj = (from c in Context.ScheduledPickUp
                           where c.EmailId == emailId
                           select c).FirstOrDefault();
                if (tempobj != null)
                {
                    //Console.WriteLine("Not null");
                    tempobj.AwbNumber = (from db in Context.ReceivePackage where db.CustomerName == tempobj.Name select db.AwbNumber).FirstOrDefault();


                    Context.ScheduledPickUp.Update(tempobj);
                    Context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        
    }
}
