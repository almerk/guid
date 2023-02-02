using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace GuidConversion;
public static class Extensions
{
    public static string ToUrlParameterString(this Guid guid)
    {
        Span<byte> guidBytes = stackalloc byte [16];
        Span<byte> base64Bytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(guidBytes, ref guid);
        Base64.EncodeToUtf8(guidBytes, base64Bytes, out _, out _);

        Span<char> base64Chars = stackalloc char[22];
         for (var i = 0; i < 22; i++)
        {
            base64Chars[i] = base64Bytes[i] switch
            {
                (byte)'+' => '_',
                (byte)'/' => '-',
                _ => (char)base64Bytes[i]
            };
        }

        return new string(base64Chars);
    }

    public static Guid FromUrlParameterString(this string str) 
        => SpanFromUrlString(str ?? throw new ArgumentNullException(nameof(str)));

    private static Guid SpanFromUrlString(ReadOnlySpan<char> str)
    {
        Span<char> base64Chars = stackalloc char[24];
        
        for (var i = 0; i < 22; i++)
        {
            base64Chars[i] = str[i] switch
            {
                '_' => '+',
                '-' => '/',
                _ => str[i]
            };
        }
        
        base64Chars[22] = '=';
        base64Chars[23] = '=';

        Span<byte> guidBytes = stackalloc byte [16];

        if (!Convert.TryFromBase64Chars(base64Chars, guidBytes, out _))
            throw new ArgumentException("Given argument is in wrong format");

        return new Guid(guidBytes);
    }
}
