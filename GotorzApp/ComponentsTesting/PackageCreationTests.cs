using GotorzApp.Components.Pages.PackageCreationComponents;
using GotorzApp.Components.Pages.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Shared;
using Shared.Data;
using Shared.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsTesting;

public class PackageCreationTests : TestContext
{
    private readonly Mock<ITravelPackageService> packageServiceMock;
    private readonly Mock<IHotelService> hotelServiceMock;
    private readonly Mock<IIataLocationService> iataLocationServiceMock;
    private readonly Mock<ICurrentWeatherService> currentWeatherServiceMock;
    public PackageCreationTests()
    {
        packageServiceMock = new Mock<ITravelPackageService>();
        Services.AddSingleton(packageServiceMock.Object);

        hotelServiceMock = new Mock<IHotelService>();
        Services.AddSingleton(hotelServiceMock.Object);

        iataLocationServiceMock = new Mock<IIataLocationService>();
        Services.AddSingleton(iataLocationServiceMock.Object);

        currentWeatherServiceMock = new Mock<ICurrentWeatherService>();
        Services.AddSingleton(currentWeatherServiceMock.Object);
    }

    [Fact]
    public void Component_Renders_Correctly()
    {
        // Arrange
        iataLocationServiceMock.Setup(m => m.GetAll())
            .ReturnsAsync(new List<IataLocation>
            {
                new IataLocation { Id = 2, Iata = "JFK", City = "New York" },
                new IataLocation { Id = 3, Iata = "LAX", City = "Los Angeles" },
            });

        // Configure JSInterop to handle the call to window.blazorBootstrap.collapse.initialize
        JSInterop.SetupVoid("window.blazorBootstrap.collapse.initialize", _ => true);

        // Act
        var cut = RenderComponent<PackageCreation>();

        // Assert
        cut.Find("h1").MarkupMatches("<h1>Package Createion</h1>");
    }

    [Fact]
    public async Task SubmitTravelPackage_SuccessAsync()
    {
        // Arrange
        var travelPackageToInsert = new TravelPackage
        {
            Title = "Test Package",
            Description = "Test Description",
            Flightpaths = new List<Flightpath>
        {
            new Flightpath
            {
                Fare = 1000,
                Luggage = true,
                OutboundFlight = new Flight()
                {
                    DepartureTime = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
                    IataOriginId = 2,
                    IataDestinationId = 3
                },
                HomeboundFlight = new Flight()
                {
                    DepartureTime = DateTime.Now.AddDays(2),
                    ArrivalTime = DateTime.Now.AddDays(2).AddHours(2),
                    IataDestinationId = 2,
                    IataOriginId = 3
                },
            },
        },
            Hotel = new Hotel()
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Name = "Test Hotel",
                Address = "123 Test St",
                Telephonenumber = "1234567890",
                Email = "test@email.com",
                Rate = 200,
                Description = "Test Hotel Description"
            }
        };

        packageServiceMock.Setup(service => service.Add(It.IsAny<TravelPackage>())).ReturnsAsync(true);

        JSInterop.SetupVoid("window.blazorBootstrap.collapse.initialize", _ => true);

        var cut = RenderComponent<PackageCreation>();
        cut.Instance.newTravelPackage = travelPackageToInsert;
        cut.Render();

        // Act
        cut.Find("button#submitBtn").Click();
        cut.Render(); // Force a re-render of the component

        //// Debugging output
        //var markup = cut.Markup;
        //Console.WriteLine(markup); // Print the component's markup to the console

        // Assert
        Assert.True(cut.Instance.added);
        packageServiceMock.Verify(service => service.Add(It.IsAny<TravelPackage>()), Times.Once);
        cut.Find("h3").MarkupMatches("<h3>Travel Package Created</h3>");
    }
}