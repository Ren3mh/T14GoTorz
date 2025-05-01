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


}
