using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyFixit.FixitTaskEntity
{
    public partial class FixItTask
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DisplayCreatedDate
        {
            get
            {
                return CreatedDate;
            }
        }    
    }
}
