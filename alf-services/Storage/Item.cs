using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.IO;
using System.Configuration;

namespace alf_services.Storage
{
    public class Item
    {
        public static string Database = "testdb";

        public static MemoryStream Get(string filename)
        {
            var server = MongoServer.Create(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
            var database = server.GetDatabase(Database);
            
            var file = database.GridFS.FindOne(Query.EQ("filename", filename));
            var stream = file.OpenRead();

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);

            return new MemoryStream(buffer);
        }

        public static void Store(string filename, MemoryStream stream)
        {
            var server = MongoServer.Create(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
            var database = server.GetDatabase(Database);

            stream.Position = 0;
            database.GridFS.Delete(filename);
            var fsinfo = database.GridFS.Upload(stream, filename);
        }

        public static IEnumerable<string> GetAll()
        {
            var server = MongoServer.Create(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
            var database = server.GetDatabase(Database);

            return database.GridFS.FindAll().Select(x => x.Name);
        }
    }
}
