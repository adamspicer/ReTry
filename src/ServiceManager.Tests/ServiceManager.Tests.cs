﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceManager.Tests.cs" company="Jamie Dixon Ltd.">
//   Copyright (c) Jamie Dixon Ltd. All rights reserved.
// </copyright>
// <summary>
//   Defines the Given_ServiceManager_Instance type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceManager.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Service_Manager;

    [TestClass]
    public class Given_ServiceManager_Instance
    {
        [TestClass]
        public class When_ExecuteService_Invoked
        {
            [TestMethod]
            public void Passed_Method_Is_Called()
            {
                // Arrange
                var serviceManager = new ServiceManager();

                // Act
                var result = serviceManager.ExecuteService(() => "Hello World", 5).Result<string>();

                // Assert
                Assert.AreEqual("Hello World", result);
            }
        }

        [TestClass]
        public class When_IfServiceFailsThen_Invoked
        {
            [TestMethod]
            public void Passed_Method_Is_Called_When_Exception_On_ExecuteService()
            {
                // Arrange
                var serviceManager = new ServiceManager();

                // Act
                var result = serviceManager
                    .ExecuteService<string>(() => { throw new Exception("Boom"); }, 1)
                    .IfServiceFailsThen(() => "Hello World")
                    .Result<string>();

                // Assert
                Assert.AreEqual("Hello World", result);
            }
        }

        [TestClass]
        public class When_Result_Invoked
        {
            [TestMethod]
            public void Result_Contains_Last_IfServiceFailsThen_Method_Result()
            {
                // Arrange
                var serviceManager = new ServiceManager();

                // Act
                var result = serviceManager
                    .ExecuteService<string>(() => { throw new Exception("Boom"); }, 1)
                    .IfServiceFailsThen(() => "Hello World")
                    .IfServiceFailsThen(() => "You Rock")
                    .IfServiceFailsThen(() => "My World!")
                    .Result<string>();
                
                // Assert
                Assert.AreEqual("My World!", result);
            }
        }
    }
}