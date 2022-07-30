/*
 * Production API
 *
 * API exposing endpoints for managing well headers and daily production.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using Org.OpenAPITools.Client;
using Org.OpenAPITools.Api;
// uncomment below to import models
//using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test.Api
{
    /// <summary>
    ///  Class for testing WellHeaderApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class WellHeaderApiTests : IDisposable
    {
        private WellHeaderApi instance;

        public WellHeaderApiTests()
        {
            instance = new WellHeaderApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of WellHeaderApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' WellHeaderApi
            //Assert.IsType<WellHeaderApi>(instance);
        }

        /// <summary>
        /// Test ProductionAddWellHeader
        /// </summary>
        [Fact]
        public void ProductionAddWellHeaderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //WellHeaderInput wellHeaderInput = null;
            //var response = instance.ProductionAddWellHeader(wellHeaderInput);
            //Assert.IsType<WellHeader>(response);
        }

        /// <summary>
        /// Test ProductionAddWellHeaders
        /// </summary>
        [Fact]
        public void ProductionAddWellHeadersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //List<WellHeaderInput> wellHeaderInput = null;
            //var response = instance.ProductionAddWellHeaders(wellHeaderInput);
            //Assert.IsType<List<WellHeader>>(response);
        }

        /// <summary>
        /// Test ProductionDeleteWellHeader
        /// </summary>
        [Fact]
        public void ProductionDeleteWellHeaderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string uwi = null;
            //var response = instance.ProductionDeleteWellHeader(uwi);
            //Assert.IsType<bool>(response);
        }

        /// <summary>
        /// Test ProductionGetWellHeader
        /// </summary>
        [Fact]
        public void ProductionGetWellHeaderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string uwi = null;
            //var response = instance.ProductionGetWellHeader(uwi);
            //Assert.IsType<WellHeader>(response);
        }

        /// <summary>
        /// Test ProductionGetWellHeaders
        /// </summary>
        [Fact]
        public void ProductionGetWellHeadersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? offset = null;
            //int? limit = null;
            //var response = instance.ProductionGetWellHeaders(offset, limit);
            //Assert.IsType<List<WellHeader>>(response);
        }

        /// <summary>
        /// Test ProductionUpdateWellHeader
        /// </summary>
        [Fact]
        public void ProductionUpdateWellHeaderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //WellHeader wellHeader = null;
            //var response = instance.ProductionUpdateWellHeader(wellHeader);
            //Assert.IsType<WellHeader>(response);
        }
    }
}