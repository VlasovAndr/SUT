﻿using OpenQA.Selenium;

namespace TestFramework.Pages.Locators;

public class HomePageLocators
{
    public By ProductLink => By.LinkText("Product");
    public By CreateProductLink => By.LinkText("Create New");
    public By ProductInTable(string name, int columnIndex) => By.XPath($".//table/tbody/tr/td[{columnIndex}][contains(text(),'{name}')]");
    public By ProductOperaton(string name, int columnIndex, string operation) => By.XPath($".//table/tbody/tr/td[{columnIndex}][contains(text(),'{name}')]/..//td[6]/a[text() = '{operation}']");
    public By ColumnsInProductTable => By.XPath($".//table/thead//th");
    public By CellValuesInProductTable(int columnIndex) => By.XPath($".//table/tbody/tr/td[{columnIndex}]");
}
