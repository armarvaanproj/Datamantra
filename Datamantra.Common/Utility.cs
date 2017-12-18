using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web;
using Datamantra.Model.Entity;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using ImageResizer;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace Datamantra.Common
{
    public static class Utility
    {
        #region Convert to bool
        public static bool GetBool(object value)
        {
            bool result = false;
            if (value != null)
            {
                bool.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Get Nullable bool
        public static bool? GetNullablebool(object value)
        {
            bool? nullableResult = null;

            bool result;

            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                nullableResult = bool.TryParse(value.ToString(), out result) ? result : (bool?)null;
            }

            return nullableResult;
        }
        #endregion

        #region Convert to Long
        public static long GetLong(this object value)
        {
            long result = 0;
            if (value != null)
            {
                long.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Convert to Int
        public static int GetInt(object value)
        {
            int result = 0;
            if (value != null)
            {
                int.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Get Role type by role Id
        public static string GetRoleTypebyRoleId(long id)
        {
            return ((RoleType)Enum.Parse(typeof(RoleType), id.ToString())).ToString().ToLower();
        }
        #endregion

        #region Convert to Byte
        public static byte GetByte(object value)
        {
            byte result = 0;
            if (value != null)
            {
                byte.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Convert To Guid
        public static Guid ConvertToGuid(object p)
        {
            Guid _retValue = Guid.Empty;
            if (p != null)
            {
                Guid.TryParse(p.ToString(), out _retValue);
            }
            return _retValue;
        }
        #endregion

        #region Convert to Short
        public static short GetShort(object value)
        {
            short result = 0;
            if (value != null)
            {
                short.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Convert to DateTime
        /// <summary>
        /// Convert to double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetDateTime(object value)
        {
            DateTime result = new DateTime();
            if (value != null)
            {
                DateTime.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Get App Settings
        /// <summary>
        /// Get the appsetting value from the web.config
        /// </summary>
        /// <param name="appKey">appsettings key</param>
        /// <returns></returns>
        public static string GetAppSettings(string appKey)
        {
            object value = null;
            value = ConfigurationManager.AppSettings[appKey];
            if (value != null)
            {
                return value.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region Create Salt
        /// <summary>
        /// Creates the salt.
        /// </summary>
        /// <param name="saltSize">Size of the salt.</param>
        /// <returns></returns>
        public static string CreateSalt(int saltSize)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltSize];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        #endregion

        #region EncryptPassword
        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="pPassword">The password.</param>
        /// <param name="pSalt">The salt.</param>
        /// <returns></returns>
        public static string EncryptPassword(string pPassword, string pSalt)
        {
            string saltAndPwd = String.Concat(pPassword, pSalt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            return hashedPwd;
        }
        #endregion

        #region Serialize
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="seralizedObject"></param>
        /// <returns></returns>
        public static string Serialize(object seralizedObject)
        {
            string serializedData = string.Empty;
            if (seralizedObject != null)
            {
                StringWriter strWriter = new StringWriter();
                XmlSerializer serializer = new XmlSerializer((seralizedObject).GetType());
                serializer.Serialize(strWriter, seralizedObject);
                serializedData = strWriter.ToString();
                //serializedData = serializedData.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty);
                //serializedData = serializedData.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty);
                //serializedData = serializedData.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
                StringReader str = new StringReader(serializedData);
                XmlDocument doc = new XmlDocument();
                doc.Load(str);
                StringWriter sw = new StringWriter();
                XmlTextWriter xw = new XmlTextWriter(sw);
                doc.WriteTo(xw);
                serializedData = sw.ToString();
            }
            return serializedData;
        }
        #endregion

        #region Get UserId from session
        public static long GetUserIdFromSession()
        {
            return GetLong(SessionEncryption.GetValueFromSession(SessionItemKey.UserId));
        }
        #endregion

        #region Get User Name from session
        public static string GetUserNameFromSession()
        {
            return SessionEncryption.GetValueFromSession(SessionItemKey.UserName);
        }
        #endregion

        #region Get EmailAddress from session
        public static string GetEamilAddressFromSession()
        {
            return SessionEncryption.GetValueFromSession(SessionItemKey.EmailAddress);
        }
        #endregion

        #region Get Role Type from session
        public static int GetRoleIDFromSession()
        {
            return GetInt(SessionEncryption.GetValueFromSession(SessionItemKey.RoleID));
        }
        #endregion

        #region Get FormattedImage
        public static string GetFormattedImage(string path, int height, int width)
        {
            var imageArray = Utility.GetBytesFromString(path);

            var settings = new ResizeSettings
            {
                MaxWidth = width,
                MaxHeight = height
            };

            MemoryStream stream = new MemoryStream();
            ImageBuilder.Current.Build(imageArray, stream, settings);
            return Convert.ToBase64String(stream.ToArray());
        }
        #endregion

        #region Get Bytes From String
        public static byte[] GetBytesFromString(string path)
        {
            var webClient = new WebClient();
            return webClient.DownloadData(path);
        }
        #endregion

        #region Remove special Characters from string
        public static string RemoveSpecialCharacters(string StringName)
        {
            StringName = StringName.Replace("#", "");
            StringName = StringName.Replace("@", "");
            StringName = StringName.Replace("$", "");
            StringName = StringName.Replace("/", "");
            StringName = StringName.Replace(",", "");
            StringName = StringName.Replace("(", "");
            StringName = StringName.Replace(")", "");
            StringName = StringName.Replace(".", "");
            StringName = StringName.Replace(" ", "");
            StringName = StringName.Replace("'", "");
            StringName = StringName.Replace("’", "");
            StringName = StringName.Replace("-", "");
            StringName = StringName.Replace("*", "");
            StringName = StringName.Replace("+", "");
            StringName = StringName.Replace("[", "");
            StringName = StringName.Replace("]", "");
            StringName = StringName.Replace("\\", "");
            StringName = StringName.Replace("\"", "");
            StringName = StringName.Replace("!", "");
            StringName = StringName.Replace("|", "");
            StringName = StringName.Replace("^", "");
            StringName = StringName.Replace("&", "");
            StringName = StringName.Replace("_", "");
            StringName = StringName.Replace("=", "");
            StringName = StringName.Replace("?", "");
            StringName = StringName.Replace("~", "");
            StringName = StringName.Replace("`", "");

            return StringName;
        }
        #endregion
    }
}
