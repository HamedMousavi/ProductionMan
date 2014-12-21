using System;
using System.Net.Http;
using System.Reflection;
using log4net;
using System.Web.Http.Tracing;


namespace ProductionMan.Web.Api.Services
{

    public class Log4NetTraceWriter : ITraceWriter
    {


        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            LogTrace(rec);
        }


        private ILog _logger;


        private ILog Log
        {
            get
            {
                return _logger ??
                       (_logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
            }
        }


        private void LogTrace(TraceRecord rec)
        {
            const string format = "Kind:{0};Status={1};Operation={2};Message={3};{4}Request={5}";
            var args = new object[]
            {
                rec.Kind,
                rec.Status,
                rec.Operation,
                rec.Message,
                Environment.NewLine,
                rec.Request
            };

            switch (rec.Level)
            {
                case TraceLevel.Error:
                    Log.ErrorFormat(format, args);
                    break;
                case TraceLevel.Info:
                    Log.InfoFormat(format, args);
                    break;
                case TraceLevel.Debug:
                    Log.DebugFormat(format, args);
                    break;
                case TraceLevel.Warn:
                    Log.WarnFormat(format, args);
                    break;
                case TraceLevel.Fatal:
                    Log.FatalFormat(format, args);
                    break;
            }
        }
    }
}