﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class ProviderServiceList
    {
        public ProviderServiceList()
        {
            
        }

        [Key]
        public int ProviderWorkId { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        [ForeignKey("ServiceProvider")]
        public int ServiceProviderId { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProviderName { get; set; }

        [Required]
        [MaxLength(800)]
        public string Address { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(150)]
        public string IsDone { get; set; }

        [Required]
        public DateTime DateOfService { get; set; }

        [Required]
        [MaxLength(250)]
        public string DateOfServiceForShow { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descriptions { get; set; }

        #region Realtions
        public ServiceProvider ServiceProvider { get; set; }
        public IEnumerable<VisibleService> VisibleServices { get; set; }    

        #endregion
    }
}
