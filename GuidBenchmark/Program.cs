using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

BenchmarkRunner.Run<GuidBenchmark>();

[MemoryDiagnoser(displayGenColumns: false)]
public class GuidBenchmark 
{
    private static readonly Guid Guid = Guid.Parse("1d3e6ce5-9327-4ba5-aa3c-27e2520ad0ef");
    const string GuidStr = "5Ww_HSeTpUuqPCfiUgrQ7w";

    [Benchmark]
    public string ToStringSimpleB() => ToStringSimple(Guid);
    [Benchmark]
    public Guid FromStringSimpleB () => FromStringSimple(GuidStr);


    static string ToStringSimple(Guid guid)
    {
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace('+','_')
            .Replace('/','-')
            .TrimEnd('=');
    }

    static Guid FromStringSimple(string base64)
    {
        var mapped = base64.Replace('_', '+').Replace('-','/') + "==";

        return new Guid(Convert.FromBase64String(mapped));
    }
}



