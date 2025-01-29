using Microsoft.AspNetCore.Mvc;
using Moq;
using YandexMusic.Application;
using YandexMusic.Application.Services;
using YandexMusic.Controllers.user;
using YandexMusic.DataAccess.DTOs;
namespace TestProject1;

public class UnitTest1
{
   
     private readonly Mock<ICardService> _cardServiceMock;
     private readonly Mock<IGenresService> _genresServiceMock;
    private readonly Mock<ITarrifTypeService> _tarriftypeService;
   
    public UnitTest1()
    {
        _cardServiceMock = new Mock<ICardService>();
        _genresServiceMock = new Mock<IGenresService>();
        _tarriftypeService = new Mock<ITarrifTypeService>();

    }
    [Fact]
    public async Task CardCreationTest_ShouldReturnSuccess()
    {
        var CardDTo = new CardDTO()
        {
            cardNumber = "4321124596357841",
            ExpiredDate = "23.12.2026",
            UserID = Guid.Parse("0194635b-03e5-7f3c-bc0b-5f3498293e12")
        };
        _cardServiceMock
            .Setup(service => service.CreateCard(CardDTo))
            .ReturnsAsync("New Card add Succesfuly");

        var service = _cardServiceMock.Object;

        string result = await service.CreateCard(CardDTo);

        Assert.Equal("New Card add Succesfuly", result);
    }
    [Fact]
    public async Task DeleteGenre_ShouldReturn()
    {
        Guid id = Guid.Parse("01946997-d975-7084-b6ed-f58c2b15e132");
        _genresServiceMock
            .Setup(service => service.DeleteGenresAsync(id))
            .ReturnsAsync(true);

        var service = _genresServiceMock.Object;

        bool result = await service.DeleteGenresAsync(id);
        Assert.True(result);

    }
    [Fact]
    public async void AddTarrif_ReturnsOKResult()
    {

        var newTarrif = new TarrifTypeDTO() { Amount = 100, Type = "Super" };
        _tarriftypeService.Setup(service => service.AddTarrifAsync(newTarrif)).ReturnsAsync(newTarrif);

        var controller = new TarrifTypeController(_tarriftypeService.Object);

        var result  = await controller.AddTarrif(newTarrif);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<TarrifTypeDTO>(okResult.Value);
        Assert.Equal(100, returnValue.Amount); 
        Assert.Equal("Super", returnValue.Type);
    }
    
}

