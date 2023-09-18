using Moq;
using Newtonsoft.Json;
using PaymentSystem.Core.Entities;
using PaymentSystem.Core.Repository;
using PaymentSystemAPI.Models;
using System.Net.Http.Headers;
using System.Text;

namespace PaymentSystem.NUnit
{
    public class CustomerControllerTest
    {
        private HttpClient? _client;
        private Mock<IGenericRepository<Customer>> _customerRepository;
        [SetUp]
        public void Setup()
        {
            var application = new PaymentSystemApplication();
            _client = application.CreateClient();

            _customerRepository = new Mock<IGenericRepository<Customer>>();
        }

        [Test]
        public async Task CanCreateCustomer()
        {
            var data = new CustomerModel
            {
                NationalIDNumber = "2193HDJ33293J3",
                DOB = DateTime.Now.AddYears(-19),
                FirstName = "Test1",
                LastName = "TestSurname1",
                PhoneNumber = "08038492921"
            };

            _customerRepository.Setup(m => m.Create(It.IsAny<Customer>())).Returns(Task.CompletedTask);
            _customerRepository.Setup(m => m.SaveChangesAsync()).Returns(Task.CompletedTask);

            var content = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("api/customers/create", byteContent);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
