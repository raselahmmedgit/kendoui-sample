using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace RnD.KendoUISample.ViewModels
{
    public class MinistryAgencyViewModel
    {
        // [Id]
        //,[DivisionId]
        //,[AgencyName]
        //,[SortOrder]
        //,[IsActive]
        //,[IUser]
        //,[EUser]
        //,[IDate]
        //,[EDate]

        [Range(1, long.MaxValue, ErrorMessage = "Please select one.")]
        public int DivisionId { get; set; }

        [DisplayName("Division Name")]
        public string DivisionName { get; set; }

        [DisplayName("Agency Name")]
        [Required(ErrorMessage = "Agency Name is required")]
        public string AgencyName { get; set; }

        [DisplayName("Sort Order")]
        [Required(ErrorMessage = "Sort Order is required")]
        public int? SortOrder { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { set; get; }

        [DisplayName("Active")]
        public string Active { set; get; }

        public IEnumerable<SelectListItem> ddlDivisions { get; set; }
    }

    //SubSector
    public class SubSectorViewModel
    {
        // [Id]
        //,[SectorId]
        //,[SubSectorName]
        //,[Acronym]
        //,[IATICode]
        //,[SortOrder]
        //,[IsActive]
        //,[IUser]
        //,[EUser]
        //,[IDate]
        //,[EDate]

        [Range(1, long.MaxValue, ErrorMessage = "Please select one.")]
        public int SectorId { get; set; }

        [DisplayName("Sector Name")]
        public string SectorName { get; set; }

        [DisplayName("Sub-Sector Name")]
        [Required(ErrorMessage = "Sub-Sector is required")]
        public string SubSectorName { get; set; }

        [DisplayName("Acronym Name")]
        [Required(ErrorMessage = "Acronym is required")]
        public string Acronym { get; set; }

        [DisplayName("IATI Code")]
        [Required(ErrorMessage = "IATICode is required")]
        public string IATICode { get; set; }

        [DisplayName("Sort Order")]
        [Required(ErrorMessage = "Sort Order is required")]
        public int? SortOrder { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { set; get; }

        [DisplayName("Active")]
        public string Active { set; get; }

        public IEnumerable<SelectListItem> ddlSectors { get; set; }
    }

    //FundSource
    public class FundSourceViewModel
    {
        //[Id] [int] NOT NULL,
        //[FundSourceName] [varchar](100) NOT NULL,
        //[FundSourceCategoryId] [int] NOT NULL,
        //[FundSourceGroup] [varchar](100) NULL,
        //[CurrencyId] [int] NOT NULL,
        //[Address] [varchar](250) NULL,
        //[TelephoneNo] [varchar](50) NULL,
        //[FaxNo] [varchar](50) NULL,
        //[WebURL] [varchar](100) NULL,
        //[IATICode] [varchar](50) NULL,
        //[ContactPersonName] [varchar](100) NULL,
        //[ContactPersonTelephone] [varchar](50) NULL,
        //[ContactPersonEMail] [varchar](100) NULL,
        //[IsActive] [bit] NULL,
        //[IUser] [varchar](50) NOT NULL,
        //[EUser] [varchar](50) NULL,
        //[IDate] [datetime] NOT NULL,
        //[EDate] [datetime] NULL,

        [DisplayName("Fund Source Name")]
        [Required(ErrorMessage = "Fund Source Name is required")]
        public string FundSourceName { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select one.")]
        public int FundSourceCategoryId { get; set; }

        [DisplayName("Fund Source Category")]
        public string FundSourceCategoryName { get; set; }

        [DisplayName("Fund Source Group")]
        [Required(ErrorMessage = "Fund Source Group is required")]
        public string FundSourceGroup { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select one.")]
        public int CurrencyId { get; set; }

        [DisplayName("Currency")]
        public string CurrencyName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Telephone No")]
        public string TelephoneNo { get; set; }

        [DisplayName("FaxNo")]
        public string FaxNo { get; set; }

        [DisplayName("WebURL")]
        public string WebURL { get; set; }

        [DisplayName("IATI Code")]
        [Required(ErrorMessage = "IATICode is required")]
        public string IATICode { get; set; }

        [DisplayName("Contact Person")]
        public string ContactPersonName { get; set; }

        [DisplayName("Contact Person Telephone")]
        public string ContactPersonTelephone { get; set; }

        [DisplayName("Contact Person EMail")]
        public string ContactPersonEMail { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { set; get; }

        [DisplayName("Active")]
        public string Active { set; get; }

        public IEnumerable<SelectListItem> ddlFundSourceCategories { get; set; }
        public IEnumerable<SelectListItem> ddlCurrencies { get; set; }
    }
}