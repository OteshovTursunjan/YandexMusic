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
    [Fact] //Атбрибут xUnit, обозначающий что этот атрибу тестовый
    public async Task CardCreationTest_ShouldReturnSuccess()
    {
        // Arrange  Подготовка
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
        // Act  Действия 

        string result = await service.CreateCard(CardDTo);
        // Assert Проверка

        Assert.Equal("New Card add Succesfuly", result);
    }
    [Fact]
    public async Task DeleteGenre_ShouldReturnTrue()
    {
        //Arrange  Preparation 
        Guid id = Guid.Parse("01946997-d975-7084-b6ed-f58c2b15e185");
        

        //Создается фейковый сервис 
        _genresServiceMock
            .Setup(service => service.DeleteGenresAsync(id))
            .ReturnsAsync(true);


        //Act  Действия 
        //Получаем объект из мок объекта теперь service работет как в Setup
        var service = _genresServiceMock.Object; 


        // Assert Сhecnking 
        bool result = await service.DeleteGenresAsync(id); // Running  moc-object 


        Assert.True(result); //Result checking  

    }
    [Fact] //Атбрибут xUnit, обозначающий что этот атрибу тестовый
    public async void AddTarrif_ReturnsOKResult() 
    {
        var newTarrif = new TarrifTypeDTO() { Amount = 100, Type = "Super" };
        _tarriftypeService.Setup(service => service.AddTarrifAsync(newTarrif)).ReturnsAsync(newTarrif);


        var controller = new TarrifTypeController(_tarriftypeService.Object);
        var result  = await controller.AddTarrif(newTarrif);


        var okResult = Assert.IsType<OkObjectResult>(result); // Вернул HTTP 200
        var returnValue = Assert.IsType<TarrifTypeDTO>(okResult.Value); // возрашает иммено DTO а не другое 
        Assert.Equal(100, returnValue.Amount); 
        Assert.Equal("Super", returnValue.Type);
    }

    
}

