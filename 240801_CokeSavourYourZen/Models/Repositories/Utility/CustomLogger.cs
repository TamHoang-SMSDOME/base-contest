using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models.Repositories.Utility
{
    public class CustomLogger : ILogger
    {
        private static CustomLogger instance;
        private static Logger logger;
        private readonly Repository _repo;
        private CustomLogger()
        {
            _repo = Repository.Instance;
        }
        private CustomLogger(Repository repo)
        {
            _repo = repo;
        }
        public static CustomLogger GetInstance(Repository repo)
        {
            if(instance == null)
            {
                instance = new CustomLogger(repo);
            }
            return instance;
        }
        private Logger GetLogger(string theLogger)
        {
            if(CustomLogger.logger == null)
            {
                CustomLogger.logger = LogManager.GetLogger(theLogger);
            }
            return CustomLogger.logger;
        }
        public void Debug(string message)
        {
            GetLogger("db").WithProperty("Contest", _repo.StartDate.ToString("yyMMdd") + "_" + _repo.Keyword).Debug(message);
        }

        public void Error(string message, Exception ex = null)
        {
            GetLogger("db").WithProperty("Contest", _repo.StartDate.ToString("yyMMdd") + "_" + _repo.Keyword).Error(ex, message);
        }

        public void Info(string message)
        {
            GetLogger("db").WithProperty("Contest", _repo.StartDate.ToString("yyMMdd") + "_" + _repo.Keyword).Info(message);
        }

        public void Warning(string message)
        {
            GetLogger("db").WithProperty("Contest", _repo.StartDate.ToString("yyMMdd") + "_" + _repo.Keyword).Warn(message);
        }
    }
}