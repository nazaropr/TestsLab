using RestSharp;
using System.Net;
using NUnit.Framework;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using RestSharp.Authenticators;
using System.Collections;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace RestTestLab_3
{
    public class RestfulBookerTests
    {
        RestClient client;
        RestRequest request;
        IRestResponse response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetBookingIds()
        {
            // Arrange 
            request = new RestRequest("booking", Method.GET);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenCreateBooking()
        {
            // Arrange 
            var testObject = new
            {
                firstname = "Nazarii",
                lastname = "Opryshko",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new {
                    checkin = "2020-01-01",
                    checkout = "2022-01-01"
                },
                additionalneeds = "Breakfast"
            };
            
            request = new RestRequest("booking", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(testObject);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenUpdateBooking()
        {
            // Arrange 
            var testObject = new
            {
                firstname = "Nazarii",
                lastname = "Opryshko",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2020-01-01",
                    checkout = "2022-01-01"
                },
                additionalneeds = "Breakfast"
            };

            request = new RestRequest("booking", Method.GET);
            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddJsonBody(testObject);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var result = new JsonDeserializer().Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["firstname"], Is.EqualTo(testObject.firstname));
            Assert.That(result["lastname"], Is.EqualTo(testObject.lastname));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenDeleteBooking()
        {
            // Arrange 
            request = new RestRequest("booking", Method.GET);
            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.DELETE);
            request.AddHeader("Content-Type", "application/json");

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        private RestClient client1;
        private RestResponse<PostsModels> response1;
        [Test]
        public void GET_whenGetRequestWithMatchId_ShouldBeSuccesResponse()
        {

            RestClient client1 = new RestClient("https://ghibliapi.herokuapp.com/");
            RestRequest request1 = new RestRequest("films/2baf70d1-42bb-4437-b551-e5fed5a87abe", Method.GET);

            //act
            response1 = (RestResponse<PostsModels>)client1.Execute<PostsModels>(request1);

            //assert
            Assert.That(response1.Data.title, Is.EqualTo("Castle in the Sky"));
            Assert.That(response1.Data.director, Is.EqualTo("Hayao Miyazaki"));
        }
    }

    class Booking
    {
        public string bookingId { get; set; }
    }
}