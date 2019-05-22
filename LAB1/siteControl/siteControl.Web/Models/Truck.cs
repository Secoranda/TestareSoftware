using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteControl.Web.Models
{

    public class Truck
    {
        [Key]
        public int ID { get; set; }



        [Required]
        public int DispatchExt { get; set; }

        [Required]
        public int TruckID { get; set; }

        [Required]
        public string CurrentLocation { get; set; }

        [Required]
        public string CurrentStatus { get; set; }

        public string Date_Posted { get; set; }
        public string Time_Posted { get; set; }
        public string Date_Edited { get; set; }
        public string Time_Edited { get; set; }
        public string Comments { get; set; }


        public virtual User UserID {get; set;}
    

    }
}