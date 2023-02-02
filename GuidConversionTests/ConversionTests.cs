namespace GuidConversionTests;

public class ConversionTests
{
    [Fact]
    public void GuidToAndFromConversion_SameResult()
    {
        var guid = Guid.NewGuid();
        var urlParameter = guid.ToUrlParameterString();
        Assert.NotNull(urlParameter);
        
        var convertedBack = urlParameter.FromUrlParameterString();
        Assert.Equal(guid, convertedBack);
    }
}