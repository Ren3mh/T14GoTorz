using Bunit;
using GotorzApp.Components.Pages.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SharedLib;
using SharedLib.Service;
using System.Collections.Generic;
using System;
using Xunit;

namespace ComponentsTesting;

public class PackageViewTest : TestContext
{
    private readonly Mock<ITravelPackageService> packageServiceMock;
    private readonly Mock<ICurrentWeatherService> currentWeatherServiceMock;
    
    public PackageViewTest()
    {
        packageServiceMock = new Mock<ITravelPackageService>();
        Services.AddSingleton(packageServiceMock.Object);

        currentWeatherServiceMock = new Mock<ICurrentWeatherService>();
        Services.AddSingleton(currentWeatherServiceMock.Object);
    }

    [Fact]
    public void Component_Renders_Correctly_When_No_Packages()
    {
        // Arrange
        packageServiceMock.Setup(m => m.GetAll())
            .ReturnsAsync(new List<TravelPackage>());
        // Act
        var cut = RenderComponent<PackagesView>();
        // Assert
        cut.Find("span").MarkupMatches("<span>No packages available.</span>");
    }

    [Fact]
    public void Initially_No_Input_Field()
    {
        // Arrange
        var selectedPackage = new TravelPackage
        {
            Id = 1,
            Title = "Test Package",
            Hotel = new Hotel
            {
                Name = "Test Hotel",
                Address = "Test Address",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1),
                Rate = 100
            },
            Flightpaths = new List<Flightpath>()
        };

        var currentWeather = new CurrentWeather
        {
            Main = new Main { TempMax = 25 }
        };

        // Act
        var cut = RenderComponent<TravelPackageEditor>(parameters => parameters
            .Add(p => p.SelectedPackage, selectedPackage)  //måske denne bare KUN skulle render PackagesView?
            .Add(p => p.CurrentWeather, currentWeather)
        );

        // Assert: Input with id 10000 should not exist initially
        Assert.Throws<ElementNotFoundException>(() => cut.Find("input#HotelNameInput"));
    }

    [Fact]
    public void Clicking_Edit_Button_Shows_Input_Field()
    {
        // Arrange
        var selectedPackage = new TravelPackage
        {
            Id = 1,
            Title = "Test Package",
            Hotel = new Hotel
            {
                Name = "Test Hotel",
                Address = "Test Address",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1),
                Rate = 100
            },
            Flightpaths = new List<Flightpath>()
        };

        var currentWeather = new CurrentWeather
        {
            Main = new Main { TempMax = 25 }
        };
        // Act
        var cut = RenderComponent<TravelPackageEditor>(parameters => parameters
            .Add(p => p.SelectedPackage, selectedPackage)  //måske denne bare KUN skulle render PackagesView?
            .Add(p => p.CurrentWeather, currentWeather)
        );
        // Assert initial state
        Assert.Throws<ElementNotFoundException>(() => cut.Find("input#HotelNameInput")); // Input should not exist initially

        // Act: Click the edit button
        cut.Find("button#edit").Click();

        // Assert: Input with id 10000 should now be visible
        var input = cut.Find("input#HotelNameInput");
        Assert.NotNull(input);
    }

    [Fact]
    public void EditPropertyAndSave()
    {
        // Arrange
        var selectedPackage = new TravelPackage
        {
            Id = 1,
            Title = "Test Package",
            Hotel = new Hotel
            {
                Name = "Test Hotel",
                Address = "Test Address",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1),
                Rate = 100
            },
            Flightpaths = new List<Flightpath>()
        };

        var currentWeather = new CurrentWeather
        {
            Main = new Main { TempMax = 25 }
        };

        // Act
        var cut = RenderComponent<TravelPackageEditor>(parameters => parameters
            .Add(p => p.SelectedPackage, selectedPackage)  //måske denne bare KUN skulle render PackagesView?
            .Add(p => p.CurrentWeather, currentWeather)
        );

        cut.Find("button#edit").Click(); // Find the edit button by its ID or selector
        cut.Find("input#HotelNameInput").Change("New Hotel Name"); // Find the input element by its ID or selector and input text
        cut.Find("button#save").Click(); // Find the save button by its ID or selector

        // Assert
        Assert.Equal("New Hotel Name", selectedPackage.Hotel.Name);
    }
}
