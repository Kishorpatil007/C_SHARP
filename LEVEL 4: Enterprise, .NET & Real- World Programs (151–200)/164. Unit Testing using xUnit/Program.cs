using Xunit;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        Calculator calc = new Calculator();

        int result = calc.Add(2, 3);

        Assert.Equal(5, result);
    }
}