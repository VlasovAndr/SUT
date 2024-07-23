using System;
using Reqnroll;

namespace TestProjectBDD.Hooks;

[Binding]
public class BaseHooks
{
    protected readonly ScenarioContext scenarioContext;

    public BaseHooks(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    public string GetParameterFromTag(string tagWithParam)
    {
        var tags = scenarioContext.ScenarioInfo.Tags;

        foreach (var tag in tags)
        {
            if (tag.Contains(tagWithParam))
            {
                return tag.Split(':')[1];
            }
        }

        throw new ArgumentException($"{tagWithParam} tag does not have ':' separator for parameter");
    }

    public string[] GetParametersFromTag(string tagWithParam)
    {
        var paramsAsString = GetParameterFromTag(tagWithParam);

        return paramsAsString.Split(';');
    }
}