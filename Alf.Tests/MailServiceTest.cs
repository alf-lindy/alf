using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace Alf.Tests
{
    [TestClass]
    public class MailServiceTest
    {
        MailService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new MailService();
        }


        [TestMethod]
        public void Sender_riktig_mail()
        {
            // Arrange
            // Assert
            // Act
            _service.SendSignupEmail();
        }
    }
}
