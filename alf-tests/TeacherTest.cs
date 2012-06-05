using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using alf_2013.Models;

namespace alf_tests
{
    [TestClass]
    public class TeacherTest
    {
        [TestMethod]
        public void TeacherNameShouldConcatenateFirstAndLastName()
        {
            var teacher = new Teacher { FirstName = "John", LastName = "Smith" };
        }
    }
}
