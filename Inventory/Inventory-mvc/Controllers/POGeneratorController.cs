﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_mvc.Entity;
using Inventory_mvc.Models;

namespace Inventory_mvc.Service
{
    public class POGeneratorController : Controller
    {
        Inventory_mvc.Models.StationeryModel ctx = new Models.StationeryModel();
        PurchaseOrderService pos = new PurchaseOrderService();

        // GET: POGenerator

        [HttpGet]
        public ActionResult gen(string id) //id is purchase order number
        {

            int orderNo = Int32.Parse(id);
            Purchase_Order_Record por = pos.FindByOrderID(orderNo);

            Inventory_mvc.Models.User clerk = ctx.Users.Where(x => x.userID == por.clerkID).First();

            Supplier s = ctx.Suppliers.Where(x => x.supplierCode == por.supplierCode).First();
            ViewBag.orderNo = id;
            ViewBag.clerkID = por.clerkID;
            ViewBag.clerkName = clerk.name;
            ViewBag.supplier = s.supplierName;
            ViewBag.delivery = por.expectedDeliveryDate;

            List<Purchase_Detail> model = ctx.Purchase_Detail.Where(x => x.orderNo == orderNo).ToList();

            return View(model);
        }
    }
}