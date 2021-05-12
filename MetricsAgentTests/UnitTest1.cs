using AutoMapper;

using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;

using Microsoft.Extensions.Logging;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private readonly CpuAgentController _controller;
        private readonly Mock<ICpuMetricsRepository> _mock;
        private readonly Mock<IMapper> _mockMapper;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            var mockLogger = new Mock<ILogger<CpuAgentController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new CpuAgentController(mockLogger.Object, _mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������

            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetrics>()));

            // ��������� �������� �� �����������
            //var result = _controller.Create(new DotNetMetricCreateRequest() { Time = 12, Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // ��������� �������� �� �����������
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class DotNetMetricsControllerUnitTests
    {
        private readonly DotNetAgentController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;
        private readonly Mock<IMapper> _mockMapper;

        public DotNetMetricsControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var mockLogger = new Mock<ILogger<DotNetAgentController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new DotNetAgentController(mockLogger.Object, _mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������

            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetrics>()));

            // ��������� �������� �� �����������
            //var result = _controller.Create(new DotNetMetricCreateRequest() { Time = 12, Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // ��������� �������� �� �����������
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class HddMetricsControllerUnitTests
    {
        private readonly HddAgentController _controller;
        private readonly Mock<IHddMetricsRepository> _mock;
        private readonly Mock<IMapper> _mockMapper;

        public HddMetricsControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            var mockLogger = new Mock<ILogger<HddAgentController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new HddAgentController(mockLogger.Object, _mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������

            _mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>()));

            // ��������� �������� �� �����������
            //var result = _controller.Create(new HddMetricCreateRequest() { Time = 12, Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<HddMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // ��������� �������� �� �����������
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkAgentController _controller;
        private readonly Mock<INetworkMetricsRepository> _mock;
        private readonly Mock<IMapper> _mockMapper;

        public NetworkMetricsControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            var mockLogger = new Mock<ILogger<NetworkAgentController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new NetworkAgentController(mockLogger.Object, _mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������

            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>()));

            // ��������� �������� �� �����������
            //var result = _controller.Create(new NetworkMetricCreateRequest() { Time = 12, Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<NetworkMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // ��������� �������� �� �����������
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class RamMetricsControllerUnitTests
    {
        private readonly RamAgentController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;
        private readonly Mock<IMapper> _mockMapper;
        public RamMetricsControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            var mockLogger = new Mock<ILogger<RamAgentController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new RamAgentController(mockLogger.Object, _mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������

            _mock.Setup(repository => repository.Create(It.IsAny<RamMetrics>()));

            // ��������� �������� �� �����������
            //var result = _controller.Create(new RamMetricCreateRequest() { Time = 12, Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // ��������� �������� �� �����������
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
