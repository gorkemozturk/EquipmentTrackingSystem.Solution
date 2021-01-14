using EquipmentTrackingSystem.Extension.Interfaces;
using EquipmentTrackingSystem.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace EquipmentTrackingSystem.IntegrationTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;

        public IntegrationTest()
        {
            _client = new WebApplicationFactory<Startup>().CreateClient();
        }
    }
}
