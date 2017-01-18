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
            Class1 tmp = new Class1();
            tmp.CheckLicense("gSayPSQQv9UfoQKwCtb+qVoEHZ+BPY7Sre/oIoqMKqdkqV+XdM20fLhnzqsdlwXJt5HUypulgNOdb+fMZBTMwQcjCqpeF2wbpIfOL6UTlbEdMKtu2J9Vy4UTMirnjJ9/WmdCxWdzobf5GwG14KuQNl0/TZ4dQqpSGZnBzZtit7M=");
         // Console.WriteLine("证书错误信息：");
            Console.WriteLine(tmp.LicenseError);
            Console.WriteLine();
            Console.WriteLine("本机序号原始序号：");
            Console.WriteLine(tmp.Getprint(false));
            Console.WriteLine();
            Console.WriteLine("测试机序号：");
            Console.WriteLine(tmp.Getprint(true));
            Console.WriteLine();
            Console.WriteLine(tmp.RSAKey());

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


        public bool CheckLicense(string encrypt)
        {
            System.DateTime now = System.DateTime.Now;
            using (System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider())
            {
                try
                {
                    rSACryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>1Fb8uQoFq4vdVPNTDfQQkACSHyG5jEz4ivNv5BNgtWwbJzDeF91A2LTRM5AdH9X6xGRdrmW2QNpE5+RI8Oub5rMYNZm8byzo4EbK0mOpoLQcpQ3d1R94VTht4g0BDt29m2v47vKaf/k1Jg12Ccn5D5bb8AO7UsxQUhKHTh5oCNc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
                    BigInteger bigInteger = new BigInteger(System.Convert.FromBase64String(encrypt));
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
                        if (array[0] == this.GetFingerprint())
                        {
                            System.DateTime now2 = System.DateTime.Now;
                            if (System.DateTime.TryParse(array[1], out now2))
                            {
                                if (Class1.GetSystemTime() > now2)
                                {
                                    this._isRight = false;
                                    this._licenseError = "许可证授权到期！" + "\r\n" + array[0] + "\r\n" + array[1];
                                }
                                else
                                {
                                    this._isRight = true;
                                    this._licenseError = "许可证授权" + "\r\n" + array[0] + "\r\n" + array[1];
                                }
                            }
                            else
                            {
                                this._isRight = false;
                                this._licenseError = "许可证格式错误" + "\r\n" + array[0] + "\r\n" + array[1];
                            }
                        }
                        else
                        {
                            this._isRight = false;
                            this._licenseError = "无效的许可证" + "\r\n" + array[0] + "\r\n" + array[1];
                        }
                    }
                    else
                    {
                        this._isRight = false;
                        this._licenseError = "无效的许可证" + "\r\n" + array[0];
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
