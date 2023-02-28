using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using HFT.Logic.Services;
using HFT.Models;
using HFT.Repository.Interfaces;

namespace HFT.Test
{
    [TestFixture]
    public class CarLogicTester
    {
        CarLogic cl;
        Mock<ICarRepository> mockCarRepository;
        Mock<IOwnerRepository> mockOwnerRepository;
        Mock<IBrandRepository> mockBrandRepository;

        [SetUp]
        public void Init()
        {
            Brand fakeBrand1 = new Brand() { Id = 1, Name = "Tesla" };
            Brand fakeBrand2 = new Brand() { Id = 2, Name = "TeslaYes" };
            Brand fakeBrand3 = new Brand() { Id = 3, Name = "TeslaNo" };

            Owner fakeOwner1 = new Owner() { Id = 1, Name = "Attila" };
            Owner fakeOwner2 = new Owner() { Id = 2, Name = "Patrick" };

            var fakeCars = new List<Car>()
            {
                new Car() { Id = 1, BasePrice = 1000, Model = "Tesla Model 1", BrandId = fakeBrand1.Id, Brand = fakeBrand1, OwnerId = fakeOwner1.Id, Owner = fakeOwner1 },
                new Car() { Id = 2, BasePrice = 1000, Model = "TeslaYes Model 1", BrandId = fakeBrand2.Id, Brand = fakeBrand2, OwnerId = fakeOwner1.Id, Owner = fakeOwner1 },
                new Car() { Id = 3, BasePrice = 1000, Model = "TeslaNo Model 1", BrandId = fakeBrand3.Id, Brand = fakeBrand3, OwnerId = fakeOwner2.Id, Owner = fakeOwner2 }
            }.AsQueryable();

            var fakeOwners = new List<Owner>()
            {
                fakeOwner1,
                fakeOwner2
            }.AsQueryable();

            var fakeBrands = new List<Brand>()
            {
                fakeBrand1,
                fakeBrand2,
                fakeBrand3
            }.AsQueryable();

            mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(r => r.ReadAll()).Returns(fakeCars);
            mockOwnerRepository = new Mock<IOwnerRepository>();
            mockOwnerRepository.Setup(r => r.ReadAll()).Returns(fakeOwners);
            mockBrandRepository = new Mock<IBrandRepository>();
            mockBrandRepository.Setup(r => r.ReadAll()).Returns(fakeBrands);
            cl = new CarLogic(mockCarRepository.Object, mockOwnerRepository.Object, mockBrandRepository.Object);
        }

        [Test]
        public void TestCreateCarwithValidName()
        {
            var car = new Car() { Model = "TeslaYes Model 2" };
            cl.Create(car);

            mockCarRepository.Verify(r => r.Create(car), Times.Once);
        }

        [Test]
        public void TestCreateCarwithInvalidName()
        {
            var car = new Car() { Model = "" };
            Assert.Throws<InvalidOperationException>(() => cl.Create(car));
        }


        [Test]
        public void TestAVGPrice()
        {
            var result = cl.AVGPrice();
            Assert.That(result, Is.EqualTo(1000));
        }

        [Test]
        public void TestAVGPriceByBrands()
        {
            var result = cl.AVGPriceByBrands();
            var expected = new List<KeyValuePair<string, double>>()
            {
                {
                    new KeyValuePair<string, double>("Tesla", 1000)
                },
                {
                    new KeyValuePair<string, double>("TeslaYes", 1000)
                },
                {
                    new KeyValuePair<string, double>("TeslaNo", 1000)
                }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestAVGPriceByOwners()
        {
            var result = cl.AVGPriceByOwners();
            var expected = new List<KeyValuePair<string, double>>()
            {
                {
                    new KeyValuePair<string, double>("Attila", 1000)
                },
                {
                    new KeyValuePair<string, double>("Patrick", 1000)
                }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestDelete()
        {
            cl.Delete(1);

            mockCarRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void TestReadWithInvalidIdThrowsException()
        {
            mockCarRepository
                .Setup(r => r.Read(It.IsAny<int>()))
                .Returns(value: null);

            Assert.Throws<ArgumentException>(() => cl.Read(1));
        }

        [Test]
        public void TestReadWithValidId()
        {
            Car expected = new Car()
            {
                Id = 1,
                Model = "Tesla",
                BasePrice = 1000
            };

            mockCarRepository
                .Setup(r => r.Read(0))
                .Returns(expected);
            var actual = cl.Read(0);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestOwnersByBrandName()
        {
            var result = cl.OwnersByBrandName("Tesla");
            var expected = new List<string>() { "Attila" };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestOwnersByBrandNameInvalidBrandName()
        {
            Assert.Throws<ArgumentException>(() => cl.OwnersByBrandName(""));
        }

        [Test]
        public void TestGetOwnersWithCars()
        {
            var result = cl.GetOwnersWithCars();
            var expected = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(
                    "Attila",
                    "Tesla Model 1, TeslaYes Model 1"
                ),
                new KeyValuePair<string, string>(
                    "Patrick",
                    "TeslaNo Model 1"
                )
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetCarsByBrands()
        {
            var result = cl.GetCarsByBrands();
            var expected = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(
                    "Tesla",
                    "Tesla Model 1"
                ),
                new KeyValuePair<string, string>(
                    "TeslaYes",
                    "TeslaYes Model 1"
                ),
                new KeyValuePair<string, string>(
                    "TeslaNo",
                    "TeslaNo Model 1"
                )
            };

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}