using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using alf_services.Storage;

namespace alf_tests
{
    [TestClass]
    public class StorageTest
    {
        [Ignore]
        [TestMethod]
        public void SaveFileAndRetrieve()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("Jabba-dabba-daa"));
            
            Storage.Store("myfile", stream);

            var file = Storage.Get("myfile");

            var db_reader = new StreamReader(file);
            var file_content = db_reader.ReadToEnd();

            Assert.AreEqual("Jabba-dabba-daa", file_content);
        }
        [Ignore]
        [TestMethod]
        public void ServiceReturnsCorrectAmountOfFiles()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("Jabba-dabba-daa"));

            Storage.Store("myfile", stream);
            Storage.Store("myfile2", stream);
            Storage.Store("myfile3", stream);

            Assert.AreEqual(3, Storage.GetAll().Count());
        }
    }
}
