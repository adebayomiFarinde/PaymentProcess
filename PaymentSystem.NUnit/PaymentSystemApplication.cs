using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework.Interfaces;
using PaymentSystem.Core.Entities;
using PaymentSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.NUnit
{
    class PaymentSystemApplication : WebApplicationFactory<Program>
    {
        private readonly IGenericRepository<Customer> _genericCustomerRepository;
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IGenericRepository<Customer>>();
                services.TryAddTransient(_ => _genericCustomerRepository);
            });
            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }
}
