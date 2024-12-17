using Moq;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Services.Tests
{
	[TestFixture]
	public class WoodcarverServiceTests
	{
		private Mock<IWoodcarverRepository> woodcarverRepositoryMock;
		private IWoodcarverService woodcarverService;

		private IList<Woodcarver> woodcarversData;

		[SetUp]
		public void Setup()
		{
			// Setup data for tests
			woodcarversData = new List<Woodcarver>
			{
				new Woodcarver
				{
					Id = Guid.NewGuid(),
					FirstName = "John",
					LastName = "Doe",
					Age = 45,
					PhoneNumber = "123-456-7890",
					CityId = Guid.NewGuid(),
					ImageUrl = "/images/john.jpg",
					IsDeleted = false
				},
				new Woodcarver
				{
					Id = Guid.NewGuid(),
					FirstName = "Jane",
					LastName = "Smith",
					Age = 37,
					PhoneNumber = "987-654-3210",
					CityId = Guid.NewGuid(),
					ImageUrl = "/images/jane.jpg",
					IsDeleted = false
				}
			};

			// Mock repository
			woodcarverRepositoryMock = new Mock<IWoodcarverRepository>();

			woodcarverService = new WoodcarverService(woodcarverRepositoryMock.Object);
		}

		[Test]
		public async Task GetAllWoodcarversAsync_ShouldReturnWoodcarvers()
		{
			// Arrange
			woodcarverRepositoryMock
				.Setup(repo => repo.GetAllAttached())
				.Returns(woodcarversData.AsQueryable());

			// Act
			var result = await woodcarverService.GetAllWoodcarversAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.That(result.Any(w => w.FirstName == "John"));
			Assert.That(result.Any(w => w.FirstName == "Jane"));
		}

		[Test]
		public async Task CreateWoodcarverAsync_ValidInput_ShouldReturnTrue()
		{
			// Arrange
			var inputModel = new WoodcarverCreateViewModel
			{
				FirstName = "Michael",
				LastName = "Johnson",
				Age = 50,
				PhoneNumber = "555-555-5555",
				CityId = Guid.NewGuid(),
				ImageUrl = null // Check default image behavior
			};

			woodcarverRepositoryMock
				.Setup(repo => repo.AddAsync(It.IsAny<Woodcarver>()))
				.Returns(Task.CompletedTask);

			woodcarverRepositoryMock
				.Setup(repo => repo.SaveChangesAsync())
				.Returns(Task.CompletedTask);

			// Act
			var result = await woodcarverService.CreateWoodcarverAsync(inputModel);

			// Assert
			Assert.IsTrue(result);
			woodcarverRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Woodcarver>()), Times.Once);
			woodcarverRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
		}

		[Test]
		public async Task CreateWoodcarverAsync_NullInput_ShouldReturnFalse()
		{
			// Act
			var result = await woodcarverService.CreateWoodcarverAsync(null);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetWoodcarverDetailsAsync_ValidId_ReturnsDetailsViewModel()
		{
			// Arrange
			var woodcarverId = Guid.NewGuid();
			var city = new City { Id = Guid.NewGuid(), CityName = "Test City" };

			var woodcarver = new Woodcarver
			{
				Id = woodcarverId,
				FirstName = "John",
				LastName = "Doe",
				Age = 30,
				PhoneNumber = "123456789",
				CityId = city.Id,
				City = city,
				ImageUrl = "test.jpg",
				IsDeleted = false
			};

			var mockWoodcarvers = new List<Woodcarver> { woodcarver }.AsQueryable();

			woodcarverRepositoryMock
				.Setup(r => r.GetAllAttached())
			.Returns(mockWoodcarvers);

			IWoodcarverService service = new WoodcarverService(woodcarverRepositoryMock.Object);

			// Act
			var result = await service.GetWoodcarverDetailsAsync(woodcarverId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(woodcarverId, result.Id);
			Assert.AreEqual($"{woodcarver.FirstName} {woodcarver.LastName}", result.FullName);
			Assert.AreEqual(city.CityName, result.CityName);
		}


		[Test]
		public async Task GetWoodcarverDetailsAsync_InvalidId_ShouldReturnNull()
		{
			// Arrange
			woodcarverRepositoryMock
				.Setup(repo => repo.GetAllAttached())
				.Returns(woodcarversData.AsQueryable());

			// Act
			var result = await woodcarverService.GetWoodcarverDetailsAsync(Guid.NewGuid());

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public async Task DeleteWoodcarverAsync_ValidId_ReturnsTrue()
		{
			// Arrange
			var woodcarverId = Guid.NewGuid();

			var woodcarver = new Woodcarver
			{
				Id = woodcarverId,
				FirstName = "Jane",
				LastName = "Doe",
				IsDeleted = false
			};

			var mockWoodcarvers = new List<Woodcarver> { woodcarver }.AsQueryable();

			woodcarverRepositoryMock
				.Setup(r => r.GetAllAttached())
				.Returns(mockWoodcarvers);

			woodcarverRepositoryMock
				.Setup(r => r.Update(It.IsAny<Woodcarver>()));

			IWoodcarverService service = new WoodcarverService(woodcarverRepositoryMock.Object);

			// Act
			var result = await service.DeleteWoodcarverAsync(woodcarverId);

			// Assert
			Assert.IsTrue(result);
			woodcarverRepositoryMock.Verify(r => r.Update(It.Is<Woodcarver>(w => w.Id == woodcarverId && w.IsDeleted)), Times.Once);
			woodcarverRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
		}


		[Test]
		public async Task DeleteWoodcarverAsync_InvalidId_ShouldReturnFalse()
		{
			// Arrange
			woodcarverRepositoryMock
				.Setup(repo => repo.GetAllAttached())
				.Returns(woodcarversData.AsQueryable());

			// Act
			var result = await woodcarverService.DeleteWoodcarverAsync(Guid.NewGuid());

			// Assert
			Assert.IsFalse(result);
		}
	}
}
