using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [ 
          Test,
          TestCase("abcd1234", false),
          TestCase("irf@uni-corvinus", false),
          TestCase("irf.uni-corvinus.hu", false),
          TestCase("irf@uni-corvinus.hu", true)
            ]
        public void TestValidateEmail(string email, bool expectedResult)
        {

            //Arrange

            var accountController = new AccountController();

            //Act

            var actualResult = accountController.ValidateEmail(email);

            //Assert

            Assert.AreEqual(expectedResult, actualResult);

        }



        /* 
         * "A jelszó legalább 8 karakter hosszú kell legyen, 
         * csak az angol ABC betűiből és számokból állhat, 
         * és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot."
        
        */
        
        [
            Test,
            TestCase("TestJelszo", false),
            TestCase("TESTJELSZO2", false),
            TestCase("testjelszo3", false),
            TestCase("Test4", false),
            TestCase("TestJelszo5", true)
           ]
        public void voidTestValidatePassword ( string password, bool expectedResult)
        {
            //Arrange

            var accountController = new AccountController();

            //Act

            var actualResult = accountController.ValidatePassword(password);

            //Assert

            Assert.AreEqual(expectedResult, actualResult);
        }


        [
         Test,
         TestCase("irf@uni-covrinus.hu","Abcd1234"),
         TestCase("irf@stud.uni-covrinus.hu", "TesztJelszo13")
            ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //Arrange

            var accountController = new AccountController();

            //Act           

            var actualResult = accountController.Register(email, password);

            //Assert

            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);

        }


        [
            Test,
            TestCase("test@test", "TesztJelszo1234"),
            TestCase("test@test.hu", "TESZTJELSZO1234")
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            //Arrange

            var accountController = new AccountController();

            //Act           
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
                
            }
            

            //Assert

            

        }

    }
}
