using Bunit;
using GotorzApp.Components.Pages.Presentation;
using Microsoft.AspNetCore.Components;
using Moq;
using Shared;
using Shared.Service;
using System.Collections.Generic;
using System;

namespace ComponentsTesting;

public class FlightsViewTests : TestContext
{
    private readonly Mock<IFlightService> flightServiceMock;

    public FlightsViewTests()
    {
        flightServiceMock = new Mock<IFlightService>();
        flightServiceMock.Setup(m => m.GetAll())
            .ReturnsAsync(new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    IataOrigin = new IataLocation { City = "New York", Iata = "JFK" },
                    IataDestination = new IataLocation { City = "Los Angeles", Iata = "LAX" },
                    ArrivalTime = DateTime.Now
                },

                new Flight
                {
                    Id = 2,
                    IataOrigin = new IataLocation { City = "Copenhagen", Iata = "CPH" },
                    IataDestination = new IataLocation { City = "Paris", Iata = "CDG" },
                    ArrivalTime = DateTime.Now.AddDays(1)
                }
            });
        Services.AddSingleton(flightServiceMock.Object);
    }

    //[Fact]
    //public void Component_Renders_Correctly_When_Loading()
    //{
    //    // Arrange
    //    _mockFlightService.Setup(service => service.GetAll()).ReturnsAsync((List<Flight>?)null);

    //    // Act
    //    var cut = RenderComponent<FlightsView>();

    //    // Assert
    //    cut.Find("span").MarkupMatches("<span>Loading...</span>");
    //}

    [Fact]
    public void Component_Renders_Correctly_When_No_Flights()
    {
        // Arrange
        var flightServiceMock1 = new Mock<IFlightService>();
        flightServiceMock1.Setup(m => m.GetAll())
            .ReturnsAsync(new List<Flight>());

        Services.AddSingleton(flightServiceMock1.Object);

        // Act
        var cut = RenderComponent<FlightsView>();

        // Assert
        cut.Find("span").MarkupMatches("<span>No flights found.</span>");
        
    }

    [Fact]
    public void Component_Renders_Flight_List()
    {
        //Arrange
        // arranged in constructor

        // Act
        var cut = RenderComponent<FlightsView>();

        // Assert
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(2, rows.Count);
        Assert.Contains("New York", rows[0].InnerHtml);
        Assert.Contains("Los Angeles", rows[0].InnerHtml);
        Assert.Contains("Copenhagen", rows[1].InnerHtml);
        Assert.Contains("Paris", rows[1].InnerHtml);
    }

    [Fact]
    public void GoTopc_Navigates_To_PackageCreation()
    {
        // Arrange
        var mockNavigationManager = Services.GetRequiredService<NavigationManager>();
        var cut = RenderComponent<FlightsView>();

        // Act
        cut.Find("button").Click();

        // Assert
        var expectedUri = mockNavigationManager.BaseUri.Substring(0, mockNavigationManager.BaseUri.Length - 1) + "/pc";
        var actualUri = mockNavigationManager.Uri;
        Assert.Equal(expectedUri, actualUri);
    }
}
