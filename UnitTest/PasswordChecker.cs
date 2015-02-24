using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class _PasswordChecker
    {
        Mock<IUserRepository> userRepository;
        private PasswordChecker checker;

        [TestInitialize]
        public void PasswordChecker_Initialize()
        {
            this.userRepository = new Mock<IUserRepository>();
            this.checker = new PasswordChecker(userRepository.Object);
        }

        #region Exceptions

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Verify_PasswordIsNull_Exception()
        {
            string password = null;

            bool result = checker.Verify(password).Item1;
        }

        #endregion

        #region User

        [TestMethod]
        public void Verify_PasswordLengthIsLessThanSeven_False()
        {
            bool expected = false;
            string password = "1a3456";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_PasswordLengthIsGretastThanSeven_True()
        {
            bool expected = true;
            string password = "a234567";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_PasswordDoseNotContainDigit_False()
        {
            bool expected = false;
            string password = "asdfghj";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_PasswordContainsDigit_True()
        {
            bool expected = true;
            string password = "asdfghj7";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_PasswordDoseNotContainAlphabetic_False()
        {
            bool expected = false;
            string password = "1234567";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_PasswordContainsAlphabetic_True()
        {
            bool expected = true;
            string password = "asdfghj7";

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_PasswordInvalid_CreateUser_Never()
        {
            bool expected = false;
            string password = "123456";
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
            this.userRepository.Verify(t => t.CreateUser(It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void Verify_PasswordValid_CreateUser_Once()
        {
            bool expected = true;
            string password = "asdfghj7";
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();

            bool result = checker.Verify(password).Item1;

            Assert.AreEqual(expected, result);
            this.userRepository.Verify(t => t.CreateUser(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Admin

        [TestMethod]
        public void Verify_Admin_PasswordLengthIsLessThanTen_False()
        {
            bool expected = false;
            bool isAdmin = true;
            string password = "a2345678!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_Admin_PasswordLengthIsGretastThanTen_True()
        {
            bool expected = true;
            bool isAdmin = true;
            string password = "a23456789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_Admin_PasswordDoseNotContainDigit_False()
        {
            bool expected = false;
            bool isAdmin = true;
            string password = "asdfghjkl!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_Admin_PasswordContainsDigit_True()
        {
            bool expected = true;
            bool isAdmin = true;
            string password = "asdfghj789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_Admin_PasswordDoseNotContainAlphabetic_False()
        {
            bool expected = false;
            bool isAdmin = true;
            string password = "123456789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_Admin_PasswordContainsAlphabetic_True()
        {
            bool expected = true;
            bool isAdmin = true;
            string password = "asdfghj789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_Admin_PasswordDoseNotContainsSpecialСharacter_False()
        {
            bool expected = false;
            bool isAdmin = true;
            string password = "asdfghj789";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_Admin_PasswordContainsSpecialСharacter_Ture()
        {
            bool expected = true;
            bool isAdmin = true;
            string password = "asdfghj789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Verify_PasswordDoseNotContainsSpecialСharacter_True()
        {
            bool expected = true;
            bool isAdmin = false;
            string password = "asdfghj7";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Verify_Admin_PasswordInvalid_CreateUser_Never()
        {
            bool expected = false;
            bool isAdmin = true;
            string password = "123456";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
            this.userRepository.Verify(t => t.CreateUser(It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void Verify_Admin_PasswordValid_SingIn_Once()
        {
            bool expected = true;
            bool isAdmin = true;
            string password = "asdfghj789!";

            bool result = checker.Verify(password, isAdmin).Item1;

            Assert.AreEqual(expected, result);
            this.userRepository.Verify(t => t.CreateUser(It.IsAny<string>()), Times.Once);
        }

        #endregion
    }
}
