using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace app3
{
    public class RMCrypt
    {
        private Byte[] _Key = {0x01, 0x09, 0x08, 0x05, 0x00, 0x06, 0x00, 0x05, 0x01, 0x09, 0x07, 0x09, 0x00, 0x08, 0x00, 0x08};
        private Byte[] _IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private RijndaelManaged _RM;

        public RMCrypt()
        {
            this._RM = new RijndaelManaged();
            this._RM.Key = this._Key;
            this._RM.IV = this._IV;
        }

        public RMCrypt(string strKey):this()
        {
            string myKey = strKey.PadLeft(16, 'x').Substring(0, 16);//必须为16位长，不足以‘x’填充
            this._RM.Key = ASCIIEncoding.ASCII.GetBytes(myKey);
            this._RM.IV = ASCIIEncoding.ASCII.GetBytes(myKey);
        }

        public string Encrypt(string strContext)
        {
            byte[] inputByteArray = Encoding.Default.GetBytes(strContext);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, this._RM.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder strResult = new StringBuilder();
            foreach(byte b in ms.ToArray())
            {
                strResult.AppendFormat("{0:X2}", b);//格式化为16进制数字
            }
            return strResult.ToString();
        }

        public string Decrypt(string strContext)
        {
            //将加密后的字符串转为 ByteArray
            byte[] inputByteArray = new byte[strContext.Length / 2];
            for (int x = 0; x < strContext.Length / 2; x++)
            {
                int i = (Convert.ToInt32(strContext.Substring(x * 2, 2), 16));//从数字串转为16进制数字后放入byteArray
                inputByteArray[x] = (byte)i;
            }
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, this._RM.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
