using Bunit;
using GotorzApp.Components.Pages.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared;
using Shared.Service;
using System.Collections.Generic;
using System;
using Xunit;

namespace ComponentsTesting;

public class FlightsViewTests : TestContext
{
    private readonly Mock<IService<Flight>> _mockFlightService;

    public FlightsViewTests()
    {
        _mockFlightService = new Mock<IService<Flight>>();
        Services.AddSingleton(_mockFlightService.Object);
    }

    [Fact]
    public void Component_Renders_Correctly_When_Loading()
    {
        // Arrange
        _mockFlightService.Setup(service => service.GetAll()).ReturnsAsync((List<Flight>?)null);

        // Act
        var cut = RenderComponent<FlightsView>();

        // Assert
        cut.Find("span").MarkupMatches("<span>Loading...</span>");
    }

    [Fact]
    public void Component_Renders_Correctly_When_No_Flights()
    {
        // Arrange
        _mockFlightService.Setup(service => service.GetAll()).ReturnsAsync(new List<Flight>());

        // Act
        var cut = RenderComponent<FlightsView>();

        // Assert
        cut.Find("span").MarkupMatches("<span>No flights found.</span>");
        
    }

    [Fact]
    public void Component_Renders_Flight_List()
    {
        // Arrange
        var flights = new List<Flight>
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
                IataOrigin = new IataLocation { City = "Chicago", Iata = "ORD" },
                IataDestination = new IataLocation { City = "Miami", Iata = "MIA" },
                ArrivalTime = DateTime.Now
            }
        };

        _mockFlightService.Setup(service => service.GetAll()).ReturnsAsync(flights);

        // Act
        var cut = RenderComponent<FlightsView>();

        // Assert
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(2, rows.Count);
        Assert.Contains("New York", rows[0].InnerHtml);
        Assert.Contains("Los Angeles", rows[0].InnerHtml);
        Assert.Contains("Chicago", rows[1].InnerHtml);
        Assert.Contains("Miami", rows[1].InnerHtml);
    }

    [Fact]
    public void GoTopc_Navigates_To_FlightCreation()
    {
        // Arrange
        var mockNavigationManager = Services.GetRequiredService<NavigationManager>();
        var cut = RenderComponent<FlightsView>();

        // Act
        cut.Find("button").Click();

        // Assert
        Assert.Equal(mockNavigationManager.BaseUri.Substring(0, mockNavigationManager.BaseUri.Length - 1) + "/pc", mockNavigationManager.Uri);
    }
}
