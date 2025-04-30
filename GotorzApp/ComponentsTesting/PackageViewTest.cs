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
    private readonly Mock<IService<TravelPackage>> _mockPackageService;


    public PackageViewTest()
    {
        _mockPackageService = new Mock<IService<TravelPackage>>();
        Services.AddSingleton(_mockPackageService.Object);

    }



    
}
