using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMedDataSourceMSSQL.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Table( "Clients" )]
    public class Client
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column( "id" )]
        public Int32 Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column( "Name" )]
        [MaxLength( 20 )]
        [Required]
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column( "SecondName" )]
        [MaxLength( 20 )]
        [Required]
        public String SecondName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column( "ThirdName" )]
        [MaxLength( 25 )]
        [Required]
        public String ThirdName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(128)]
        [Column("Phone")]
        public String Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("Address")]
        [MaxLength(256)]
        public String Address
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Visit> Visits
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("BirthDay")]
        [Required]
        public DateTime DateBirth
        {
            get;
            set;
        }
    }
}
