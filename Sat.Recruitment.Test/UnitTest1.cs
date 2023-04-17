using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Entities.Dto;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController();

            var userDto = new UserDTO
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = 124
            };

            var result = userController.CreateUser(userDto);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            var userDto = new UserDTO
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = 124
            };

            var result = userController.CreateUser(userDto);


            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void CalculateUserMoney_NormalUserMoneyGreaterThan100_ReturnsCorrectGif()
        {
            // Arrange
            var newUser = new UserDTO
            {
                userType = "Normal",
                money = 150
            };

            var userService = new UserService();

            // Act
            userService.calculateUserMoney(ref newUser);

            // Assert
            Assert.Equal(18, newUser.money - 150);
        }

        [Fact]
        public void CalculateUserMoney_SuperUserMoneyGreaterThan100_ReturnsCorrectGif()
        {
            // Arrange
            var newUser = new UserDTO
            {
                userType = "SuperUser",
                money = 150
            };

            var userService = new UserService();

            // Act
            userService.calculateUserMoney(ref newUser);

            // Assert
            Assert.Equal(30, newUser.money - 150);
        }

        [Fact]
        public void NormalizeEmail_ValidEmail_ReturnsLowercaseEmail()
        {
            // Arrange
            var email = "Test@Example.com";

            var userService = new UserService();

            // Act
            var result = userService.normalizeEmail(email);

            // Assert
            Assert.Equal("test@example.com", result);
        }
    }
}
