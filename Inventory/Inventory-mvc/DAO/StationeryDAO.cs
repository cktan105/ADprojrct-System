﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_mvc.Models;
using System.Data.Entity;

namespace Inventory_mvc.DAO
{
    public class StationeryDAO : IStationeryDAO
    {
        bool IStationeryDAO.AddNewStationery(Stationery stationery)
        {
            using (StationeryModel context = new StationeryModel())
            {
                context.Stationeries.Add(stationery);
                int rowAffected = context.SaveChanges();

                if (rowAffected != 1)
                {
                    throw new DAOException();
                }

                return true;
            }
        }


        public bool DeleteStationery(string itemCode)
        {
            using (StationeryModel context = new StationeryModel())
            {
                Stationery stationery = (from s in context.Stationeries
                                     where s.itemCode == itemCode
                                     select s).FirstOrDefault();

                context.Stationeries.Remove(stationery);
                context.SaveChanges();

                return true;
            }
        }

        Stationery IStationeryDAO.FindByItemCode(string itemCode)
        {
            StationeryModel context = new StationeryModel();

            return (from s in context.Stationeries
                    where s.itemCode == itemCode
                    select s).FirstOrDefault();
        }

        

         int IStationeryDAO.UpdateStationeryInfo(Stationery stationery)
        {
            using (StationeryModel context = new StationeryModel())
            {
                Stationery s = (from x in context.Stationeries
                              where x.itemCode == stationery.itemCode
                              select x).FirstOrDefault();

                s.categoryID = stationery.categoryID;
                s.description = stationery.description;
                s.reorderLevel = stationery.reorderLevel;
                s.reorderQty = stationery.reorderQty;
                s.unitOfMeasure = stationery.unitOfMeasure;
                s.stockQty = stationery.stockQty;
                s.location = stationery.location;
                s.firstSupplierCode = stationery.firstSupplierCode;
                s.secondSupplierCode = stationery.secondSupplierCode;
                s.thirdSupplierCode = stationery.thirdSupplierCode;
                s.price = stationery.price;
                int rowAffected = context.SaveChanges();

                return rowAffected;
            }
        }
   

        List<string> IStationeryDAO.GetAllItemCode()
        {

            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.itemCode).ToList();
            }
        }
        

    
        List<Stationery> IStationeryDAO.GetAllStationery()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s)
                        .Include(s => s.Category)
                        .ToList();
            }
        }
       
     


        List<string> IStationeryDAO.GetUOMList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.unitOfMeasure).Distinct().ToList();
            }
        }


        List<Category> IStationeryDAO.GetAllCategory()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from c in context.Categories
                        select c).ToList();
            }

        }




        List<String> IStationeryDAO.GetAllUOMList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.unitOfMeasure).Distinct().ToList();
            }
        }

        List<string> IStationeryDAO.GetAllFirstSupplierList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.firstSupplierCode).Distinct().ToList();
            }
        }

        List<string> IStationeryDAO.GetAllSecondSupplierList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.secondSupplierCode).Distinct().ToList();
            }
        }

        List<string> IStationeryDAO.GetAllThirdSupplierList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.thirdSupplierCode).Distinct().ToList();
            }
        }

        List<int> IStationeryDAO.GetAllCategoryIDList()
        {
            using (StationeryModel context = new StationeryModel())
            {
                return (from s in context.Stationeries
                        select s.categoryID).Distinct().ToList();
            }
        }


        List<Stationery> IStationeryDAO.GetStationeriesBasedOnCriteria(string searchString, string categoryID)
        {
            using (StationeryModel context = new StationeryModel())
            {
                var stationeries = from s in context.Stationeries select s;

                if(categoryID != "All")
                {
                    stationeries = stationeries.Where(s => s.categoryID.ToString() == categoryID);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    string[] searchStringArray = searchString.Split();
                    foreach (string s in searchStringArray)
                    {
                        string search = s.ToLower().Trim();
                        if (!String.IsNullOrEmpty(search))
                        {
                            stationeries = stationeries.Where(x => x.description.ToLower().Contains(search));
                        }
                    }
                }

                return stationeries.Include(s => s.Category).ToList();
            }

        }
    }
}