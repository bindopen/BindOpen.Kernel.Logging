﻿using BindOpen.Scoping.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace BindOpen.Logging.Tests
{
    [TestFixture, Order(401)]
    public class BdoLogFormatTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var log = BdoLogging.NewLog();
            for (int i = 0; i < 2; i++)
            {
                log
                    .AddError("Error" + i)
                    .AddException("Exception" + i)
                    .AddMessage("Message" + i)
                    .AddWarning("Warning" + i)
                    .AddChild(
                        BdoLogging.NewLog().WithDisplayName("Sub log" + i)
                    );
            }

            _testData = new
            {
                log
            };
        }

        [Test, Order(1)]
        public void LogApisTest()
        {
            BdoLog log = _testData.log;

            var count = log.Events().Count();
            Assert.That(log._Events.Count == count, "Bad ToString function.");

            Assert.That(log.Events(q => q.DisplayName == "Message0").Any(), "Bad ToString function.");

            Assert.That(log._Events[4].Parent == log, "Bad ToString function.");

            log = BdoLogging.NewLog();
            log.AddChild(
                    BdoLogging.NewLog()
                        .AddChild(),
                    title: "Child0");
            count = log.LastLogs().Count();
            Assert.That(count == 2, "Bad ToString function.");
        }

        [Test, Order(2)]
        public void LogToStringTest()
        {
            BdoLog log = _testData.log;
            var st = log.ToString<BdoSnapLoggerFormat>();
            var st_expected =
                "- Error0" + Environment.NewLine +
                "- Exception0" + Environment.NewLine +
                "- Message0" + Environment.NewLine +
                "- Warning0" + Environment.NewLine +
                "o Sub log0" + Environment.NewLine +
                "- Error1" + Environment.NewLine +
                "- Exception1" + Environment.NewLine +
                "- Message1" + Environment.NewLine +
                "- Warning1" + Environment.NewLine +
                "o Sub log1" + Environment.NewLine;

            Assert.That(st == st_expected, "Bad ToString function.");
        }

        [Test, Order(3)]
        public void LogEventToStringTest()
        {
            BdoLog log = _testData.log;
            var logEvent = log._Events?[0];
            var st = logEvent.ToString<BdoSnapLoggerFormat>();
            var st_expected = "- Error0";

            Assert.That(st == st_expected, "Bad ToString function.");
        }
    }
}
