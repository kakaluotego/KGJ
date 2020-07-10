using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RSAExtensions;

namespace KGJ.Web.Host.Startup
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _privateKey = @"MIIEowIBAAKCAQEAsdCMk6u8mW4MeVq+N4BX28twnQGcxn6+ifNi8+Q4+tUAnCE3
n5ZxRlSE2cSTOyHKzIEPSm5QxRiEFUL/Fw9u2QzKhX58C4ayOMDvEuDIis92AsVS
a7LNRF6tTOBSO8rZspkPRwXPUMXcHw04XqjraPQ+FkZqi/fabDKA1BvU8wJNwKK0
eq1Gd7fTuApwn9pv13fFYeW62guArNtp+0sW3WXdceJfcU4BHbWrN9kdiUEKlHl3
hR7KlyJeaFqLeiDpoMlxgmZqtoIHETNnXvJyJVnl/rNHZSDPBwCPuOwip2jZmm1M
cNAbOeLkPuMGKlTH8/pjdo33omqZBmgHkm3a9wIDAQABAoIBAC/lYghg6RNW/kst
8zDWBFRzDylrhsQ83awhABV78xE1kM19cRMuneJ437Qc7B+wcYT2epzs7Bq7CNEF
U9rtUEls1Zxxvo4IQTdiWMN4AuzJBxul9RaHeh0hd4LOpbfvYwZljiTuXrJ+KVW/
5OAylf6WU6cCXq7HG3GrhOIdgRXXpQ3q/DJkBHP5FMjpxQTID4lkqxn+KaN0LsuU
x49Saw/XI1PRegqdjBEZn/EI1ZzMofPoiUKRrfAJW3invA6TnyzSthP3BWuOkbLC
KEEiGnVRIfeg4p6N3wVDEVDgvV5wKjJjvzoMgdG6UzsAn1NcEFcM0CNm/f9uA6Dg
r/CEZSECgYEA2XGZ3TJeQt6ci8lzjfipU3wm5Y+v7FEc58kzDQxPc5wrMMO/h/iz
zffy1PCboa+mwkA2angJTYpfF+x/acbWi7jJDzirdky1u4DmE05QsFavbZ3x9Igs
IXyK/pbYmsbVnxTxe4mlEi1f6e6r3njAlCvAXEk9USGDhzZ3aZ/XjHECgYEA0VgR
jgsBqe8FmrVQVc3D84NaNM53AxwC/2Qtu+APmeIFp1Cwf0UZFJyi/ZGM8bYZRzvR
vwBAMFqiEsjmpnWtVKIXDTmpLzlgcRM2NlxxJystwDG6X60i4wHLC+umeUku6Rmx
VUrxuejDPF09fNHEaRqB8I0VOxUrLwGEVbYUsecCgYAFbZSNtQgtWylFRA4iaSaw
8tR1vpSBrbSvBJLFrJz/IdLiMMDDQC4c4iepsSfxbVUYXEn/dmelC+M70aeMmG4a
ps4+FwgQt/GBcMJNkRLqk/9lGSBFHnEblORTXqihlH0Yr4knsFPylDdku5SGxTTp
ff4bT6i8Mb36zh1FAbx7wQKBgQDIOW2A5/INcHlxwR03E/jo/abXfunBZZdQYaaE
XQdO7SjvIHPPJm2Yk0ApOn8N6FdBFGvGPR0nomgFg3VnnIHNwsI9efrhGgWt4Own
Dj+DcP7vJiDVxajqahqkKh0tP9vIQzSjGW0dnQyuvZdDvPYYeKvzQV0fB14oNVE3
EVG13QKBgDOdxxCXZuuJbOOSZKbCebOkrAV3zaHo3OBBqp++Xi5xL0aeOs1S542T
X5mPRiGwKoEAVS6MGVdL98mjXBIhr3TloHOrbIUwFERXsVuVaSNkLxl2Bxc5Xd44
UUATcYj14CquotcKnjtMaySBZy75cshlbO3gpc4PlK23hDsjK4tq";

        /// <summary>
        /// 构造 Http 请求中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="cacheService"></param>
        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("aeskey"))
            {
                if (context.Request.Method == "POST" || context.Request.Method == "PUT")
                {
                    //创建http的原始请求和响应流
                    var reqOrigin = context.Request.Body;
                    var resOrigin = context.Response.Body;

                    try
                    {
                        var req = context.Request;
                        req.EnableBuffering();
                        var data = string.Empty;
                        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                        {
                            data = await reader.ReadToEndAsync();
                        }

                        // 这里读取过body  Position是读取过几次  而此操作优于控制器先行 控制器只会读取Position为零次的
                        req.Body.Position = 0;

                        var aeskey = context.Request.Headers["aeskey"].ToString();
                        var rsa = CreateRsaFromPrivateKey(_privateKey);
                        aeskey = rsa.DecryptBigData(aeskey, RSAEncryptionPadding.Pkcs1);
                        var dataJson = AesDecrypt(data, aeskey);

                        //解密完之后把解密后内容写入新的request流去
                        var newReq = new MemoryStream();
                        context.Request.Body = newReq;
                        context.Request.ContentType = "application/json, text/plain";
                        using (var streamWriter = new StreamWriter(newReq))
                        {
                            streamWriter.Write(dataJson);
                            streamWriter.Flush();
                            //此处一定要设置=0，否则controller的action里模型绑定不了数据
                            newReq.Position = 0;
                            //进入action
                            await _next(context);
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                else
                {
                    if (context.Request.QueryString.Value.Length > 1)
                    {
                        var request = context.Request.QueryString.Value.Substring(1, context.Request.QueryString.Value.Length - 1);
                        var aeskey = context.Request.Headers["aeskey"].ToString();
                        var rsa = CreateRsaFromPrivateKey(_privateKey);
                        aeskey = rsa.DecryptBigData(aeskey, RSAEncryptionPadding.Pkcs1);
                        request = AesDecrypt(request, aeskey);

                        context.Request.QueryString = new QueryString("?" + request);
                    }

                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            string result;
            try
            {
                byte[] array = Convert.FromBase64String(str);

                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
                byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                result = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                result = null;
                Console.WriteLine(ex);
            }
            return result;
        }

        private static RSA CreateRsaFromPrivateKey(string privateKey)
        {
            var privateKeyBits = System.Convert.FromBase64String(privateKey);
            var rsa = RSA.Create();
            var RSAparams = new RSAParameters();

            using (var binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(RSAparams);
            return rsa;
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }
    }
}
