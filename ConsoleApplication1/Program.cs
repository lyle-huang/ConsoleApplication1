using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;





namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
        //    string PubStr = "<RSAKeyValue><Modulus>tjPgeyf1BVYgugvQH/MIiW/O5SzzBgQGvePviM0rA4tIDCyDO/F5aSxpfTaMMSdcG98wrNfmQUKNQtoELrTnU9tWO0FxLByxqSPciN0mCyPaLGRM6Gr217RVFEi2huoCuxBFWjYxkTbWRHve8cLacwdp4taDXAiD9lr9jYzBz98=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
         //   string PrivStr = "<RSAKeyValue><Modulus>tjPgeyf1BVYgugvQH/MIiW/O5SzzBgQGvePviM0rA4tIDCyDO/F5aSxpfTaMMSdcG98wrNfmQUKNQtoELrTnU9tWO0FxLByxqSPciN0mCyPaLGRM6Gr217RVFEi2huoCuxBFWjYxkTbWRHve8cLacwdp4taDXAiD9lr9jYzBz98=</Modulus><Exponent>AQAB</Exponent><P>3gIk0qgIh867sHrzbd/AVMGh5DsgApbr8dxqD898FqbtFMG0aQQksvS2nll5pm1SnsEf/ac2LKIDhK1PF5Y/tw==</P><Q>0hmBOUgVhJhvNPkuiDGMi539g/fYiOltjlK6L1sfxJgzmnfvlhH28vmRXVQ/QoX0sj+ONj0FtIq6JDz/mBkhGQ==</Q><DP>dgZbvyYXyuNMAsmKipoObMC4KVaJcPb9HoqYVClxBbXeik9kcwQB4qWYRFy7AJBUARQYxsfTVC0Zg7qDNr3oxw==</DP><DQ>rWmJxIrf3+Kln3aw1o73L2IHFv7iBheNarSTonS3IuBxb2ThJt/LEb+2IO9hi3nff6R/r/rsE5mRjEmguZy8OQ==</DQ><InverseQ>L4gG4Ty5B7V/axODZoLJlnUmTW/qj8UUTbw5xfKoo9wzmg8D7a7PjD8ft/EsU8n9FgIcyVJ+YQCToc8LZ3EF8Q==</InverseQ><D>o5CEl+qx4h2EeZ/7MYNKiZ6uvDuS2zaoGrXRphyQ3biecBdRpHFNTeZJuNdMGSA9ZuQA3Vlf4fvttjLjiE5sTu5xw9rpRQfI5/X8YUxW6crH7GKwog8YX4lO+WKIkfaKED/IdMaZxvvT1m/6QBAVn788SL0oXoQSn9n0vv1U/BE=</D></RSAKeyValue>";
            string m = "ZCJgSGT3XQOi3F9vZRGnychO2Tiag88FcpAQogawSoM0Tbky1nF3Wemo2EHmC5Gq1AJ/K89QLjMv7oynXgM+fqtmXoEwScRTWkv+oY+/b8Lp6ZLBoFSx+FUYrV/NKmnTHzlRZNLQD9irfB6bWfrSEi44MuEu4gMNUCI2W/w4vDI=";
            string m1= "gSayPSQQv9UfoQKwCtb+qVoEHZ+BPY7Sre/oIoqMKqdkqV+XdM20fLhnzqsdlwXJt5HUypulgNOdb+fMZBTMwQcjCqpeF2wbpIfOL6UTlbEdMKtu2J9Vy4UTMirnjJ9/WmdCxWdzobf5GwG14KuQNl0/TZ4dQqpSGZnBzZtit7M=";
            Class1 tmp = new Class1();
            tmp.CheckLicense(m1);
         // Console.WriteLine("证书错误信息：");
            Console.WriteLine(tmp.LicenseError);
            Console.WriteLine();
            Console.WriteLine("本机序号原始序号：");
            Console.WriteLine(tmp.Getprint(false));
        /*    Console.WriteLine();
            Console.WriteLine("测试机序号：");
            Console.WriteLine(tmp.Getprint(true));
            Console.WriteLine();
            Console.WriteLine(tmp.RSAKey()); */
            Console.WriteLine();
            Console.WriteLine(tmp.License("//Vpq2MzgVS6+4ws7aoEUg==" + "|2050-12-31"));
            Console.WriteLine();
            tmp.CheckLicense(m);
            Console.WriteLine(tmp.LicenseError);
            Console.WriteLine("End");

        }
    }
    public class Class1
    {
        private bool _isRight = false;

        private string _licenseError = "";

        public string xmlPublicKey = "";
        public string xmlPrivateKey = "";

        public bool IsRight
        {
            get
            {
                return this._isRight;
            }
        }



        public string LicenseError
        {
            get
            {
                return this._licenseError;
            }
        }

        public string GetFingerprint()
        {
            string result = "";
            try
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        object obj = managementObject["SerialNumber"];
                        string text;
                        if (obj == null)
                        {
                            text = Class1.GetDiskID();
                            if (text == "")
                            {
                                text = "7E0220010372";
                            }
                        }
                        else
                        {
                            text = obj.ToString().Trim();
                        }
                        byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
                        System.Security.Cryptography.MD5 mD = new System.Security.Cryptography.MD5CryptoServiceProvider();
                        byte[] inArray = mD.ComputeHash(bytes);
                        result = System.Convert.ToBase64String(inArray);
                    }
                }
                managementObjectSearcher.Dispose();
            }
            catch (System.Exception ex)
            {
            }
            return result;
        }






        public string Getprint(bool T)
        {
            string result = "";
            string text = ""; 
            try
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        object obj = managementObject["SerialNumber"];
                        
                        if (obj == null)
                        {
                            text = Class1.GetDiskID();
                            if (text == "")
                            {
                                text = "7E0220010372";
                            }
                        }
                        else
                        {
                            text = obj.ToString().Trim();
                        }
                        if (T == true)
                        {
                            text = "WD-WCC2E2FAAR5K";
                        }
                        byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
                        System.Security.Cryptography.MD5 mD = new System.Security.Cryptography.MD5CryptoServiceProvider();
                        byte[] inArray = mD.ComputeHash(bytes);
                        result = System.Convert.ToBase64String(inArray);
                    }
                }
                managementObjectSearcher.Dispose();
            }
            catch (System.Exception ex)
            {
            }
            return result + "\r\n"  + text;
        }

        //		public bool CheckLicenseFile()
        //		{
        //			System.IO.StreamReader streamReader = new System.IO.StreamReader(DsAppliction.GetRootPath() + "Class1.oprh", System.Text.Encoding.Default);
        //			return this.CheckLicense(streamReader.ReadLine());
        //	}

        public string RSAKey()
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
                this.xmlPrivateKey = rsa.ToXmlString(true);
                this.xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "私钥是：" + "\r\n" + this.xmlPrivateKey + "\r\n" + "公钥是：" + "\r\n" + this.xmlPublicKey;
        }


        public string License(string encrypt)
        {
            string rt="";
            System.DateTime now = System.DateTime.Now;
            using (System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider())
            {
                try
                {
                    rSACryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>tjPgeyf1BVYgugvQH/MIiW/O5SzzBgQGvePviM0rA4tIDCyDO/F5aSxpfTaMMSdcG98wrNfmQUKNQtoELrTnU9tWO0FxLByxqSPciN0mCyPaLGRM6Gr217RVFEi2huoCuxBFWjYxkTbWRHve8cLacwdp4taDXAiD9lr9jYzBz98=</Modulus><Exponent>AQAB</Exponent><P>3gIk0qgIh867sHrzbd/AVMGh5DsgApbr8dxqD898FqbtFMG0aQQksvS2nll5pm1SnsEf/ac2LKIDhK1PF5Y/tw==</P><Q>0hmBOUgVhJhvNPkuiDGMi539g/fYiOltjlK6L1sfxJgzmnfvlhH28vmRXVQ/QoX0sj+ONj0FtIq6JDz/mBkhGQ==</Q><DP>dgZbvyYXyuNMAsmKipoObMC4KVaJcPb9HoqYVClxBbXeik9kcwQB4qWYRFy7AJBUARQYxsfTVC0Zg7qDNr3oxw==</DP><DQ>rWmJxIrf3+Kln3aw1o73L2IHFv7iBheNarSTonS3IuBxb2ThJt/LEb+2IO9hi3nff6R/r/rsE5mRjEmguZy8OQ==</DQ><InverseQ>L4gG4Ty5B7V/axODZoLJlnUmTW/qj8UUTbw5xfKoo9wzmg8D7a7PjD8ft/EsU8n9FgIcyVJ+YQCToc8LZ3EF8Q==</InverseQ><D>o5CEl+qx4h2EeZ/7MYNKiZ6uvDuS2zaoGrXRphyQ3biecBdRpHFNTeZJuNdMGSA9ZuQA3Vlf4fvttjLjiE5sTu5xw9rpRQfI5/X8YUxW6crH7GKwog8YX4lO+WKIkfaKED/IdMaZxvvT1m/6QBAVn788SL0oXoQSn9n0vv1U/BE=</D></RSAKeyValue>");
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(encrypt);
                    BigInteger bigInteger = new BigInteger(bytes);
                    System.Security.Cryptography.RSAParameters rSAParameters = rSACryptoServiceProvider.ExportParameters(true);
                    BigInteger n = new BigInteger(rSAParameters.Modulus);
                    BigInteger d = new BigInteger(rSAParameters.D);
                    //BigInteger exp = new BigInteger(rSAParameters.Exponent);
                    BigInteger bigInteger2 = bigInteger.modPow(d, n);
                 //   string @string = System.Text.Encoding.Default.GetString(bigInteger2.getBytes());
                 //   string sss = bigInteger2.ToHexString();
                    bytes = bigInteger2.getBytes();
                    rt = Convert.ToBase64String(bytes);
                 
                }
                catch (System.Exception ex)
                {
                    this._isRight = false;
                    this._licenseError = "许可验证失败，原因：" + ex.Message;
                }
            }
            return rt;
        }


        public bool CheckLicense(string encrypt)
        {
            System.DateTime now = System.DateTime.Now;
            using (System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider())
            {
                try
                {

                  //  rSACryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>tjPgeyf1BVYgugvQH/MIiW/O5SzzBgQGvePviM0rA4tIDCyDO/F5aSxpfTaMMSdcG98wrNfmQUKNQtoELrTnU9tWO0FxLByxqSPciN0mCyPaLGRM6Gr217RVFEi2huoCuxBFWjYxkTbWRHve8cLacwdp4taDXAiD9lr9jYzBz98=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

                    rSACryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>1Fb8uQoFq4vdVPNTDfQQkACSHyG5jEz4ivNv5BNgtWwbJzDeF91A2LTRM5AdH9X6xGRdrmW2QNpE5+RI8Oub5rMYNZm8byzo4EbK0mOpoLQcpQ3d1R94VTht4g0BDt29m2v47vKaf/k1Jg12Ccn5D5bb8AO7UsxQUhKHTh5oCNc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
                    BigInteger bigInteger = new BigInteger(System.Convert.FromBase64String(encrypt));
                    byte[] bytes1 = System.Convert.FromBase64String(encrypt);

                    System.Security.Cryptography.RSAParameters rSAParameters = rSACryptoServiceProvider.ExportParameters(false);
                    BigInteger n = new BigInteger(rSAParameters.Modulus);
                    BigInteger exp = new BigInteger(rSAParameters.Exponent);
                    BigInteger bigInteger2 = bigInteger.modPow(exp, n);
                    string @string = System.Text.Encoding.Default.GetString(bigInteger2.getBytes());
                    string[] array = @string.Split(new char[]
					{
						'|'
					});
                    if (array.Length == 2)
                    {
                        
                            this._isRight = false;
                            this._licenseError = "认证ID：" + "\r\n" + array[0] + "\r\n"+"认证时间： " + array[1];
                        
                    }
                    else
                    {
                        this._isRight = false;
                        this._licenseError = "认证ID：" + "\r\n" + array[0];
                    }
                }
                catch (System.Exception ex)
                {
                    this._isRight = false;
                    this._licenseError = "许可验证失败，原因：" + ex.Message;
                }
            }
            return this._isRight;
        }

        private static System.DateTime GetSystemTime()
        {
            return System.DateTime.Now;
        }

        [System.Runtime.InteropServices.DllImport("DiskID32.dll")]
        private static extern long DiskID32(ref byte DiskModel, ref byte DiskID);

        private static string GetDiskID()
        {
            byte[] array = new byte[31];
            byte[] array2 = new byte[31];
            string text = "";
            if (Class1.DiskID32(ref array[0], ref array2[0]) != 1L)
            {
                for (int i = 0; i < 31; i++)
                {
                    if (System.Convert.ToChar(array2[i]) != System.Convert.ToChar(0))
                    {
                        text += System.Convert.ToChar(array2[i]);
                    }
                }
                text = text.Trim();
            }
            return text;
        }
    }
}
