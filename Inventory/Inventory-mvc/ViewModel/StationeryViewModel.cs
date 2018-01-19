﻿using Inventory_mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory_mvc.ViewModel
{
    public class StationeryViewModel
    {
        [Key]
        [Required]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string ItemCode { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        [Required]
        [Display(Name = "Reorder Quantity")]
        public int ReorderQty { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "UOM")]
        public string UnitOfMeasure { get; set; }

        [Required]
        [Display(Name = "Stock Quantity")]
        public int StockQty { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "1st Supplier")]
        public string FirstSupplierCode { get; set; }

       
        [StringLength(50)]
        [Display(Name = "2nd Supplier")]
        public string SecondSupplierCode { get; set; }

        
        [StringLength(50)]
        [Display(Name = "3rd Supplier")]
        public string ThirdSupplierCode { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
    }
}