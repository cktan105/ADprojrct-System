﻿using Inventory_mvc.Models;
using Inventory_mvc.Service;
using Inventory_mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Inventory_mvc.Controllers
{
    public class StationeryController : Controller
    {
        
        IStationeryService stationeryService = new StationeryService();
        // GET: Stationery
        public ActionResult Index(string searchString, string categoryID = "All")
        {
            List<StationeryViewModel> stationeries = stationeryService.GetAllStationeryViewModel();
            
            ViewBag.CategoryList = stationeryService.GetAllCategory();


            if (categoryID == "All")
            {
                ViewBag.CategoryID = "All";
            }
            else
            {
                ViewBag.CategoryID = categoryID;

                stationeries = (from s in stationeries
                                where s.CategoryID.ToString() == categoryID
                                select s).ToList();
            }

            ViewBag.SearchString = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                string search = searchString.ToLower().Trim();

                stationeries = (from s in stationeries
                                where s.Description.ToLower().Contains(search)
                                select s).ToList();
            }
           
            // return View(stationeryService.GetAllStationeryViewModel());
            return View(stationeries);
        }



        // GET: Supplier/Edit/{id}
        public ActionResult Edit(string id)
        {
            StationeryViewModel stationeryVM = stationeryService.FindStationeryViewModelByItemCode(id);
            return View(stationeryVM);
        }


        // POST: Supplier/Edit/{id}
        [HttpPost]
        public ActionResult Edit(StationeryViewModel stationeryVM)
        {
            string code = stationeryVM.ItemCode;
            int level = stationeryVM.ReorderLevel;
            int qty = stationeryVM.ReorderQty;
            decimal price = stationeryVM.Price;

            if (stationeryService.isPositiveLevel(level))
            {
                string errorMessage = String.Format("{0}  must be positive.", level);
                ModelState.AddModelError("ReorderLevel", errorMessage);
            }
            if (stationeryService.isPositiveQty(qty))
            {
                string errorMessage = String.Format("{0}  must be positive.", qty);
                ModelState.AddModelError("ReorderQty", errorMessage);
            }
            if (stationeryService.isPositivePrice(price))
            {
                string errorMessage = String.Format("{0}  must be positive.", price);
                ModelState.AddModelError("Price", errorMessage);
            }

            else if (ModelState.IsValid)
            {
                try
                {
                    if (stationeryService.UpdateStationeryInfo(stationeryVM))
                    {
                        TempData["EditMessage"] = String.Format("'{0}' has been updated", code);
                    }
                    else
                    {
                        TempData["EditErrorMessage"] = String.Format("There is not change to '{0}'.", code);
                    }
            
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ExceptionMessage = e.Message;
                }
            }
            return View(stationeryVM);
        }

        // GET: Stationery/Create
        public ActionResult Create()
        {
            return View(new StationeryViewModel());
        }

        // POST: Stationery/Create
        [HttpPost]
        public ActionResult Create(StationeryViewModel stationeryVM)
        {
            string code = stationeryVM.ItemCode;
            int level = stationeryVM.ReorderLevel;
            int qty = stationeryVM.ReorderQty;
            decimal price = stationeryVM.Price;

            if (stationeryService.isExistingCode(code) )
                {
                string errorMessage = String.Format("{0} has been used.", code);
                    ModelState.AddModelError("ItemCode", errorMessage);
                }
            if (stationeryService.isPositiveLevel(level))
                {
                    string errorMessage = String.Format("{0}  must be positive.", level);
                    ModelState.AddModelError("ReorderLevel", errorMessage);
                }
            if (stationeryService.isPositiveQty(qty))
            {
                string errorMessage = String.Format("{0}  must be positive.", qty);
                ModelState.AddModelError("ReorderQty", errorMessage);
            }
            if (stationeryService.isPositivePrice(price))
            {
                string errorMessage = String.Format("{0}  must be positive.", price);
                ModelState.AddModelError("Price", errorMessage);
            }
            else if (ModelState.IsValid)
            { 
                {
                    try
                    {
                        stationeryService.AddNewStationery(stationeryVM);
                        TempData["CreateMessage"] = String.Format("Stationery '{0}' is added.", code);
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["ExceptionMessage"] = e.Message;
                    }
                }                       
            }
                     
             return View(stationeryVM);
        }

        // GET: Stationery/Delete/{id}
        public ActionResult Delete(string id)
        {
            if (stationeryService.DeleteStationery(id))
            {
                TempData["DeleteMessage"] = String.Format("Stationery '{0}' has been deleted", id);
            }
            else
            {
                TempData["DeleteErrorMessage"] = String.Format("Cannot delete stationery '{0}'", id);
            }

            return RedirectToAction("Index");
        }


        // GET: Stationery/Details
        public ActionResult ViewStockCard(string id)
        {
            return View(stationeryService.FindStationeryViewModelByItemCode(id));
        }


        public ActionResult ResetCatalogue()
        {
            return RedirectToAction("Index", new { searchString = "", categoryID = "All" });
        }

        // GET: Stationery/Browse
        //public ActionResult Browse()
        //{            
        //    ViewBag.CategoryList = stationeryService.GetAllCategory();

        //    return View();
        //}


    }
}