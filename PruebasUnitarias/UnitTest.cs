using System;
using Xunit;
using Test.Controllers;
using Test.EFModels;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    public class UnitTest
    {
        #region test_user
        private Test.Infrastructure.ILog logger;
        [Fact]
        public async void insertUser()
        {
            UserController test = new UserController(logger);
            User usuario = new User {
                Name = "Alfredo Ricardo",
                UserName = "ARMORA2",
                CreateDate = DateTime.Now.ToString(),
                Password = "Pruebas"
            };

            var res = await test.saveUser(usuario);
            Assert.IsType<User>(usuario);

        }

        [Fact]
        public async void updateUser()
        {
            UserController test = new UserController(logger);
            User usuario = new User
            {
                Id = 1,
                Name = "Alfredo Ricardo Morales Perez",
                UserName = "ARMORA2",
                CreateDate = DateTime.Now.ToString(),
                Password = "Tests",
                Status = 1
            };

            var res = await test.saveUser(usuario);
            Assert.IsType<User>(usuario);

        }
        #endregion

        #region test_provider
        [Fact]
        public async void insertProvider()
        {
            ProvidersController test = new ProvidersController(logger);

            Provider provedor = new Provider
            {
                Name = "Muebles Morales"
            };

            var res = test.saveProvider(provedor);
            Assert.IsType<Provider>(provedor);
        }

        [Fact]
        public void updateProvider()
        {
            ProvidersController test = new ProvidersController(logger);

            Provider provedor = new Provider
            {
                Id = 1,
                Status = 1,
                Name = "Muebles Morales Perez"
            };

            var res = test.saveProvider(provedor);
            Assert.IsType<Provider>(provedor);
        }
        #endregion

        #region test_moneda
        [Fact]
        public async void insertCurrency()
        {
            CurrenciesController test = new CurrenciesController(logger);

            Currency currency = new Currency
            {
                Name = "Dolares"
            };

            var res = await test.saveCurrency(currency);
            Assert.IsType<Currency>(currency);
        }
        [Fact]
        public async void UpdateCurrency()
        {
            CurrenciesController test = new CurrenciesController(logger);

            Currency currency = new Currency
            {
                Id = 1,
                Name = "Dolar",
                Status = 1
            };

            var res = await test.saveCurrency(currency);
            Assert.IsType<Currency>(currency);
        }

        #endregion

        #region test_receipts

        #endregion
    }
}
