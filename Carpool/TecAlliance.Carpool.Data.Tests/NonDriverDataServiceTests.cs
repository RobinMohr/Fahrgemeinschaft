using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Moq;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services;
using TecAlliance.Carpools.Business.Services.Interfaces;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services;
using TecAlliance.Carpools.Data.Services.Interfaces;

namespace TecAlliance.Carpools.Data.Tests
{
    [TestClass]
    public class NonDriverDataServiceTests
    {
        private NonDriverDataService _nonDriverDataService = new NonDriverDataService(@$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "NonDriverInformation.csv"))}");

        [TestMethod]
        public void GetNonDriverTest()
        {
            //AAA Pattern für unit tests
            //Fluent assertions

            //Arrange
            int id = 0;

            //Act
            var result = _nonDriverDataService.GetNonDriverByID(id);

            //Assert
            NonDriver expectedResult = new NonDriver { ID = 0, City = "Weikersheim", Deleted = false, Name = "Robin Mohr", Password = "221133 " };
            Assert.AreEqual(expectedResult.ID, result.ID);
        }

        [TestMethod]
        public void ReadNonDriverDataTest()
        {
            // Arrange

            // Act
            List<NonDriver> result = _nonDriverDataService.ReadNonDriverData();

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<NonDriver>));
        }
    }

    [TestClass]
    public class DriverDataServiceTests
    {
        private DriverDataService _driverDataService = new DriverDataService(@$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "DriverInformation.csv"))}");

        [TestMethod]
        public void GetDriverTest()
        {
            //Arrange
            int id = 0;

            //Act
            var result = _driverDataService.GetDriverByID(id);

            //Assert
            NonDriver expectedResult = new NonDriver { ID = 0, City = "Weikersheim", Deleted = false, Name = "Robin Mohr", Password = "221133 " };
            Assert.AreEqual(expectedResult.ID, result.ID);
        }
    }

    [TestClass]
    public class CarpoolDataServiceTests
    {
        private CarpoolDataService _carpoolDataService;
        private readonly Mock<IDriverDataService> _driverDataServiceMock = new Mock<IDriverDataService>();
        private readonly Mock<INonDriverDataService> _nonDriverDataServiceMock = new Mock<INonDriverDataService>();

        public CarpoolDataServiceTests()
        {
            _carpoolDataService = new CarpoolDataService(_driverDataServiceMock.Object, _nonDriverDataServiceMock.Object, @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Carpools.csv"))}");
        }

        [TestMethod]
        public void GetCarpoolTest()
        {
            // Arrange
            int carpoolID = 0;
            int ownerID = 0;

            var driver = new Driver();
            _driverDataServiceMock.Setup(x => x.GetDriverByID(ownerID))
                .Returns(driver);

            //_nonDriverDataServiceMock.Setup(x => x.GetNonDriver())

            // Act 
            var resultCarpool = _carpoolDataService.GetCarpoolDataByID(carpoolID);


            // Assert
            Carpool expectedCarpool = new Carpool { CarpoolId = 0 };
            Assert.AreEqual(expectedCarpool.CarpoolId, resultCarpool.CarpoolId);
        }
    }

    [TestClass]
    public class CarpoolBusinessServiceTests
    {
        private CarpoolBusinessService _carpoolBusinessService;

        private readonly Mock<ICarpoolDataService> _carpoolDataServiceMock = new Mock<ICarpoolDataService>();
        private readonly Mock<INonDriverDataService> _nonDriverDataServiceMock = new Mock<INonDriverDataService>();
        private readonly Mock<IDriverBusinessService> _driverBusinessService = new Mock<IDriverBusinessService>();
        private readonly Mock<IDriverDataService> _driverDataServiceMock = new Mock<IDriverDataService>();

        public CarpoolBusinessServiceTests()
        {
            _carpoolBusinessService = new CarpoolBusinessService(_carpoolDataServiceMock.Object, _nonDriverDataServiceMock.Object, _driverBusinessService.Object, _driverDataServiceMock.Object);
        }


        [TestMethod]
        public void GetByIdTest()
        {
            // Arrange
            int id = 0;

            List<Carpool> allCarpools = new List<Carpool>();
            _carpoolDataServiceMock.Setup(x => x.ReadCarpoolData())
                .Returns(allCarpools);

            Carpool carpool = new Carpool();
            _carpoolDataServiceMock.Setup(x => x.GetCarpoolDataByID(id))
                .Returns(carpool);

            // Act
            var actual = _carpoolBusinessService.GetCarpoolByID(id);

            // Assert
            var expected = new CarpoolDto { CarpoolId = 0 };
            Assert.AreEqual(expected.CarpoolId, actual.CarpoolId);
        }

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            List<Carpool> allCarpools = new List<Carpool>();
            _carpoolDataServiceMock.Setup(x => x.ReadCarpoolData())
                .Returns(allCarpools);

            //Act
            var actualResult = _carpoolBusinessService.GetAllCarpools();

            //Assert
            Assert.IsInstanceOfType(actualResult, typeof(List<CarpoolDto>));
        }
        [TestMethod]
        public void UpdateCarpoolTest()
        {
            //Arrange
            int id = 0;
            string startingPoint = "221133";
            string endingPoint= "Hans Perte";
            int freeSpaces = 5 ;
            string time = "20002";

            List<Carpool> allCarpools = new List<Carpool>();
            _carpoolDataServiceMock.Setup(x => x.ReadCarpoolData())
                .Returns(allCarpools);

            Carpool carpool = new Carpool();
            _carpoolDataServiceMock.Setup(x => x.GetCarpoolDataByID(id))
                .Returns(carpool);

            _carpoolDataServiceMock.Setup(x => x.PrintCarpools(allCarpools));

            //Act
            var actualResult = _carpoolBusinessService.ChangeCarpoolDataByID(id, startingPoint, endingPoint, freeSpaces, time);

            //Assert
            Carpool expectedResult = new Carpool() { CarpoolId = id, StartingPoint = startingPoint, EndingPoint = endingPoint, FreeSpaces = freeSpaces, Time = time, Deleted = false};

            Assert.AreEqual(expectedResult.CarpoolId, actualResult.CarpoolId);
            Assert.AreEqual(expectedResult.StartingPoint, actualResult.StartingPoint);
            Assert.AreEqual(expectedResult.EndingPoint, actualResult.EndingPoint);
            Assert.AreEqual(expectedResult.FreeSpaces, actualResult.FreeSpaces);
            Assert.AreEqual(expectedResult.Time, actualResult.Time);
        }
        [TestMethod]
        public void JoinCarpoolTest()
        {

        }
    }
}