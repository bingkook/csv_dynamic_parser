using CSVDynamicParser.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.DbAccessor.Models
{
    [Table("ConfigurationItem")]
    public class ConfigurationItem
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DataTypeEnum DataType { get;set;}
        public bool Required { get; set; }
        public int Size { get; set; }
    }
}
