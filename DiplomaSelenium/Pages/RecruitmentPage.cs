using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSelenium.Pages;

public class RecruitmentPage : BasePage
{
    public RecruitmentPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }
}
