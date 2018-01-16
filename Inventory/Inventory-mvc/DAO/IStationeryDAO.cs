﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_mvc.Models;

namespace Inventory_mvc.DAO
{
    public interface IStationeryDAO
    {
        List<Stationery> GetAllStationery();

        Stationery FindByItemCode(string itemCode);

        bool AddNewStationery(Stationery stationery);

        int UpdateStationeryInfo(Stationery stationery);

        bool DeleteStationery(string itemCode);

        List<string> GetUOMList();

        List<Category> GetAllCategory();
     
    }
}