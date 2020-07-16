using iMedDataSource.enums;
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
    [Table("Visits")]
    public class Visit
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("Id")]
        public Int32 Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("VisitData")]
        public DateTime VisitData
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("VisitType")]
        public eVisitType VisitType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(25)]
        [Column("Diagnos")]
        public String Diagnos
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("ClientId")]
        public Int32 ClientId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Client Client
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public Visit()
        {
        }
    }
}
