using CSVDynamicParser.Servies.Implements;
using CSVDynamicParser.Servies.Interfaces;
using CSVDynamicParser.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSVDynamicParser.Tests.ServiceTest
{
    [TestClass]
    public class ServiceTest : ServiceTestBase
    {
        [TestMethod]
        public void CommonService_getDataTypes()
        {
            // Arrange
            var types =new CommonService().getDataTypes();
            // Assert
            Assert.IsTrue(types.Count==3);
        }

        [TestMethod]
        public void FileService_IfColumnMapped()
        {
            var file = new FileService();
            var para = new CSVConvertParameter()
            {
                HasHeader=true,
                Headers=new List<HeaderParameter>()
                {
                    new HeaderParameter()
                    {
                        Index=1,
                        Name="test1",
                        Value=new ConfigurationItemViewModel()
                        {
                            dataType=DataTypeEnum.String,
                            id=Guid.NewGuid(),
                            name="testcof",
                            required=true,
                            size=0

                        }
                    },
                      new HeaderParameter()
                    {
                        Index=2,
                        Name="test2",
                        Value=new ConfigurationItemViewModel()
                        {
                            dataType=DataTypeEnum.String,
                            id=Guid.NewGuid(),
                            name="testcof",
                            required=true,
                            size=0

                        }
                    },
                        new HeaderParameter()
                    {
                        Index=3,
                        Name="test3",
                        Value=new ConfigurationItemViewModel()
                        {
                            dataType=DataTypeEnum.String,
                            id=Guid.NewGuid(),
                            name="testcof",
                            required=true,
                            size=0

                        }
                    },
                }
            };
          var mapped=  file.IfColumnMapped(para, 3);
            Assert.IsTrue(mapped.Name=="test3");
        }
    }
}
