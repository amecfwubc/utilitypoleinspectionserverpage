using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace WebApp.Models
{
    public class PoleInfoViewModel
    {
        public int ID { get; set; }
        [Required]
        public int? UserID { get; set; }

        [Required]
        [Display(Name = "Assigned Person")]
        public int? TaskAssainUserID { get; set; }

        [Required]
        [Display(Name = "Pole ID")]
        public string PoleID { get; set; }
        [Required]
        [Display(Name = "Pole Type")]
        public int? TypeID { get; set; }
        public string TypeName { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Task Added date")]
        public DateTime? TaskAddeddate { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Task Performed date")]
        public DateTime? TaskPerformeddate { get; set; }
        [Display(Name = "Map Image")]
        public string ImageMapPath { get; set; }
        [Display(Name = "Adjacent Pole Height")]
        public double? AdjacentPoleHeight { get; set; }
        [Display(Name = "Trans Former Loading")]
        public string TransFormerLoading { get; set; }

        public string Notes { get; set; }
        [Display(Name = "Taken Images")]
        public string ImagesTakenpath { get; set; }

        [Display(Name = "Assigned Person")]
        public string UserFullName { get; set; }
        
    }

    public class PoleTypeViewModel
    {

        [Required]
        public string UserID { get; set; }
        [Required]
        [Display(Name = "Pole Type")]
        public string TypeName { get; set; }

    }

    public  class PoleImageViewModel
    {
        public long ID { get; set; }
        public int PoleInfoID { get; set; }
        public string PoleID { get; set; }
        public string ImageMapPath { get; set; }
        public int UserID { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ImageBase64 { get; set; }
    }
}
