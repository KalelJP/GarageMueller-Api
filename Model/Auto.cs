using System;
using System.ComponentModel.DataAnnotations;

namespace GarageMueller.Model
{
    public class Auto
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Required]
        public int Id { get; set; }


        /// <summary>
        /// Automarke, z.B . BMW
        /// </summary>
        [StringLength(10, MinimumLength =2)]
        [Required]
        public string Marke { get; set; }


        /// <summary>
        /// Auto Modell, z.B . M1
        /// </summary>
        [Required]
        public string Modell { get; set; }


        /// <summary>
        /// Leistung in PS
        /// </summary>
        [Range(-1, 1000)]
        [Required]
        public int Leistung { get; set; }
    }
}
