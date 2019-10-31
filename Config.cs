using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HG.Config
{
    /// <summary>
    /// This class will read config values in .config file
    /// </summary>
    public abstract class Config
    {
        private const string SEPARATOR = ";";

        private T GetConfigValue<T>()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[methodBase.Name.Substring(4)], typeof(T));
        }

        private IEnumerable<string> GetConfigListValue(string separator = SEPARATOR)
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();

            string configValue = ConfigurationManager.AppSettings[methodBase.Name.Substring(4)];

            if (string.IsNullOrEmpty(separator))
            {
                separator = SEPARATOR;
            }

            string[] splittedValue = configValue.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(splittedValue);
        }
    }
}
