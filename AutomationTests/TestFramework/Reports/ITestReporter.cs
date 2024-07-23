﻿namespace TestFramework.Reports;

public interface ITestReporter
{
    void AddInfo(string message);
    void AddParameter(string paramName, string paramValue);
    void AddAttachment(string name, string type, byte[] attachment);
}
