//166. Mocking using Moq
using Moq;
using NUnit.Framework;

public interface IService
{
    int GetData();
}

public class MyClass
{
    private readonly IService _service;

    public MyClass(IService service)
    {
        _service = service;
    }

    public int FetchData()
    {
        return _service.GetData();
    }
}

[TestFixture]
public class TestClass
{
    [Test]
    public void Mock_Test()
    {
        var mockService = new Mock<IService>();
        mockService.Setup(s => s.GetData()).Returns(10);

        var obj = new MyClass(mockService.Object);
        int result = obj.FetchData();

        Assert.AreEqual(10, result);
    }
}