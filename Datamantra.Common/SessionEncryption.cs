using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using Datamantra.Model.Entity;

namespace Datamantra.Common
{
    public class SessionEncryption
    {
        /// <summary>
        /// This security key should be very complex and Random for encrypting the text. This playing vital role in encrypting the text.
        /// </summary>
        public static string _securityKey = Utility.GetAppSettings(Constants.SessionEncryptionKey).ToString();

        /// <summary>
        /// This method is used to convert the plain text to Encrypted/Un-Readable Text format.
        /// </summary>
        /// <param name="PlainText">Plain Text to Encrypt before transferring over the network.</param>
        /// <returns>Cipher Text</returns>
        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            //Getting the bytes of Input String.
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));

            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);

            //Releasing the Memory Occupied by TripleDES Service Provider for Encryption.
            objTripleDESCryptoService.Clear();

            //Convert and return the encrypted data/byte into string format.
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// This method is used to convert the Cipher/Encypted text to Plain Text.
        /// </summary>
        /// <param name="CipherText">Encrypted Text</param>
        /// <returns>Plain/Decrypted Text</returns>
        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));

            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Releasing the Memory Occupied by TripleDES Service Provider for Decryption.          
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #region Get Value From Session
        public static string GetValueFromSession(string sessionProp)
        {
            string sessionValue = string.Empty;
            //IDictionary<string, string> sessionObjDict = new Dictionary<string, string>();
            //if (HttpContext.Current.Session != null && HttpContext.Current.Session[Constants.SessionObjects] != null)
            //{
            //    sessionObjDict = DecryptSessionObject((string)HttpContext.Current.Session[Constants.SessionObjects]);
            //}

            //if (sessionObjDict != null && sessionObjDict.ContainsKey(sessionProp))
            //{
            //    sessionObjDict.TryGetValue(sessionProp, out sessionValue);
            //}
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[sessionProp] != null)
            {
                sessionValue =  HttpContext.Current.Session[sessionProp].ToString();
            }
            return sessionValue;
        }
        #endregion

        #region Set Session Object
        public static void SetSessionObject(SessionObjects sObject)
        {
            IDictionary<string, string> sessionDict = new Dictionary<string, string>();
            bool rememberPwd = false;

            // We called this inspectionReportfields because the entity properties correspond to form/report fields I'm generating from this data.
            var sessionProperties = sObject.GetType().GetProperties();

            foreach (var prop in sessionProperties)
            {
                var sessionValue = (prop.GetValue(sObject) != null ? prop.GetValue(sObject).ToString() : null);
                if (!string.IsNullOrEmpty(sessionValue))
                {
                    sessionDict.Add(prop.Name, (prop.GetValue(sObject) != null ? prop.GetValue(sObject).ToString() : null));
                }
            }

            if (HttpContext.Current.Session != null)
            {
                // assign the updated sessionObject to current session by Encrypting SessionObject to String
                string encryptedSessionDict = EncryptSessionObject(sessionDict);

                HttpContext.Current.Session[Constants.SessionObjects] = encryptedSessionDict;

                //Add Session Items to cookies Container
                if (sessionDict != null && sessionDict.Any() && sessionDict.ContainsKey(SessionItemKey.EmailAddress))
                {
                    CookieSessionItems _cookieSessionItems = new CookieSessionItems
                    {
                        EmailAddress = sessionDict[SessionItemKey.EmailAddress] != null ? sessionDict[SessionItemKey.EmailAddress] : null,
                        //EncryptedSession = encryptedSessionDict,
                        SessionObject = ObjectToString(sessionDict)
                    };

                    //Add a cookie for remember, if remember me checked, else delete the cookie
                    if (rememberPwd)
                    {
                        _cookieSessionItems.RememberMe = rememberPwd;
                        AppCookies.SetValue(SessionItemKey.EmailAddress, _cookieSessionItems.EmailAddress, DateTime.MaxValue);
                        AppCookies.SetValue(SessionItemKey.CookieSession, HttpUtility.UrlEncode(Utility.Serialize(_cookieSessionItems)), DateTime.MaxValue);
                    }
                    else
                    {
                        _cookieSessionItems.RememberMe = rememberPwd;
                        AppCookies.SetValue(SessionItemKey.EmailAddress, _cookieSessionItems.EmailAddress);
                        AppCookies.SetValue(SessionItemKey.CookieSession, HttpUtility.UrlEncode(Utility.Serialize(_cookieSessionItems)), DateTime.MaxValue);
                    }
                }
            }
        }
        #endregion

        #region Encrypt Session Object
        public static string EncryptSessionObject(object sObject)
        {
            // convert the class object to base64 string
            string sessionString = ObjectToString(sObject);

            //return the encrpted plain text
            return EncryptPlainTextToCipherText(sessionString);
        }
        #endregion

        #region Decrypt Session Object
        public static IDictionary<string, string> DecryptSessionObject(string sessionString)
        {
            IDictionary<string, string> localObject = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(sessionString))
            {
                // get the decrypted text from session string
                var decrytText = SessionEncryption.DecryptCipherTextToPlainText(sessionString);
                // change the string
                localObject = (Dictionary<string, string>)StringToObject(decrytText);
            }
            return localObject;
        }
        #endregion

        #region Object To Base64 String
        public static string ObjectToString(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        #endregion

        #region Base64 String To Object
        public static object StringToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Set Session Value
        public static void SetSessionValue(string sessionProp, object sessionValue)
        {
            string sessionValueStr = string.Empty;
            bool rememberPwd = false;

            if (sessionValue != null)
            {
                sessionValueStr = sessionValue.ToString();
            }

            IDictionary<string, string> sessionObjDict = new Dictionary<string, string>();

            if (HttpContext.Current.Session != null && HttpContext.Current.Session[Constants.SessionObjects] != null)
            {
                sessionObjDict = DecryptSessionObject((string)HttpContext.Current.Session[Constants.SessionObjects]);
            }

            if (sessionObjDict != null && !sessionObjDict.ContainsKey(sessionProp))
            {
                sessionObjDict.Add(sessionProp, sessionValueStr);
            }
            else
            {
                sessionObjDict[sessionProp] = sessionValueStr;
            }

            string encryptedSessionDict = EncryptSessionObject(sessionObjDict);

            HttpContext.Current.Session[Constants.SessionObjects] = encryptedSessionDict;

            //Add Session Items to cookies Container
            if (sessionObjDict != null && sessionObjDict.Any() && sessionObjDict.ContainsKey(SessionItemKey.EmailAddress))
            {
                CookieSessionItems _cookieSessionItems = new CookieSessionItems
                {
                    EmailAddress = sessionObjDict[SessionItemKey.EmailAddress] != null ? sessionObjDict[SessionItemKey.EmailAddress] : null,
                    //EncryptedSession = encryptedSessionDict,
                    SessionObject = ObjectToString(sessionObjDict)
                };

                //Add a cookie for remember, if remember me checked, else delete the cookie
                if (rememberPwd)
                {
                    _cookieSessionItems.RememberMe = rememberPwd;
                    AppCookies.SetValue(SessionItemKey.EmailAddress, _cookieSessionItems.EmailAddress, DateTime.MaxValue);
                    AppCookies.SetValue(SessionItemKey.CookieSession, HttpUtility.UrlEncode(Utility.Serialize(_cookieSessionItems)), DateTime.MaxValue);
                }
                else
                {
                    _cookieSessionItems.RememberMe = rememberPwd;
                    AppCookies.SetValue(SessionItemKey.EmailAddress, _cookieSessionItems.EmailAddress);
                    AppCookies.SetValue(SessionItemKey.CookieSession, HttpUtility.UrlEncode(Utility.Serialize(_cookieSessionItems)), DateTime.MaxValue);
                }
            }
        }
        #endregion
    }
}