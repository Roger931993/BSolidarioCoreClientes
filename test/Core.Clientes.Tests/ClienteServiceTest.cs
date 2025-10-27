using Core.Clientes.API.Controllers;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Cliente.Commands;
using Core.Clientes.Application.Features.Cliente.Queries;
using Core.Clientes.Application.Interfaces.Infraestructure;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Core.Clientes.Tests
{
    public class ClienteServiceTest
    {
        [TestFixture]
        public class ClientesControllerTests
        {
            private Mock<IMediator> _mediatorMock;
            private Mock<IMemoryCacheLocalService> _cacheLocalMock;
            private Mock<IRedisCache> _redisCacheMock;
            private ClientesController _controller;

            [SetUp]
            public void Setup()
            {
                _mediatorMock = new Mock<IMediator>();
                _cacheLocalMock = new Mock<IMemoryCacheLocalService>();
                _redisCacheMock = new Mock<IRedisCache>();

                _controller = new ClientesController(_mediatorMock.Object, _cacheLocalMock.Object, _redisCacheMock.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                };
            }

            [Test]
            public async Task GetAllQuery_ShouldReturnOk_WithExpectedResponse()
            {
                // Arrange
                var expectedResponse = new ResponseBase<GetClienteResponse>
                {
                   data = new GetClienteResponse()
                   {
                       cliente = new List<Application.DTOs.clienteDto>()
                       {
                           new Application.DTOs.clienteDto()
                           {
                               apellido_materno = "apellido_materno1",
                                apellido_paterno= "apellido_paterno1",
                               cliente_id = 1,
                               identificacion = "1234567895",
                               primer_nombre = "primern_ombre1",
                               segundo_nombre = "segundo_nombre1",                               
                           },
                           new Application.DTOs.clienteDto()
                           {
                               apellido_materno = "apellido_materno2",
                                apellido_paterno= "apellido_paterno2",
                               cliente_id = 2,
                               identificacion = "1234567896",
                               primer_nombre = "primern_ombre2",
                               segundo_nombre = "segundo_nombre2",
                           }
                       }
                   },
                   error = new Error()
                   {
                       codeError = 10000,
                       messageError = string.Empty,
                       success = true
                   }
                };

                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<GetClienteQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

                // Crear HttpContext simulado
                var httpContext = new DefaultHttpContext();
                httpContext.SetEndpoint(new Endpoint(
                    context => Task.CompletedTask,
                    new EndpointMetadataCollection(),
                    "FakeEndpoint"
                ));

                _controller.ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                };

                // Act
                var result = await _controller.GetAllQuery();

                // Assert  
                var okResult = result.Result as ObjectResult;
                okResult.Should().NotBeNull();
                okResult.StatusCode.Should().Be(200);
                (okResult.Value as ResponseBase<GetClienteResponse>).Should().BeEquivalentTo(expectedResponse);
            }

            [Test]
            public async Task GetById_ShouldSendQuery_WithCorrectId()
            {
                // Arrange
                var expectedResponse = new ResponseBase<GetClienteResponse>
                {
                   data = new GetClienteResponse()
                   {
                       cliente = new List<Application.DTOs.clienteDto>()
                       {
                           new Application.DTOs.clienteDto()
                           {
                               apellido_materno = "apellido_materno1",
                                apellido_paterno= "apellido_paterno1",
                               cliente_id = 1,
                               identificacion = "1234567895",
                               primer_nombre = "primern_ombre1",
                               segundo_nombre = "segundo_nombre1",
                           }                          
                       }
                   },
                    error = new Error()
                    {
                        codeError = 10000,
                        messageError = string.Empty,
                        success = true
                    }
                };

                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<GetClienteQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);


                // Crear HttpContext simulado
                var httpContext = new DefaultHttpContext();
                httpContext.SetEndpoint(new Endpoint(
                    context => Task.CompletedTask,
                    new EndpointMetadataCollection(),
                    "FakeEndpoint"
                ));

                _controller.ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                };

                // Act
                var result = await _controller.GetById(1);

                // Assert
                _mediatorMock.Verify(m => m.Send(
                    It.Is<GetClienteQuery>(q => q.request.Request!.cliente_id == 1),
                    It.IsAny<CancellationToken>()), Times.Once);

                (result.Result as ObjectResult)?.StatusCode.Should().Be(200);
            }

            [Test]
            public async Task Register_ShouldReturnOk_WhenCommandSucceeds()
            {
                // Arrange
                var commandResponse = new ResponseBase<RegisterClienteResponse>
                {
                   data = new RegisterClienteResponse
                   {
                       cliente = new Application.DTOs.clienteDto()
                           {
                               apellido_materno = "apellido_materno1",
                               apellido_paterno = "apellido_paterno1",
                               cliente_id = 1,
                               identificacion = "1234567895",
                               primer_nombre = "primern_ombre1",
                               segundo_nombre = "segundo_nombre1",
                           }
                   },
                   error = new Error
                   {
                       codeError = 10000,
                       messageError = string.Empty,
                       success = true
                   }
                };

                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<RegisterClienteCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(commandResponse);


                // Crear HttpContext simulado
                var httpContext = new DefaultHttpContext();
                httpContext.SetEndpoint(new Endpoint(
                    context => Task.CompletedTask,
                    new EndpointMetadataCollection(),
                    "FakeEndpoint"
                ));

                _controller.ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                };

                var request = new RegisterClienteRequest();

                // Act
                var result = await _controller.Register(request);

                // Assert
                var okResult = result.Result as ObjectResult;
                okResult.Should().NotBeNull();
                okResult!.StatusCode.Should().Be(200);
                okResult.Value.Should().BeEquivalentTo(commandResponse);
            }
        }
    }
}
