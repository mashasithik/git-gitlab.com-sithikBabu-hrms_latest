using FakeItEasy;
using FluentAssertions;
using HRMS.Adecco.Management.BusinessAccessLayer.Master;
using HRMS.Adecco.Management.Common.Master;
using HRMS.Adecco.Management.CommonLayer;
using HRMS.Adecco.WebAPI.xUnitTest.Fixtures;
using HRMS.Management.WebAPI.Controllers.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Adecco.WebAPI.xUnitTest.Master
{
   
    public class LocationControllerTest : IClassFixture<ControllerFixture>
    {
        LocationController LocationController;

        public LocationControllerTest(ControllerFixture fixture)
        {
            LocationController = fixture.locationController;
        }
        [Fact]
        public void Get_WithoutParam_Ok_Test()
        {
            var result = LocationController.GetAllLocation() as LocationCollectionDataModel;

            Assert.Equal(CoreResponseStatus.Success, result.Status);
            Assert.True((result.LocationModelList as Location[]).Length > 1);
        }
    }
}
