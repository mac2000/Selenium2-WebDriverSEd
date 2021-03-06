﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverSEd.ElementTypes;
using WebDriverSEd.Extensions;

namespace Selenium2WebDriverSEd.Test.Elements
{
    [TestFixture]
    public class GenericElementsTestFixture
    {
        public IWebDriver WebDriver { get; set; }
        public IWebElement WebElement { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            FirefoxProfile profile = new FirefoxProfile();
            WebDriver = new FirefoxDriver(profile);
        }

        [Test]
        public void ExtraElementInfoTest()
        {
            WebDriver.Navigate().GoToUrl("https://stratusbeta.com/");
            var r = new ElementSe(WebDriver, By.TagName("body")).Links;
            LinkSe CreateSiteLink = new LinkSe(WebDriver, By.LinkText("Create New Site"));
            Assert.AreEqual(CreateSiteLink.Url, "https://stratusbeta.com/Site/Create");

            DivSe buttonsDiv = new DivSe(WebDriver, By.CssSelector("#wrapper div#content fieldset div.formButtons"));
            Assert.AreEqual(buttonsDiv.ClassName, "formButtons rightAlign");

            ButtonSe continueButton = new ButtonSe(WebDriver, By.Id("Continue"));          
            Assert.AreEqual(continueButton.ElementTag, "input");
            Assert.AreEqual(continueButton.Value, "Continue");
            Assert.AreEqual(continueButton.Id, "Continue");
            Assert.AreEqual(continueButton.Type, "submit");

            continueButton.Click();

            CheckBoxSe rememberMeCheckBox = new CheckBoxSe(WebDriver, By.Id("rememberMe"));
            Assert.AreEqual(rememberMeCheckBox.Title, "Check this box if you would like us to remember your user name and password for 6 months.");
            Assert.AreEqual(rememberMeCheckBox.Name, "rememberMe");
            Assert.AreEqual(rememberMeCheckBox.IsChecked, false);
            
            rememberMeCheckBox.Click();
            Assert.AreEqual(rememberMeCheckBox.IsChecked, true);

            LabelSe rememberMeLabel = new LabelSe(WebDriver, By.CssSelector("#content form fieldset div.leftColumn div.field label.inline"));
            Assert.AreEqual(rememberMeLabel.For, "rememberMe");
            Assert.AreEqual(rememberMeLabel.Text, "Keep me signed in.");
        }

        [Test]
        public void FindElementsByLinQTest()
        {
            WebDriver.Navigate().GoToUrl("https://stratusbeta.com/");

            LinkSe createNewSiteLink = new LinkSe(WebDriver, By.TagName("a"), i => i.Text == "Create New Site");
            createNewSiteLink.WaitUntilVisible();
        }

        [Test]
        public void MyTest()
        {
            WebDriver.Navigate().GoToUrl("http://www.nuget.org");

            var searchBox = WebDriver.FindElement(By.Id("searchBoxInput"));
            searchBox.SendKeys("WebDriver");

            var searchButton = WebDriver.FindElement(By.Id("searchBoxSubmit"));
            searchButton.Click();

            var searchResults = WebDriver.FindElement(By.Id("searchResults"));
            var theListItems = searchResults.FindElements(By.TagName("li"));

            var searchResults2 = WebDriver.FindElement(By.Id("searchResults"));
            var theListItems2 = searchResults.FindElements(By.CssSelector("ol#searchResults > li"));

            var webDriverSEdLI = theListItems2.First(item => item.Text.Contains("WebDriverSEd"));
            //var thePackage = results.First(i => i.Text.Contains("WebDriverSEd"));
            //thePackage.FindElement(By.TagName("a")).Click();

            webDriverSEdLI.FindElement(By.TagName("a")).Click();

            var theTable = WebDriver.FindElements(By.TagName("table"));

            var table = new TableSeCollection(WebDriver, By.ClassName("sexy-table"));
        }

        [TearDown]
        public virtual void TearDown()
        {
            WebDriver.Dispose();
        }
    }
}
