using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paypal;

namespace PayPal.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult Index()
        {
            PayPalExerciseEntities context = new PayPalExerciseEntities();
            return View(context.TransactionRecords);
        }

        public ActionResult PayPal()
        {
            PayPal_IPN paypalResponse = new PayPal_IPN("test");

            if (paypalResponse.TXN_ID != null)
            {
                PayPalExerciseEntities context = new PayPalExerciseEntities();
                TransactionRecord ipn = new TransactionRecord();
                ipn.transactionID = paypalResponse.TXN_ID;
                decimal amount = Convert.ToDecimal(paypalResponse.PaymentGross);
               // int totalTickets = Convert.ToInt32(paypalResponse.Quantity);
                ipn.amount = amount;
                ipn.buyerEmail = paypalResponse.PayerEmail;
                ipn.sessionID = paypalResponse.Custom;
                ipn.txTime = DateTime.Now;
                ipn.firstName = paypalResponse.PayerFirstName;
                ipn.totalTickets = paypalResponse.Quantity;
                ipn.lastName = paypalResponse.PayerLastName;
                ipn.paymentStatus = paypalResponse.PaymentStatus;
                context.TransactionRecords.Add(ipn);
                context.SaveChanges();
            }
            return View();
        }

    }
}