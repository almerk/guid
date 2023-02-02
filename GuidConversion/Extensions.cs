namespace GuidConversion;
public static class Extensions
{
    static string ToUrlString(this Guid guid)
    {
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace('+','_')
            .Replace('/','-')
            .TrimEnd('=');
    }

    static Guid FromUrlString(string base64)
    {
        var mapped = base64.Replace('_', '+').Replace('-','/') + "==";

        return new Guid(Convert.FromBase64String(mapped));
    }
}
