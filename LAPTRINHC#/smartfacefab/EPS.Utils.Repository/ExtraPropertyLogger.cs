using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EPS.Utils.Repository
{
    public class ExtraPropertyLogger : Microsoft.Extensions.Logging.ILogger, IReadOnlyList<KeyValuePair<string, object>>
    {
        readonly string _format;
        readonly object[] _parameters;
        IReadOnlyList<KeyValuePair<string, object>> _logValues;
        List<KeyValuePair<string, object>> _extraProperties;

        public ExtraPropertyLogger(string format, params object[] values)
        {
            _format = format;
            _parameters = values;
        }

        public ExtraPropertyLogger AddProp(string name, object value)
        {
            var properties = _extraProperties ?? (_extraProperties = new List<KeyValuePair<string, object>>());
            properties.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if (MessagePropertyCount == 0)
            {
                if (ExtraPropertyCount > 0)
                    return _extraProperties.GetEnumerator();
                else
                    return System.Linq.Enumerable.Empty<KeyValuePair<string, object>>().GetEnumerator();
            }
            else
            {
                if (ExtraPropertyCount > 0)
                    return System.Linq.Enumerable.Concat(_extraProperties, LogValues).GetEnumerator();
                else
                    return LogValues.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                int extraCount = ExtraPropertyCount;
                if (index < extraCount)
                {
                    return _extraProperties[index];
                }
                else
                {
                    return LogValues[index - extraCount];
                }
            }
        }

        public int Count => MessagePropertyCount + ExtraPropertyCount;

        private IReadOnlyList<KeyValuePair<string, object>> LogValues
        {
            get
            {
                if (_logValues == null)
                    Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(this, _format, _parameters);
                return _logValues;
            }
        }

        private int ExtraPropertyCount => _extraProperties?.Count ?? 0;

        private int MessagePropertyCount
        {
            get
            {
                if (LogValues.Count > 1 && !string.IsNullOrEmpty(LogValues[0].Key) && !char.IsDigit(LogValues[0].Key[0]))
                    return LogValues.Count;
                else
                    return 0;
            }
        }

        void Microsoft.Extensions.Logging.ILogger.Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _logValues = state as IReadOnlyList<KeyValuePair<string, object>> ?? Array.Empty<KeyValuePair<string, object>>();
        }

        bool Microsoft.Extensions.Logging.ILogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        IDisposable Microsoft.Extensions.Logging.ILogger.BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public static Func<ExtraPropertyLogger, Exception, string> Formatter { get; } = (l, e) => l.LogValues.ToString();
    }
}
