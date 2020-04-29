using CSVDynamicParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSVDynamicParser.Servies.Interfaces
{
  public  interface IFileService
    {
        UploadedFile ReadCSVHeader(bool hasHeader, HttpPostedFileBase file);
        DataTable ConvertCSVResult(CSVConvertParameter parameter);
        DataTableViewModel GetDataTableResult(DataTable dt);
    }
}
