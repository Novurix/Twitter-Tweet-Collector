using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Tweet_info
{
    class MainClass
    {
        static IWebDriver driver;
        static string search = "madewithunity";

        static int fileId = 1;
        static FileCreator fileCreator;

        public static void Main(string[] args)
        {
            driver = new FirefoxDriver();
            driver.Url = "https://twitter.com/search?q="+search;

            fileCreator = new FileCreator();

            scrollScreen(3);
        }

        static void scrollScreen(int scrollAmount) {

            Thread.Sleep(3000);

            for (int i = 0; i < scrollAmount; i++) {
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                executor.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
                Thread.Sleep(1000);
            }

            Thread.Sleep(3000);

            for (int tweetId = 0; tweetId < 100; tweetId++) {

                try {

                    IWebElement tweetUserName = driver.FindElement(
                    By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/section/div/div/div/div[" + (tweetId+1).ToString() + "]/div/div/article/div/div/div/div[2]/div[2]/div[1]/div/div/div[1]/div[1]/a/div/div[2]/div/span"));

                    IWebElement[] tweetInputs = new IWebElement[100];

                    for (int tweetInputId = 0; tweetInputId < tweetInputs.Length; tweetInputId++) {
                        try {
                            tweetInputs[tweetInputId] = driver.FindElement(
                            By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/section/div/div/div/div[" + (tweetId + 1).ToString() + "]/div/div/article/div/div/div/div[2]/div[2]/div[2]/div[1]/div/span[" + (tweetInputId+1).ToString() + "]"));

                        }catch (Exception e) {}
                    }

                    string fullTweet = "";

                    for (int tweetInputId = 0; tweetInputId < tweetInputs.Length; tweetInputId++) {
                        if (tweetInputs[tweetInputId] != null) {
                            tweetInputs[tweetInputId].Text.TrimEnd(' ');
                            tweetInputs[tweetInputId].Text.TrimStart(' ');

                            fullTweet += tweetInputs[tweetInputId].Text;
                        }
                    }
                    fileCreator.createFile((tweetId+1).ToString() + "_" + tweetUserName.Text, tweetUserName.Text + ": " + fullTweet);

                    Console.WriteLine(tweetId);
                }catch (Exception e) {}

                Thread.Sleep(1000);
            }
        }
    }
}
