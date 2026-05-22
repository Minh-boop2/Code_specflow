using System;
using NUnit.Framework;
using SpecFlowLoginDemo.Services;
using TechTalk.SpecFlow;

namespace SpecFlowLoginDemo.StepDefinitions
{
    // Binding giup SpecFlow biet class nay chua cac step Given, When, Then
    [Binding]
    public class LoginSteps
    {
        private readonly LoginService _loginService = new LoginService();
        private LoginResult? _result;

        // Function nay chay truoc moi Scenario
        // Muc dich: reset trang thai de test truoc khong anh huong test sau
        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine("[SETUP] Reset login data before scenario");
            _loginService.Reset();
            _result = null;
        }

        // Step Given: mo ta trang thai ban dau
        [Given(@"the user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            Console.WriteLine("[GIVEN] User is on the login page");
        }

        // Step When: nguoi dung nhap username va password mot lan
        [When(@"the user enters username ""(.*)"" and password ""(.*)""")]
        public void WhenTheUserEntersUsernameAndPassword(string username, string password)
        {
            Console.WriteLine($"[WHEN] Login with username='{username}', password='{password}'");
            _result = _loginService.Login(username, password);
            Console.WriteLine($"[RESULT] Actual message: {_result.Message}");
        }

        // Step When nang cao: nguoi dung nhap sai nhieu lan
        // Dung cho test bao mat khoa tai khoan sau 3 lan sai
        [When(@"the user enters username ""(.*)"" and password ""(.*)"" (\d+) times")]
        public void WhenTheUserEntersUsernameAndPasswordMultipleTimes(string username, string password, int times)
        {
            Console.WriteLine($"[WHEN] Login {times} times with username='{username}', password='{password}'");

            for (int i = 0; i < times; i++)
            {
                _result = _loginService.Login(username, password);
                Console.WriteLine($"[ATTEMPT {i + 1}] Actual message: {_result.Message}");
            }
        }

        // Step Then: so sanh ket qua thuc te voi ket qua mong doi
        [Then(@"the login result should be ""(.*)""")]
        public void ThenTheLoginResultShouldBe(string expectedMessage)
        {
            Console.WriteLine($"[THEN] Expected message: {expectedMessage}");
            Console.WriteLine($"[THEN] Actual message: {_result?.Message}");

            Assert.That(_result, Is.Not.Null);
            Assert.That(_result!.Message, Is.EqualTo(expectedMessage));
        }
    }
}
