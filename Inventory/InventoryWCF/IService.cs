﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InventoryWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        //[OperationContract]
        // TODO change to post method
        //[WebGet(UriTemplate = "/ValidateUser/{userid}/{password}", ResponseFormat = WebMessageFormat.Json)]
        //Boolean ValidateUser(string userid, string password);

        [OperationContract]
        // TODO change to post method
        [WebGet(UriTemplate = "/ChangePassword/{userid}/{currentpassword}/{newpassword}", ResponseFormat = WebMessageFormat.Json)]
        Boolean ChangePassword(string userid, string currentpassword, string newpassword);

        [OperationContract]
        [WebGet(UriTemplate = "/GetUser/{userid}/{password}", ResponseFormat = WebMessageFormat.Json)]
        WCFUser GetUser(string userid, string password);

        //[OperationContract]
        //[WebGet(UriTemplate = "/All", ResponseFormat = WebMessageFormat.Json)]
        //List<string> GetAllItemCode();

        [OperationContract]
        [WebGet(UriTemplate = "/GetRequisitionByItemCode/{itemCode}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRequisitionDetail> GetRequisitionDetailsByItemCode(string itemCode);

        [OperationContract]
        [WebGet(UriTemplate = "/GetRequisitionDetailsBy2Keys/{itemCode}/{requisitionNo}", ResponseFormat = WebMessageFormat.Json)]
        WCFRequisitionDetail GetRequisitionDetailsBy2Keys(string itemCode, string requisitionNO);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllStationeries", ResponseFormat = WebMessageFormat.Json)]
        List<WCFStationery> GetAllStationeries();

        [OperationContract]
        [WebGet(UriTemplate = "/GetStationery/{itemCode}", ResponseFormat = WebMessageFormat.Json)]
        WCFStationery GetStationery(string itemCode);

        [OperationContract]
        [WebGet(UriTemplate = "/GetStationeries/{categoryName}/{searchString}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFStationery> GetStationeryByCriteria(string categoryName, string searchString);

        [OperationContract]
        [WebGet(UriTemplate = "/GetStationeries/{categoryName}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFStationery> GetStationeryByCategory(string categoryName);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllCategories", ResponseFormat = WebMessageFormat.Json)]
        List<WCFCategory> GetAllCategories();

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllRequisitionRecords", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRequisitionRecord> GetAllRequisitionRecords();

        [OperationContract]
        [WebInvoke(Method = "POST",
                   UriTemplate = "/AddNewRequest/{requesterID}",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        bool AddNewRequest(string requesterID, WCFRequisitionDetail[] newRequisition);


        [OperationContract]
        [WebGet(UriTemplate = "/GetPendingRequestByDept/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRequisitionDetail> GetPendingRequestByDept(string deptCode);

        [OperationContract]
        [WebGet(UriTemplate = "/GetDetailByReqNo/{reqNo}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRequisitionDetail> GetDetailsByReqNo(string reqNo);

        //[OperationContract]
        //Boolean updateRequisitionDetails(int requisitionNo, string ItemCode, int allocateQty);

        //// TODO: Add your service operations here

        [OperationContract]
        [WebGet(UriTemplate = "/GetRetrievalList", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRetrievalForm> getRetrievalList();

        [OperationContract]
        [WebInvoke(Method= "POST",
            UriTemplate = "/UpdateRetrieval", 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool UpdateRetrieval(WCFRetrievalForm wcfr);

        //[OperationContract]
        //[WebGet(UriTemplate = "/GetRetrievalItemByName", ResponseFormat = WebMessageFormat.Json)]
        //List<WCFRetrievalForm> getRetrievalItemByName();

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllDepartments", ResponseFormat = WebMessageFormat.Json)]
        List<WCFDepartment> GetAllDepartments();

        [OperationContract]
        [WebGet(UriTemplate = "/GetDisbursementByDept/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFDisbursement> GetDisbursementByDept(string deptCode);

        //[OperationContract]
        //[WebGet(UriTemplate = "/GetAllRequisitionforAllocation", ResponseFormat = WebMessageFormat.Json)]
        //List<WCFRequisitionDetail> GetAllRequisitionforAllocation();
    }

        //[OperationContract]
        //List<Disbursement> getDisbursementList();
        ////the follwing is for employee
        //[OperationContract]
        //List<RequisitionRecord> getRequisitionListByUserID(string UserID);

        //[OperationContract]
        //List<RequisitionDetails> getrequisitionDetailsByNO(int requisitionNo);




    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class WCFDisbursement
    {
        string stationeryDescription;
        string itemCode;
        int? needQty;

        [DataMember]
        public string StationeryDescription
        {
            get { return stationeryDescription; }
            set { stationeryDescription = value; }
        }
        [DataMember]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        [DataMember]
        public int? NeedQty
    {
            get { return needQty; }
            set { needQty = value; }
        }
    }

    [DataContract]
    public class WCFRetrievalForm
    {
        string description;
        int? qty;
        int? qtyRetrieved;
        string location;

        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public int? Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        [DataMember]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        [DataMember]
        public int? QtyRetrieved
        {
            get { return qtyRetrieved; }
            set { qtyRetrieved = value; }
        }
    }

    [DataContract]
    public class WCFStationery
    {
        string itemcode;
        string description;
        string uom;
        string categoryName;
        string location;

        [DataMember]
        public string ItemCode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }


        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }

        [DataMember(Name = "Category")]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        [DataMember]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
    }

    [DataContract]
    public class WCFUser
    {
        string userid;
        string password;
        string departmentCode;
        int role;
        //need to add departmentCode

        [DataMember]
        public int Role
        {
            get { return role; }
            set { role = value; }
        }

        [DataMember]
        public string UserID
        {
            get { return userid; }
            set { userid = value; }
        }

        [DataMember]
        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; }
        }

    }

    [DataContract]
    public class WCFRequisitionRecord
    {
        int requisitionNo;
        string deptCode;
        string deptName;
        string requesterID;
        string requesterName;
        string approvingStaffID;
        string approvingStaffName;
        DateTime? approveDate;
        string status;
        DateTime? requestDate;

        [DataMember]
        public int RequisitionNo
        {
            get { return requisitionNo; }
            set { requisitionNo = value; }
        }

        [DataMember]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }

        [DataMember]
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }


        [DataMember]
        public string RequesterID
        {
            get { return requesterID; }
            set { requesterID = value; }
        }

        [DataMember]
        public string RequesterName
        {
            get { return requesterName; }
            set { requesterName = value; }
        }


        [DataMember]
        public string ApprovingStaffID
        {
            get { return approvingStaffID; }
            set { approvingStaffID = value; }
        }

        [DataMember]
        public string ApprovingStaffName
        {
            get { return approvingStaffName; }
            set { approvingStaffName = value; }
        }


        [DataMember]
        public DateTime? ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public DateTime? RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
    }

    [DataContract]
    public class WCFRequisitionDetail
    {
        int requisitonNo;
        string itemCode;
        string description;
        string uom;
        string remarks;
        int qty;
        int fulfilledQty;
        string clerkID;
        DateTime? retrievedDate;
        int allocateQty;
        DateTime? nextCollectionDate;

        [DataMember]
        public int RequisitionNo
        {
            get { return requisitonNo; }
            set { requisitonNo = value; }
        }

        [DataMember]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        [DataMember]
        public string StationeryDescription
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }


        [DataMember]
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        [DataMember(Name = "RequestQty")]
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        [DataMember]
        public int FulfilledQty
        {
            get { return fulfilledQty; }
            set { fulfilledQty = value; }
        }

        [DataMember]
        public string ClerkID
        {
            get { return clerkID; }
            set { clerkID = value; }
        }

        [DataMember]
        public DateTime? RetrievedDate
        {
            get { return retrievedDate; }
            set { retrievedDate = value; }
        }

        [DataMember]
        public int AllocateQty
        {
            get { return allocateQty; }
            set { allocateQty = value; }
        }

        [DataMember]
        public DateTime? NextCollectionDate
        {
            get { return nextCollectionDate; }
            set { nextCollectionDate = value; }
        }
    }

    [DataContract]
    public class WCFCategory
    {
        int categoryID;
        string categoryName;

        [DataMember]
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

    }

    [DataContract]
    public class WCFDepartment
    {
        [DataMember]
        public string DepartmentCode
        {
            get; set;
        }

        [DataMember]
        public string DepartmentName
        {
            get; set;
        }

        [DataMember]
        public string ContactName
        {
            get; set;
        }

        [DataMember]
        public int CollectionPointID
        {
            get; set;
        }
    }



