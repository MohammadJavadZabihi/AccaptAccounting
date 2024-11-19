using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class ClientUpdate
    {
        public ClientUpdate()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Version { get; set; }

        [Required]
        public bool IsMandentory { get; set; }

        [MaxLength(900)]
        public string RealeseNote { get; set; }

        [Required]
        public string DownloadUrl { get; set; }
    }
}
